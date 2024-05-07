using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;

namespace Test.API.Helpers
{
    public class ListOfIntsBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            if (valueProviderResult == ValueProviderResult.None)
            {
                return Task.CompletedTask;
            }

            var valueAsString = valueProviderResult.FirstValue;

            if (string.IsNullOrEmpty(valueAsString))
            {
                return Task.CompletedTask;
            }

            var valueArray = valueAsString.Split(',', StringSplitOptions.RemoveEmptyEntries);

            var list = new List<int>();

            foreach (var stringValue in valueArray)
            {
                if (int.TryParse(stringValue, NumberStyles.Integer, CultureInfo.InvariantCulture, out var value))
                {
                    list.Add(value);
                }
                else
                {
                    bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, $"The value '{stringValue}' is not a valid integer.");
                }
            }

            bindingContext.Result = ModelBindingResult.Success(list);

            return Task.CompletedTask;
        }
    }
}
