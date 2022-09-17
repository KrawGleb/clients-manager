using Aspose.Pdf;
using ClientsManager.Application.Services.Interfaces;
using ClientsManager.Domain.Models;
using System.Text;
using System.Text.RegularExpressions;

namespace ClientsManager.Application.Services;

public class PrintToPdfService : IPrintToPdfService
{
    private const string HtmlTemplate = @"
        <html>
            <body>
                <hr style=""width: 100%;"" />

                <div style=""display: flex; justify-content: space-between;"">
                  <strong>Исполнитель: ИП Лукашик Н.И.</strong>
                  <strong style=""margin-right: 100px;"">УНП: 291757225</strong>
                </div>

                <div style=""text-align: center"">
                  <h2>Акт №{{Id}} от {{Date}}</h2>
                </div>

                <div>
                  <strong style=""inline-size: 150px; overflow-wrap: break-word;"">Заказчик: {{Customer}}</strong>
                  <table style=""border: 1px solid black; border-collapse: collapse; margin-top: 4px;"">
                      <tr>
                        <th style=""border: 1px solid black; border-collapse: collapse; padding: 3px; max-width: 50px;"">Телефон</th>
                        <th style=""border: 1px solid black; border-collapse: collapse; padding: 3px;"">Марка</th>
                        <th style=""border: 1px solid black; border-collapse: collapse; padding: 3px;"">Год</th>
                        <th style=""border: 1px solid black; border-collapse: collapse; padding: 3px;"">Гос. номер</th>
                        <th style=""border: 1px solid black; border-collapse: collapse; padding: 3px;"">VIN</th>
                        <th style=""border: 1px solid black; border-collapse: collapse; padding: 3px;"">Итого</th>
                      </tr>

                      <tr>
                        <th style=""border: 1px solid black; border-collapse: collapse; padding: 3px;"">{{Phone}}</th>
                        <th style=""border: 1px solid black; border-collapse: collapse; padding: 3px;"">{{CarModel}}</th>
                        <th style=""border: 1px solid black; border-collapse: collapse; padding: 3px;"">{{Year}}</th>
                        <th style=""border: 1px solid black; border-collapse: collapse; padding: 3px;"">{{CarNumber}}</th>
                        <th style=""border: 1px solid black; border-collapse: collapse; padding: 3px;"">{{VIN}}</th>
                        <th style=""border: 1px solid black; border-collapse: collapse; padding: 3px;"">{{Price}} руб.</th>
                      </tr>
                  </table>
                </div>

                <div style=""margin-top: 10px;"">
                    <strong>Наименование работ (услуг):</strong>
                    <div style=""border: 1px solid black; padding:3px; margin-top: 4px;"">
                        {{Description}}
                    </div>
                </div>

                <div style=""display: flex; justify-content: space-between; margin-top: 30px;"">
                    <strong>Исполнитель:</strong>
                    <strong style=""margin-right: 100px;"">Заказчик:</strong>
                </div>
            </body>
        </html>
    ";

    public void CreatePdfDocument(OrderInfo item, string savePath)
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

       
        var html = ReplacePlaceholders(HtmlTemplate, item);

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

    private string ReplacePlaceholders(string template, OrderInfo order)
    {
        template = template
            .Replace("{{Id}}", order.Id.ToString())
            .Replace("{{Date}}", order.CreatedDate.ToShortDateString())
            .Replace("{{Customer}}", WrapIfTextToLong(order.Customer, 95))
            .Replace("{{Phone}}", StringToPhoneNumber(order.PhoneNumber))
            .Replace("{{CarModel}}", order.CarModel)
            .Replace("{{Year}}", order.CarReleaseYear.ToString())
            .Replace("{{CarNumber}}", order.CarNumber)
            .Replace("{{VIN}}", order.VIN)
            .Replace("{{Price}}", order.Price.ToString())
            .Replace("{{Description}}", WrapIfTextToLong(order.Description.Replace("\n", "</br>"), 95));

        return template;
    }

    private string WrapIfTextToLong(string? text, int maxLength)
    {
        if (string.IsNullOrEmpty(text))
        {
            return text;
        }

        if (text.Length >= maxLength)
        {
            for (int i = maxLength; i < text.Length; i += maxLength)
            {
                text = text.Insert(i, "</br>");
            }
        }

        return text;
    }

    private static string StringToPhoneNumber(string value)
    {
        if (string.IsNullOrEmpty(value))
            return string.Empty;

        var phoneNo = value
            .Replace("(", string.Empty)
            .Replace(")", string.Empty)
            .Replace(" ", string.Empty)
            .Replace("+", string.Empty)
            .Replace("-", string.Empty);

        return phoneNo.Length switch
        {
            9 => Regex.Replace(phoneNo, @"(\d{2})(\d{3})(\d{2})(\d{2})", "+375 ($1) $2-$3-$4"),
            12 => Regex.Replace(phoneNo, @"(\d{3})(\d{2})(\d{3})(\d{2})(\d{2})", "+$1 ($2) $3-$4-$5"),
            _ => phoneNo,
        };
    }
}
