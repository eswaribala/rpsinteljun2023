using ProductAPI.Models;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System.Text;

namespace ProductAPI.Configurations
{
    public class CsvOutputFormatter : TextOutputFormatter
    {
        public CsvOutputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("application/csv"));
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }

        protected override bool CanWriteType(Type? type)
            => typeof(Product).IsAssignableFrom(type)
                || typeof(IEnumerable<Product>).IsAssignableFrom(type);

        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            var response = context.HttpContext.Response;
            var buffer = new StringBuilder();

            if (context.Object is IEnumerable<Product>)
            {
                foreach (var Product in (IEnumerable<Product>)context.Object)
                {
                    FormatCsv(buffer, Product);
                }
            }
            else
            {
                FormatCsv(buffer, (Product)context.Object);
            }

            await response.WriteAsync(buffer.ToString(), selectedEncoding);
        }

        private static void FormatCsv(StringBuilder buffer, Product product)
        {
            
                buffer.AppendLine($"{product.ProductId},\"{product.ProductName}");
            
        }
    }
}
