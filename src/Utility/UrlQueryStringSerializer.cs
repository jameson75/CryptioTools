using System;
using System.Reflection;
using System.Text;

namespace CipherPark.CryptioTools.Utility
{
    public static class UrlQueryStringSerializer
    {
        public static string SerializeObject(object data)
        {
            StringBuilder builder = new StringBuilder();
            Type type = data.GetType();
            PropertyInfo[] properties = type.GetProperties();
            foreach (var property in properties)
            {
                var attribute = property.GetCustomAttribute<UrlParameterAttribute>();

                if (property.CanRead && property.GetGetMethod().IsPublic)
                {
                    string parameterName = (attribute == null || attribute.Name == null) ? property.Name : attribute.Name;
                    bool isFlag = attribute != null && attribute.IsFlag;
                    bool isRequired = attribute != null && attribute.IsRequired;
                    
                    object value = property.GetValue(data);
                    if (!string.IsNullOrEmpty(value?.ToString()))
                    {
                        if (builder.Length > 0)
                            builder.Append('&');

                        if (isFlag)
                            builder.Append($"{parameterName}");
                        else
                            builder.Append($"{parameterName}={value}");
                    }
                    else if (isRequired)
                        throw new InvalidOperationException($"Required value for {parameterName} is null or empty.");
                }
                else if (attribute != null)
                    throw new InvalidOperationException($"Write-only property {property.Name} decorated with {nameof(UrlParameterAttribute)}");
            }

            return builder.ToString();
        }
    }
}
