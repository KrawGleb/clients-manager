using ClientsManager.App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace ClientsManager.App.Views;
/// <summary>
/// Interaction logic for TableView.xaml
/// </summary>
public partial class TableView : UserControl
{
    private readonly List<OrderInfoModel> _orderInfos = new();

    public TableView()
    {
        InitializeComponent();

        _orderInfos.Add(new() { Id = 1, FullName = "FullName1" });
        _orderInfos.Add(new() { Id = 2, FullName = "FullName1" });
        _orderInfos.Add(new() { Id = 3, FullName = "FullName1" });
        _orderInfos.Add(new() { Id = 4, FullName = "FullName1" });
        _orderInfos.Add(new() { Id = 5, FullName = "FullName1" });
        _orderInfos.Add(new() { Id = 6, FullName = "FullName1" });
        
        
        dgOrders.ItemsSource = _orderInfos;
    }
}
