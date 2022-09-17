using Aspose.Pdf;
using ClientsManager.Application.Services.Interfaces;
using ClientsManager.Domain.Enums;
using ClientsManager.Domain.Models;
using System.Collections;
using System.ComponentModel;
using System.Text;

namespace ClientsManager.Application.Services;

public class PrintToPdfService : IPrintToPdfService
{
    private const string HtmlTemplate = @"
        <html>
            <body>
                {{Content}}
            </body>
        </html>
    ";

    private const string CardTemplate = @"
        <div>
            <h2>{{Id}} {{LastName}} {{FirstName}} {{AdditionalName}}</h2>
            <h3>{{PhoneNumber}}</h3>
            <h3>{{OrderType}}        $: {{Price}}</h3>
            <h3>{{CarModel}} {{CarNumber}}</h3>
            <pre>{{Description}}</pre>
        </div>
    ";

    public void CreatePdfDocument(IEnumerable items, string savePath)
    {
        var options = new HtmlLoadOptions()
        {
            PageInfo =
            {
                Width = 842,
                Height = 1191,
                IsLandscape = true,
            }
        };

        var content = GenerateContent(items);
        var html = HtmlTemplate.Replace("{{Content}}", content).Replace("\'", "\"");

        using var htmlStream = GetHtmlStream(html);

        var document = new Document(htmlStream, options);

        document.Save(savePath);
    }

    private MemoryStream GetHtmlStream(string inputHtml)
    {
        var memoryStream = new MemoryStream();
        var streamWriter = new StreamWriter(memoryStream, new UnicodeEncoding());

        streamWriter.Write(inputHtml);
        streamWriter.Flush();
        memoryStream.Position = 0;

        return memoryStream;
    }

    private string GenerateContent(IEnumerable items)
    {
        var stringBuilder = new StringBuilder();

        foreach (var item in items)
        {
            var order = item as OrderInfo;
            if (order is null)
            {
                continue;
            }

            stringBuilder.Append(ReplacePlaceholders(CardTemplate, order));
        }

        return stringBuilder
            .ToString()
            .Replace('\'', '\"');
    }

    private string ReplacePlaceholders(string template, OrderInfo order)
    {
        template = template
            .Replace("{{Id}}", order.Id.ToString())
            .Replace("{{PhoneNumber}}", order.PhoneNumber)
            .Replace("{{Price}}", order.Price.ToString())
            .Replace("{{CarModel}}", order.CarModel)
            .Replace("{{CarNumber}}", order.CarNumber)
            .Replace("{{OrderType}}", GetEnumDescription(order.OrderType))
            .Replace("{{Description}}", WrapTextWithBreaklines(order.Description));

        return template;
    }

    private string WrapTextWithBreaklines(string? text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return string.Empty;
        }

        var linesCount = text.Length / 100;
        for (int i = 1; i <= linesCount; i++)
        {
            text = text.Insert(100 * i, "</br>");
        }

        return text;
    }

    private string GetEnumDescription(OrderType value)
    {
        var attributes = value
            .GetType()?
            .GetField(value.ToString())?
            .GetCustomAttributes(typeof(DescriptionAttribute), false);

        if (attributes is not null && attributes.Any())
        {
            var desription = (attributes.First() as DescriptionAttribute)!.Description;
            return desription;
        }

        return string.Empty;
    }
}
