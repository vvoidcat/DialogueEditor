using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DialogueEditor.Core.Services.Interfaces;

public interface IFileStorageService
{
	string GetPathToAppData();
	string GetFileNameWithoutExtension(string path);
	string GetFileExtension(string path);
	bool FileExists(string? path);
	DateTime GetFileLastEditedDate(string path);
}
