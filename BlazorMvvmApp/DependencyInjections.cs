using BlazorMvvmApp.Features.Stats;
using BlazorMvvmApp.Features.Todos;
using CommunityToolkit.Mvvm.Messaging;

namespace BlazorMvvmApp;

public static class DependencyInjections
{
    public static void AddMvvm(this IServiceCollection services)
    {
        services.RegisterMessenger();
        services.RegisterViewModels();
    }
    
    private static void RegisterMessenger(this IServiceCollection services)
    {
        services.AddSingleton<IMessenger, WeakReferenceMessenger>();
    }
    
    private static void RegisterViewModels(this IServiceCollection services)
    {
        services.AddSingleton<TodoViewModel>();
        services.AddSingleton<StatsViewModel>();
    }
}