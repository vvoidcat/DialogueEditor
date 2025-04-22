namespace DialogueEditor.ViewModels.ModelWrappers.Factories.Interfaces;

public interface IModelWrapperFactory<ViewModel, Model>
{
	ViewModel Create(Model model);
}
