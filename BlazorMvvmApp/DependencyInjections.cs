using BlazorMvvmApp.Features.Stats;
using BlazorMvvmApp.Features.Todos;
using CommunityToolkit.Mvvm.Messaging;

namespace BlazorMvvmApp;

public static class DependencyInjections
{
    public static void AddMvvm(this IServiceCollection services)
    {
        services.RegisterViewModels();
        services.RegisterMessenger();
    }
    
    private static void RegisterViewModels(this IServiceCollection services)
    {
        services.AddSingleton<TodoViewModel>();
        services.AddSingleton<StatsViewModel>();
    }

    private static void RegisterMessenger(this IServiceCollection services)
    {
        services.AddSingleton<IMessenger, WeakReferenceMessenger>();
    }
}