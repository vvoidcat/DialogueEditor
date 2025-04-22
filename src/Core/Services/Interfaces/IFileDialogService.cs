namespace DialogueEditor.Core.Services.Interfaces;

public interface IFileDialogService
{
	bool OpenFileDialog(out string? openedFilePath);
	bool SaveFileDialog(out string? savedFilePath);
}
