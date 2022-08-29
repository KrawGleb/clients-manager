using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ClientsManager.App.Commands;

public abstract class AsyncCommandBase : ICommand
{
    private bool _isExecuting;

    public bool IsExeuting
    {
        get =>_isExecuting;
        set { 
            _isExecuting = value;
            OnCanExecuteChanged();
        }
    }

    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter)
        => !IsExeuting;

    public async void Execute(object? parameter)
    {
        IsExeuting = true;

        await ExecuteAsync(parameter);

        IsExeuting = false;
    }

    public abstract Task ExecuteAsync(object? parameter);

    protected void OnCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, new EventArgs());
    }
}
