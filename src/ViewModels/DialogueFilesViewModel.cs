using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using DialogueEditor.Core.Services.Interfaces;
using DialogueEditor.ViewModels.Common;
using DialogueEditor.ViewModels.ModelWrappers;
using DialogueEditor.ViewModels.ModelWrappers.Factories;

namespace DialogueEditor.ViewModels;

public class DialogueFilesViewModel : ObservableObject
{
	private readonly IFileDialogService _dialogService;
	private readonly IFileStorageService _fileStorageService;
	private readonly DialogueFileWrapperFactory _modelWrapperFactory;

	#region Properties

	public delegate void DialogueFileOpenedHandler(DialogueFileWrapper? item);
	public static event DialogueFileOpenedHandler OnDialogueFileOpened = delegate { };

	public ICommand OpenDialogueFileCommand
		=> new RelayCommand<DialogueFileWrapper>(OpenDialogue);
	public ICommand OpenFileExplorerCommand
		=> new RelayCommand<object>(OpenDialogueFromFileExplorer);
	public ICommand CreateNewDialogueCommand
		=> new RelayCommand<object>(CreateNewDialogue);


	private DialogueFileWrapper? _selectedDialogueFile;
	public DialogueFileWrapper? SelectedDialogueFile
	{
		get => _selectedDialogueFile;
		set => SetProperty(ref _selectedDialogueFile, value, nameof(SelectedDialogueFile));
	}

	private string? _filterString;
	public string? FilterString
	{
		get => _filterString;
		set
		{
			if (SetProperty(ref _filterString, value, nameof(FilterString)))
			{
				OnPropertyChanged(nameof(FilteredHistoryDialogues));
				OnPropertyChanged(nameof(IsVisibleNoItemsInHistoryLabel));
			}
		}
	}

	public ObservableCollection<DialogueFileWrapper> FilteredHistoryDialogues
	{
		get => string.IsNullOrEmpty(FilterString) ?
			new(AllHistoryDialogues.OrderByDescending(x => x.LastModified)) :
			new(AllHistoryDialogues.Where(x => x.DisplayName.ToUpper().Contains(FilterString.ToUpper())).OrderByDescending(x => x.LastModified));
	}

	private ObservableCollection<DialogueFileWrapper> _allHistoryDialogues = new();
	public ObservableCollection<DialogueFileWrapper> AllHistoryDialogues
	{
		get => _allHistoryDialogues;
		set
		{
			_allHistoryDialogues = value;
			OnPropertyChanged(nameof(AllHistoryDialogues));
		}
	}

	public Visibility IsVisibleNoItemsInHistoryLabel
	{
		get => FilteredHistoryDialogues.Any() ? Visibility.Hidden : Visibility.Visible;
	}

	#endregion

	#region Constructors

	public DialogueFilesViewModel
	(
		IFileDialogService dialogService,
		IFileStorageService fileStorageService,
		DialogueFileWrapperFactory modelWrapperFactory
	)
	{
		_dialogService = dialogService;
		_fileStorageService = fileStorageService;
		_modelWrapperFactory = modelWrapperFactory;

		AllHistoryDialogues.CollectionChanged += (s, e) =>
		{
			OnPropertyChanged(nameof(FilteredHistoryDialogues));
			OnPropertyChanged(nameof(IsVisibleNoItemsInHistoryLabel));
		};

	}

	#endregion

	#region Private Methods

	private void OpenDialogue(DialogueFileWrapper file)
	{
		SelectedDialogueFile = file;
		OnDialogueFileOpened(SelectedDialogueFile);
	}

	private void OpenDialogueFromFileExplorer()
	{
		if (_dialogService.OpenFileDialog(out string? openedFile) && _fileStorageService.FileExists(openedFile))
		{
			var fileViewModel = _modelWrapperFactory.Create(Core.Models.DialogueFile.Create(openedFile!, DateTime.Now));

			if (!AllHistoryDialogues.Any(x => x.Model == fileViewModel.Model))
			{
				AllHistoryDialogues.Add(fileViewModel);
			}
			else
			{
				fileViewModel.LastModified = DateTime.Now;
			}

			OpenDialogue(fileViewModel);
		}
	}

	private void CreateNewDialogue()
	{
		if (_dialogService.SaveFileDialog(out string? savedFile))
		{

		}
	}

	#endregion
}
