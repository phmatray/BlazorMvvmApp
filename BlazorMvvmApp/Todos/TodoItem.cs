using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using BlazorMvvmApp.Core;

namespace BlazorMvvmApp.Todos;

public class TodoItem : INotifyPropertyChanged
{
    private string _title;
    private bool _isComplete;

    public string Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }

    public bool IsComplete
    {
        get => _isComplete;
        set => SetProperty(ref _isComplete, value);
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}

public class TodoViewModel : ViewModelBase
{
    private string _newItemTitle;
    public string NewItemTitle
    {
        get => _newItemTitle;
        set
        {
            if (SetProperty(ref _newItemTitle, value))
            {
                // Whenever NewItemTitle changes, the can-execute of AddItemCommand may change.
                (AddItemCommand as RelayCommand)?.RaiseCanExecuteChanged();
            }
        }
    }

    public ObservableCollection<TodoItem> Items { get; } = new();

    public ICommand AddItemCommand { get; }
    public ICommand ToggleCompleteCommand { get; }

    public TodoViewModel()
    {
        AddItemCommand = new RelayCommand(_ => AddItem(), _ => !string.IsNullOrWhiteSpace(NewItemTitle));
        ToggleCompleteCommand = new RelayCommand(ToggleComplete, item => item is TodoItem);
    }

    private void AddItem()
    {
        if (!string.IsNullOrWhiteSpace(NewItemTitle))
        {
            Items.Add(new TodoItem { Title = NewItemTitle, IsComplete = false });
            NewItemTitle = string.Empty;
        }
    }

    private void ToggleComplete(object? parameter)
    {
        if (parameter is TodoItem todo)
        {
            // This will automatically raise PropertyChanged on the item and update the UI
            todo.IsComplete = !todo.IsComplete;
        }
    }
}
