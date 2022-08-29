using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace ClientsManager.App.Helpers.Models;

public sealed class NotifyTaskCompletion<TResult> : INotifyPropertyChanged
{
    public NotifyTaskCompletion(Task<TResult> task)
    {
        Task = task;

        if (!task.IsCompleted)
        {
            var _ = WatchTaskAsync(task);
        }
    }

    public NotifyTaskCompletion(Task task)
    {
        if (!task.IsCompleted)
        {
            var _ = WatchTaskAsync(task);
        }
    }

    public Task<TResult> Task { get; private set; }
    public TaskStatus Status { get => Task.Status; }
    public bool IsCompleted { get => Task.IsCompleted; }
    public bool IsNotCompleted { get => !Task.IsCompleted; }
    public bool IsSuccessfullyCompleted { get => Task.Status == TaskStatus.RanToCompletion; }
    public bool IsCanceled { get => Task.IsCanceled; }
    public bool IsFaulted { get => Task.IsFaulted; }
    public AggregateException? Exception { get => Task.Exception; }
    public Exception? InnerException { get => Exception?.InnerException; }
    public string ErrorMessage { get => InnerException?.Message; }
    public TResult? Result
    {
        get => Task.Status == TaskStatus.RanToCompletion
            ? Task.Result
            : default;
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private async Task WatchTaskAsync(Task task)
    {
        try
        {
            await task;
        }
        catch { }

        var propertyChanged = PropertyChanged;
        if (propertyChanged is null)
        {
            return;
        }

        propertyChanged(this, new PropertyChangedEventArgs("Status"));
        propertyChanged(this, new PropertyChangedEventArgs("IsCompleted"));
        propertyChanged(this, new PropertyChangedEventArgs("IsNotCompleted"));

        if (task.IsCanceled)
        {
            propertyChanged(this, new PropertyChangedEventArgs("IsCanceled"));
        }
        else if (task.IsFaulted)
        {
            propertyChanged(this, new PropertyChangedEventArgs("IsFaulted"));
            propertyChanged(this, new PropertyChangedEventArgs("Exception"));
            propertyChanged(this, new PropertyChangedEventArgs("InnerException"));
            propertyChanged(this, new PropertyChangedEventArgs("ErrorMessage"));
        }
        else
        {
            propertyChanged(this, new PropertyChangedEventArgs("IsSuccessfullyCompleted"));
            propertyChanged(this, new PropertyChangedEventArgs("Result"));
        }
    }
}
