using System.Windows;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using DialogueEditor.ViewModels;
using DialogueEditor.Views;
using DialogueEditor.ViewModels.ModelWrappers;
using DialogueEditor.ViewModels.ModelWrappers.Factories;
using DialogueEditor.Core.Services;
using DialogueEditor.Core.Services.Interfaces;

namespace DialogueEditor;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
	public static IHost? AppHost { get; private set; }

	public App()
	{
		AppHost = Host.CreateDefaultBuilder()
			.ConfigureServices((hostContext, services) =>
			{
				// Services
				services.AddSingleton<IFileStorageService, FileStorageService>();
				services.AddSingleton<IFileDialogService, FileDialogService>();

				// ViewModels
				services.AddSingleton<MainViewModel>();
				services.AddSingleton<VariablesViewModel>();
				services.AddSingleton<EditorViewModel>();
				services.AddSingleton<DialogueFilesViewModel>();
				services.AddSingleton<ConfigurationsViewModel>();

				services.AddTransient<DialogueFileWrapper>();
				services.AddTransient<DialogueFileWrapperFactory>();

				services.AddTransient<DialogueWrapper>();
				services.AddTransient<DialogueWrapperFactory>();

				services.AddTransient<DialogueNodeWrapper>();
				services.AddTransient<DialogueNodeWrapperFactory>();

				// Views
				services.AddSingleton<MainWindow>();
				services.AddSingleton<VariablesPanel>();
				services.AddSingleton<ConfigurationsPanel>();
				services.AddSingleton<DialogueFilesPanel>();
				services.AddSingleton<EditorPanel>();
				services.AddSingleton<NodeSettingsPanel>();
			})
			.Build();
	}

	public static T GetService<T>() where T : class
		=> AppHost!.Services.GetRequiredService<T>();

	protected override void OnStartup(StartupEventArgs e)
	{
		var mainWindow = AppHost!.Services.GetRequiredService<MainWindow>();
		mainWindow.Show();

		base.OnStartup(e);
	}

	protected override void OnExit(ExitEventArgs e)
	{
		AppHost!.StopAsync();
		base.OnExit(e);
	}
}