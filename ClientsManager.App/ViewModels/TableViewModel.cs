using ClientsManager.App.Models;
using ClientsManager.App.ViewModels.Dialogs;
using MaterialDesignThemes.Wpf;

namespace ClientsManager.App.ViewModels;

public class TableViewModel
{
    private const string AddDialogIdentifier = "Dialog";

    public async void ShowAddOrderDialog()
    {
        var vm = new AddOrderDialogViewModel();
        var dialogResult = await DialogHost.Show(vm, AddDialogIdentifier);

        if (dialogResult is bool boolResult && boolResult)
        {

        }
        else
        {

        }
    }

    public async void ShowEditOrderDialog(OrderInfoModel orderInfo)
    {
        var vm = new EditOrderDialogViewModel()
        {
            FirstName = "Test" + orderInfo.Id
        };
        var dialogResult = await DialogHost.Show(vm, AddDialogIdentifier);

        if (dialogResult is bool boolResult && boolResult)
        {

        }
        else
        {

        }
    }

    public async void DeleteOrder(OrderInfoModel orderInfo)
    {
        var vm = new WarningMessageDialogViewModel()
        {
            Message = $"Вы собираетесь удалить запись под номером {orderInfo.Id}"
        };
        var dialogResult = await DialogHost.Show(vm, AddDialogIdentifier);

        if (dialogResult is bool boolResult && boolResult)
        {

        }
        else
        {

        }
    }
}
