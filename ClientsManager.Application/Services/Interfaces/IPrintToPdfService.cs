using Aspose.Pdf;
using System.Collections;

namespace ClientsManager.Application.Services.Interfaces;
public interface IPrintToPdfService
{
    void CreatePdfDocument(IEnumerable items, string savePath);
}