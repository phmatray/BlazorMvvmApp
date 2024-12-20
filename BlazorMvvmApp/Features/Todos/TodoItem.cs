using CommunityToolkit.Mvvm.ComponentModel;

namespace BlazorMvvmApp.Features.Todos;

public partial class TodoItem : ObservableObject
{
    [ObservableProperty]
    private string _title = null!;
    
    [ObservableProperty]
    private bool _isComplete;
}