using System.ComponentModel;
using Microsoft.AspNetCore.Components;

namespace BlazorMvvmApp.Components;

public abstract class ViewModelComponentBase<TViewModel>
    : ComponentBase, IDisposable
    where TViewModel : INotifyPropertyChanged
{
    [Inject]
    public required TViewModel ViewModel { get; set; }

    protected override void OnInitialized()
    {
        base.OnInitialized();
        ViewModel.PropertyChanged += OnViewModelPropertyChanged;
    }

    private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        InvokeAsync(StateHasChanged);
    }

    public void Dispose()
    {
        ViewModel.PropertyChanged -= OnViewModelPropertyChanged;
        GC.SuppressFinalize(this);
    }
}