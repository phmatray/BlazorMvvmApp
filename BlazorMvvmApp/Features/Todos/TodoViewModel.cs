using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace BlazorMvvmApp.Features.Todos;

public partial class TodoViewModel : ObservableRecipient
{
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(AddItemCommand))]
    private string _newItemTitle = string.Empty;

    public ObservableCollection<TodoItem> Items { get; } =
    [
        new() { Title = "Learn about Blazor", IsComplete = true },
        new() { Title = "Build a Blazor app", IsComplete = false }
    ];

    [RelayCommand(CanExecute = nameof(CanAddItem))]
    private void AddItem()
    {
        var newItem = new TodoItem { Title = NewItemTitle, IsComplete = false };
        Items.Add(newItem);
        NewItemTitle = string.Empty;
        
        // Publish the message
        WeakReferenceMessenger.Default.Send(new TodoAddedMessage(newItem));
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
        // Simulate async data loading
        await Task.Delay(500);
        Items.Add(new TodoItem { Title = "Async Todo 1", IsComplete = false });
        Items.Add(new TodoItem { Title = "Async Todo 2", IsComplete = true });
    }
}