using Aspose.Pdf;
using System.Collections;

namespace ClientsManager.Application.Services.Interfaces;
public interface IPrintToPdfService
{
    Document CreatePdfDocument(IEnumerable items);
}