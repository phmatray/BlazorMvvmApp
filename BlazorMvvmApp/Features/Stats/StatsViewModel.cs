using BlazorMvvmApp.Features.Todos;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;

namespace BlazorMvvmApp.Features.Stats;

public partial class StatsViewModel : ObservableRecipient
{
    [ObservableProperty]
    private int totalTodos;

    public StatsViewModel()
    {
        // Subscribe to messages
        WeakReferenceMessenger.Default.Register<TodoAddedMessage>(this, (r, m) =>
        {
            TotalTodos++;
        });
    }
}