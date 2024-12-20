using System.Collections.Specialized;
using BlazorMvvmApp.Features.Todos;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BlazorMvvmApp.Features.Stats;

public partial class StatsViewModel
    : ObservableRecipient, IDisposable
{
    private readonly ITodoService _todoService;

    [ObservableProperty]
    private int _totalTodos;

    public StatsViewModel(ITodoService todoService)
    {
        ArgumentNullException.ThrowIfNull(todoService);
        
        _todoService = todoService;
        _totalTodos = _todoService.Items.Count;

        // Subscribe to collection changes
        _todoService.Items.CollectionChanged += OnItemsCollectionChanged;
    }

    private void OnItemsCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        switch (e.Action)
        {
            case NotifyCollectionChangedAction.Add:
                TotalTodos += e.NewItems?.Count ?? 0;
                break;
            case NotifyCollectionChangedAction.Remove:
                TotalTodos -= e.OldItems?.Count ?? 0;
                break;
        }
        // Handle other actions if necessary
    }

    public void Dispose()
    {
        _todoService.Items.CollectionChanged -= OnItemsCollectionChanged;
        GC.SuppressFinalize(this);
    }
}