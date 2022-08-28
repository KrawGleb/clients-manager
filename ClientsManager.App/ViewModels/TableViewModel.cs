using ClientsManager.App.Models;
using MaterialDesignThemes.Wpf;

namespace ClientsManager.App.ViewModels;

public class TableViewModel
{
    private const string AddOrderDialogIdentifier = "AddOrderDialog";

    public async void ShowAddOrderDialog()
    {
        var vm = new AddOrderDialogViewModel();
        var dialogResult = await DialogHost.Show(vm, AddOrderDialogIdentifier);

        if (dialogResult is bool boolResult && boolResult)
        {

        }
        else
        {

        }
    }

    public async void ShowEditOrderDialog(OrderInfoModel orderInfo)
    {
        var vm = new AddOrderDialogViewModel()
        {
            FirstName = "Test" + orderInfo.Id
        };
        var dialogResult = await DialogHost.Show(vm, AddOrderDialogIdentifier);

        if (dialogResult is bool boolResult && boolResult)
        {

        }
        else
        {

        }
    }

    public async void DeleteOrder(OrderInfoModel orderInfo)
    {

    }
}
