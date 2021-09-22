using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Programmania.Utilities
{
    public class FormError
    {
        public static string MakeModelError(ModelStateDictionary modelState)
        {
            var modelEntry = modelState.Values.GetEnumerator();
            var key = modelState.Keys.GetEnumerator();
            System.Text.StringBuilder str = new System.Text.StringBuilder();
            System.IO.StringWriter tw = new System.IO.StringWriter(str);
            using (Newtonsoft.Json.JsonWriter writer = new Newtonsoft.Json.JsonTextWriter(tw))
            {
                writer.WriteStartObject();
                while (key.MoveNext() && modelEntry.MoveNext())
                {
                    if (modelEntry.Current.ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                    {
                        writer.WritePropertyName(key.Current);
                        writer.WriteValue(modelEntry.Current.Errors[0].ErrorMessage);
                    }
                }
                writer.WriteEndObject();
            }
            return str.ToString();
        }

        public static string MakeServerError(string key, string value)
        {
            System.Text.StringBuilder str = new System.Text.StringBuilder();
            System.IO.StringWriter tw = new System.IO.StringWriter(str);
            using (Newtonsoft.Json.JsonWriter writer = new Newtonsoft.Json.JsonTextWriter(tw))
            {
                writer.WriteStartObject();
                writer.WritePropertyName(key);
                writer.WriteValue(value);
                writer.WriteEndObject();
            }
            return str.ToString();
        }
    }
}
