using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace MoviesAPI.Helpers
{
    public class TypeBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var propertyName = bindingContext.ModelName;
            var valuesProvider = bindingContext.ValueProvider.GetValue(propertyName);

            if (valuesProvider == ValueProviderResult.None) return Task.CompletedTask;

            try
            {
                var deserializedValue = JsonConvert.DeserializeObject<List<int>>(valuesProvider.FirstValue);
                bindingContext.Result = ModelBindingResult.Success(deserializedValue);
            }
            catch
            {
                bindingContext.ModelState.TryAddModelError(propertyName, "Invalid value for type List<int>");
            }

            return Task.CompletedTask;
        }
    }
}
