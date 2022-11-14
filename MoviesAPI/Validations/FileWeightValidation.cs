using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Validations
{
    public class FileWeightValidation : ValidationAttribute
    {
        private readonly int _maxWeightInMB;

        public FileWeightValidation(int maxWeightInMB)
        {
            _maxWeightInMB = maxWeightInMB;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;

            IFormFile formFile = value as IFormFile;
            if (formFile == null) return ValidationResult.Success;

            if(formFile.Length > _maxWeightInMB * 1024 * 1024)
            {
                string errorMessage = $"El peso del archivo no debe ser mayor a {_maxWeightInMB}MB";
                return new ValidationResult(errorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
