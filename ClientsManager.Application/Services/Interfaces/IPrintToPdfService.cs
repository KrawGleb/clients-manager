using Aspose.Pdf;
using ClientsManager.Domain.Models;
using System.Collections;

namespace ClientsManager.Application.Services.Interfaces;
public interface IPrintToPdfService
{
    void CreatePdfDocument(OrderInfo item, string savePath);
}