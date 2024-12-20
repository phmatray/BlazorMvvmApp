using System.Collections.Concurrent;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Microsoft.AspNetCore.Components;

namespace BlazorMvvmApp.Core;

public abstract class ViewModelBase : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    
    protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}

public class RelayCommand : ICommand
{
    private readonly Action<object?> _execute;
    private readonly Func<object?, bool>? _canExecute;
    
    public RelayCommand(Action<object?> execute, Func<object?, bool>? canExecute = null)
    {
        _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        _canExecute = canExecute;
    }
    
    public bool CanExecute(object? parameter) => _canExecute == null || _canExecute(parameter);
    public void Execute(object? parameter) => _execute(parameter);
    
    public event EventHandler? CanExecuteChanged;
    public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
}

public class ViewModelComponentBase<TViewModel> : ComponentBase
    where TViewModel : INotifyPropertyChanged
{
    [Inject] public TViewModel ViewModel { get; set; }

    protected override void OnInitialized()
    {
        base.OnInitialized();
        ViewModel.PropertyChanged += HandleViewModelPropertyChanged;
    }

    private void HandleViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        // Ensure UI updates whenever the ViewModel changes.
        InvokeAsync(StateHasChanged);
    }

    public void Dispose()
    {
        ViewModel.PropertyChanged -= HandleViewModelPropertyChanged;
    }
}

public interface IMessenger
{
    void Publish<TMessage>(TMessage message);
    IDisposable Subscribe<TMessage>(Action<TMessage> handler);
}

public class Messenger : IMessenger
{
    // Thread-safe dictionary to hold subscriptions for each message type
    private readonly ConcurrentDictionary<Type, List<WeakReference>> _subscriptions = new();

    public void Publish<TMessage>(TMessage message)
    {
        if (message == null) throw new ArgumentNullException(nameof(message));

        var messageType = typeof(TMessage);

        if (_subscriptions.TryGetValue(messageType, out var subscribers))
        {
            // Create a copy to prevent modification during iteration
            var subscribersCopy = subscribers.ToList();

            foreach (var weakReference in subscribersCopy)
            {
                if (weakReference.Target is Action<TMessage> action)
                {
                    action(message);
                }
                else
                {
                    // Clean up dead references
                    subscribers.Remove(weakReference);
                }
            }
        }
    }

    public IDisposable Subscribe<TMessage>(Action<TMessage> handler)
    {
        if (handler == null) throw new ArgumentNullException(nameof(handler));

        var messageType = typeof(TMessage);
        var weakReference = new WeakReference<Action<TMessage>>(handler);

        var subscribers = _subscriptions.GetOrAdd(messageType, _ => []);
        lock (subscribers)
        {
            subscribers.Add(new WeakReference(handler));
        }

        return new Subscription(() => Unsubscribe(messageType, handler));
    }

    private void Unsubscribe<TMessage>(Type messageType, Action<TMessage> handler)
    {
        if (_subscriptions.TryGetValue(messageType, out var subscribers))
        {
            lock (subscribers)
            {
                var toRemove = subscribers
                    .Where(wr => wr.Target is Action<TMessage> existingHandler && existingHandler == handler)
                    .ToList();

                foreach (var wr in toRemove)
                {
                    subscribers.Remove(wr);
                }

                // Clean up the dictionary if no subscribers remain for the message type
                if (!subscribers.Any())
                {
                    _subscriptions.TryRemove(messageType, out _);
                }
            }
        }
    }

    // Nested Subscription class to handle unsubscription
    private class Subscription : IDisposable
    {
        private Action _disposeAction;
        private bool _isDisposed;

        public Subscription(Action disposeAction)
        {
            _disposeAction = disposeAction ?? throw new ArgumentNullException(nameof(disposeAction));
        }

        public void Dispose()
        {
            if (!_isDisposed)
            {
                _disposeAction();
                _disposeAction = null;
                _isDisposed = true;
            }
        }
    }
}
