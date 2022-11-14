using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Validations
{
    public class FileTypeValidation : ValidationAttribute
    {
        private readonly string[] _validTypes;

        public FileTypeValidation(string[] validTypes)
        {
            _validTypes = validTypes;
        }

        public FileTypeValidation(FileTypeGroup fileTypeGroup)
        {
            if(fileTypeGroup == FileTypeGroup.Image)
            {
                _validTypes = new string[] { "image/jpeg", "image/jpg", "image/png" };
            }
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;

            IFormFile formFile = value as IFormFile;
            if (formFile == null) return ValidationResult.Success;

            if (!_validTypes.Contains(formFile.ContentType))
            {
                return new ValidationResult(
                    $"El tipo del archivo debe ser uno de los siguientes: {string.Join(", ", _validTypes)}"
                    );
            }

            return ValidationResult.Success;
        }
    }
}
