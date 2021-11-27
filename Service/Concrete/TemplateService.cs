using CryptoReporter.Service.Contract;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace CryptoReporter.Service.Concrete
{
    public class TemplateService<T> : ITemplateService<T>
    {
        public Task<string> CreateMessageContent(string html, T data)
        {
            Type type = typeof(T);
            PropertyInfo[] properties = type.GetProperties();

            string result = html;
            foreach (PropertyInfo property in properties)
            {
                result = result.Replace("{" + property.Name + "}", property.GetValue(data)?.ToString() ?? "");
            }

            return Task.FromResult(result);
        }

    }
}
