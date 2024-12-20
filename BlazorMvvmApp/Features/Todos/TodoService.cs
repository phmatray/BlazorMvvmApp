using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace BlazorMvvmApp.Features.Todos;

public class TodoService : ITodoService, INotifyPropertyChanged
{
    public ObservableCollection<TodoItem> Items { get; } =
    [
        new() { Title = "Learn about Blazor", IsComplete = true },
        new() { Title = "Build a Blazor app", IsComplete = false }
    ];

    public event PropertyChangedEventHandler? PropertyChanged;

    public TodoService()
    {
        Items.CollectionChanged += OnItemsCollectionChanged;
    }

    public void AddTodo(TodoItem item)
    {
        Items.Add(item);
        // Notify that Items has changed (optional, since ObservableCollection already notifies)
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Items)));
    }

    public async Task LoadTodosAsync()
    {
        // Simulate async data loading
        await Task.Delay(500);
        var todo1 = new TodoItem { Title = "Async Todo 1", IsComplete = false };
        var todo2 = new TodoItem { Title = "Async Todo 2", IsComplete = true };
        AddTodo(todo1);
        AddTodo(todo2);
    }

    private void OnItemsCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        // Notify subscribers about changes in the collection
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Items)));
    }
}