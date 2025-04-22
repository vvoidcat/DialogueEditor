using DialogueEditor.Core.Models;
using DialogueEditor.ViewModels.ModelWrappers.Factories.Interfaces;

namespace DialogueEditor.ViewModels.ModelWrappers.Factories;

public class DialogueWrapperFactory : IModelWrapperFactory<DialogueWrapper, Dialogue>
{
	public DialogueWrapper Create(Dialogue model)
		=> new DialogueWrapper(model);
}
