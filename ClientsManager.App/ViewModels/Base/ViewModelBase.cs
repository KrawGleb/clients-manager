using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ClientsManager.App.ViewModels.Base;

public abstract class ViewModelBase : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected virtual bool Set<T>(ref T filed, T value, [CallerMemberName] string? propertyName = null)
    {
        if (Equals(filed, value))
        {
            return false;
        }

        filed = value;
        OnPropertyChanged(propertyName);

        return true;
    }
}
