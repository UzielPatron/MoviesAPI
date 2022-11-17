using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MoviesAPI.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MoviesAPI.Controllers
{
    [ApiController]
    [Route("api/accounts")]
    public class AccountsController : CustomBaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountsController(
            ApplicationDbContext context,
            IMapper mapper,
            IConfiguration configuration,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
            : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("create")]
        public async Task<ActionResult<UserToken>> CreateUser([FromBody] UserInfo userModel)
        {
            var user = new IdentityUser { UserName = userModel.Email, Email = userModel.Email };
            var result = await _userManager.CreateAsync(user, userModel.Password);

            if (result.Succeded) return await BuildToken(userModel);
            else return BadRequest(result.Errors);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserToken>> Login([FromBody] UserInfo userModel)
        {
            var result = await _signInManager.PasswordSignInAsync(
                userModel.Email,
                userModel.Password,
                isPersistent: false,
                lockoutOnFailure: false
                );

            if (result.Succeded) return await BuildToken(userModel);
            else return BadRequest("Invalid email or password");
        }

        [HttpGet("users")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<ActionResult<List<UserDTO>>> Get([FromQuery] PaginationDTO paginationDTO)
        {
            var queryable = _context.Users.AsQueryable();
            queryable = queryable.OrderBy(x => x.Email);

            return await Get<IdentityUser, UserDTO>(paginationDTO);
        }

        [HttpPost("renewToken")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<UserToken>> RenewToken()
        {
            var userInfo = new UserInfo
            {
                Email = HttpContext.User.Identity.Name
            };

            return await BuildToken(userInfo);
        }

        [HttpGet("roles")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<ActionResult<List<string>>> GetRoles()
        {
            return await _context.Roles.Select(x => x.Name).ToListAsync();
        }

        [HttpPost("assignRole")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<ActionResult> AssignRole(EditRoleDTO editRoleDTO)
        {
            var user = await _userManager.FindByIdAsync(editRoleDTO.UserId);
            if (user == null) return NotFound("The specified Id is not found");

            await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, editRoleDTO.NameRole));
            return NoContent();
        }


        [HttpPost("removeRole")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<ActionResult> RemoveRole(EditRoleDTO editRoleDTO)
        {
            var user = await _userManager.FindByIdAsync(editRoleDTO.UserId);
            if (user == null) return NotFound("The specified Id is not found");

            await _userManager.RemoveClaimAsync(user, new Claim(ClaimTypes.Role, editRoleDTO.NameRole));
            return NoContent();
        }

        private async Task<UserToken> BuildToken(UserInfo userInfo)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, userInfo.Email),
                new Claim(ClaimTypes.Email, userInfo.Email)
            };

            var identityUser = await _userManager.FindByEmailAsync(userInfo.Email);

            claims.Add(new Claim(ClaimTypes.NameIdentifier, identityUser.Id));

            var claimsDB = await _userManager.GetClaimsAsync(identityUser);

            claims.AddRange(claimsDB);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expirationDate = DateTime.UtcNow.AddYears(1);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: expirationDate,
                signingCredentials: credentials
                );

            return new UserToken()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ExpirationDate = expirationDate
            };
        }


    }
}
