using System.IO;
using DialogueEditor.Core.Services.Interfaces;

namespace DialogueEditor.Core.Services;

public class FileStorageService : IFileStorageService
{
	public string GetPathToAppData()
		=> Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "DialogueEditor");   /// 

	public string GetFileNameWithoutExtension(string path)
		=> Path.GetFileNameWithoutExtension(path);

	public string GetFileExtension(string path)
		=> Path.GetExtension(path);

	public bool FileExists(string? path)
		=> !string.IsNullOrWhiteSpace(path) && File.Exists(path);

	public DateTime GetFileLastEditedDate(string path)
		=> File.GetLastWriteTime(path);
}
