using DialogueEditor.Core.Models;
using DialogueEditor.Core.Services.Interfaces;
using DialogueEditor.ViewModels.Common;

namespace DialogueEditor.ViewModels.ModelWrappers;

public class DialogueFileWrapper : ObservableObject
{
	private readonly IFileStorageService _fileStorageService;

	private DialogueFile _model;
	public DialogueFile Model => _model;

	#region Model Properties

	public Guid Id
	{
		get => _model.Id;
	}

	public string DisplayName
	{
		get => _fileStorageService.GetFileNameWithoutExtension(SystemPath);
	}

	public string SystemPath
	{
		get => _model.SystemPath;
	}

	public bool FileExists
	{
		get => _fileStorageService.FileExists(SystemPath);
	}

	private DateTime _lastModified;
	public DateTime LastModified
	{
		get => _lastModified;
		set => SetProperty(ref _lastModified, value, nameof(LastModified));
	}

	public string? LastModifiedString
	{
		get => FileExists ? $"Last edited: {LastModified}" : null;
	}

	#endregion

	#region Constructors

	public DialogueFileWrapper(DialogueFile model, IFileStorageService fileStorageService)
	{
		_fileStorageService = fileStorageService;

		_model = model;

		LastModified = _model.LastModified;
	}

	#endregion
}
