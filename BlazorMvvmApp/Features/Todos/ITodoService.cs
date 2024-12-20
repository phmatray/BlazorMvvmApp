using System.Collections.ObjectModel;

namespace BlazorMvvmApp.Features.Todos;

public interface ITodoService
{
    ObservableCollection<TodoItem> Items { get; }
    void AddTodo(TodoItem item);
    Task LoadTodosAsync();
}