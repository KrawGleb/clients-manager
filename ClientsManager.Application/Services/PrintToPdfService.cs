using Aspose.Pdf;
using Aspose.Pdf.Text;
using ClientsManager.Application.Services.Interfaces;
using ClientsManager.Domain.Enums;
using ClientsManager.Domain.Models;
using System.Collections;
using System.ComponentModel;

namespace ClientsManager.Application.Services;

public class PrintToPdfService : IPrintToPdfService
{
    public Document CreatePdfDocument(IEnumerable items)
    {
        var document = new Document();
        document.PageMode = PageMode.FullScreen;
        var page = document.Pages.Add();


        var table = new Table()
        {
            ColumnAdjustment = ColumnAdjustment.AutoFitToContent,
            DefaultCellBorder = new BorderInfo(BorderSide.Box, 0.2f, Color.Black),
            Alignment = HorizontalAlignment.Center,
            DefaultCellPadding = new MarginInfo()
            {
                Left = 2,
                Right = 4
            },
            DefaultCellTextState =
            {
                Font = FontRepository.FindFont("Times New Roman"),
            }
        };

        var headerRow = table.Rows.Add();
        headerRow.Cells.Add("№");
        headerRow.Cells.Add("Тип");
        headerRow.Cells.Add("ФИО");
        headerRow.Cells.Add("Телефон");
        headerRow.Cells.Add("Марка");
        headerRow.Cells.Add("Номер");
        headerRow.Cells.Add("Описание");
        headerRow.Cells.Add("$");

        foreach (var item in items)
        {
            var order = item as OrderInfo;
            if (order is null)
            {
                continue;
            }

            var row = table.Rows.Add();

            row.Cells.Add(order.Id.ToString() ?? "");
            row.Cells.Add(GetEnumDescription(order.OrderType));
            row.Cells.Add($"{order.LastName} {order.FirstName} {order.AdditionalName}");
            row.Cells.Add(order.PhoneNumber);
            row.Cells.Add(order.CarModel ?? "");
            row.Cells.Add(order.CarNumber ?? "");
            row.Cells.Add(order.Description ?? "");
            row.Cells.Add(order.Price.ToString() ?? "");
        }

        page.Paragraphs.Add(table);

        return document;
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
