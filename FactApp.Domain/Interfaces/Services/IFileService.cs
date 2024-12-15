namespace FactApp.Domain.Interfaces.Services
{
    public interface IFileService
    {
        Task<int> DeleteLines(string fileName, int count);
        Task<IList<string>> GetFileContent(string fileName);
        string GetFilePath(string fileName);
        Task SaveToFileAsync(string fileName, string content, bool toNewLine = true);
    }
}
