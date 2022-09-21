using ClientsManager.Domain.Models;

namespace ClientsManager.Application.Services.Interfaces;
public interface IPrintToPdfService
{
    void CreatePdfDocument(OrderInfo item, string savePath);
}