using Microsoft.Win32;
using DialogueEditor.Core.Services.Interfaces;

namespace DialogueEditor.Core.Services;

public class FileDialogService : IFileDialogService
{
	public bool OpenFileDialog(out string? openedFilePath)
	{
		openedFilePath = null;

		OpenFileDialog openFileDialog = new OpenFileDialog();

		if (openFileDialog.ShowDialog() == true)
		{
			openedFilePath = openFileDialog.FileName;
			return true;
		}

		return false;
	}

	public bool SaveFileDialog(out string? savedFilePath)
	{
		savedFilePath = null;

		SaveFileDialog saveFileDialog = new SaveFileDialog();

		if (saveFileDialog.ShowDialog() == true)
		{
			savedFilePath = saveFileDialog.FileName;
			return true;
		}

		return false;
	}
}
