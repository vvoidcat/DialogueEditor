using DialogueEditor.Core.Models;
using DialogueEditor.Core.Services.Interfaces;
using DialogueEditor.ViewModels.ModelWrappers.Factories.Interfaces;

namespace DialogueEditor.ViewModels.ModelWrappers.Factories;

public class DialogueFileWrapperFactory : IModelWrapperFactory<DialogueFileWrapper, DialogueFile>
{
	private readonly IFileStorageService _fileStorageService;

	public DialogueFileWrapperFactory(IFileStorageService fileStorageService)
	{
		_fileStorageService = fileStorageService;
	}

	public DialogueFileWrapper Create(DialogueFile model)
		=> new DialogueFileWrapper(model, _fileStorageService);
}
