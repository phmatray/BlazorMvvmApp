using System.Collections.ObjectModel;
using BlazorMvvmApp.Features.Todos;

namespace BlazorMvvmApp.Services;

public interface ITodoService
{
    ObservableCollection<TodoItem> Items { get; }
    void AddTodo(TodoItem item);
    Task LoadTodosAsync();
}