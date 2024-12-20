using System.Collections.ObjectModel;
using System.Collections.Specialized;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BlazorMvvmApp.Features.Todos;

public partial class TodoViewModel
    : ObservableRecipient, IDisposable
{
    private readonly ITodoService _todoService;

    public TodoViewModel(ITodoService todoService)
    {
        ArgumentNullException.ThrowIfNull(todoService);
        
        _todoService = todoService;
        
        // Subscribe to collection changes
        _todoService.Items.CollectionChanged += OnItemsCollectionChanged;
    }
    
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(AddItemCommand))]
    private string _newItemTitle = string.Empty;

    public ObservableCollection<TodoItem> Items
        => _todoService.Items;

    [RelayCommand(CanExecute = nameof(CanAddItem))]
    private void AddItem()
    {
        var newItem = new TodoItem { Title = NewItemTitle, IsComplete = false };
        Items.Add(newItem);
        NewItemTitle = string.Empty;
    }

    private bool CanAddItem()
        => !string.IsNullOrWhiteSpace(NewItemTitle);

    [RelayCommand]
    private void ToggleComplete(TodoItem todo)
    {
        // This will automatically raise PropertyChanged on the item and update the UI
        todo.IsComplete = !todo.IsComplete;
    }

    [RelayCommand]
    private async Task LoadTodosAsync()
    {
        await _todoService.LoadTodosAsync();
    }
    
    private void OnItemsCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        // Notify the UI that the Items collection has changed
        OnPropertyChanged(nameof(Items));
    }
    
    public void Dispose()
    {
        _todoService.Items.CollectionChanged -= OnItemsCollectionChanged;
        GC.SuppressFinalize(this);
    }
}