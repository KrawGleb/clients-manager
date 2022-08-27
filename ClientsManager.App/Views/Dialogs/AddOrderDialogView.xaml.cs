using ClientsManager.Domain.Models;
using ClientsManager.Infrastructure.Helpers.Interfaces;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClientsManager.App.Views.Dialogs;

/// <summary>
/// Interaction logic for AddOrderDialogView.xaml
/// </summary>
public partial class AddOrderDialogView : UserControl
{

    public AddOrderDialogView()
    {    
        InitializeComponent();
    }

    private void Save_Click(object sender, RoutedEventArgs e)
    {
        // TODO: OrderType
        var orderInfo = new OrderInfo()
        {
            FirstName = FirstName.Text,
            LastName = LastName.Text,
            AdditionalName = AdditionalName.Text,
            PhoneNumber = PhoneNumber.Text,
            CarModel = CarModel.Text,
            CarNumber = CarNumber.Text,
            Price = Convert.ToDecimal(Price.Text ?? "0"),
            Description = Description.Text,
        };

        Console.WriteLine(JsonSerializer.Serialize(orderInfo));
    }

    private void Cancel_Click(object sender, RoutedEventArgs e)
    {
        CloseDialog();
    }

    private void CloseDialog()
    {
        var command = DialogHost.CloseDialogCommand;

        if (command.CanExecute(null, this))
        {
            command.Execute(null, this);
        }
    }
}
