namespace FactApp.Domain.Interfaces.Services
{
    public interface IFileService
    {
        Task<IList<string>> GetFullFileContent(string fileName);
        string GetFilePath(string fileName);
        Task SaveToFileAsync(string fileName, string content, bool toNewLine = true);
        Task<IList<string>> GetFileContent(string fileName, int? count);
        Task<int> GetLineCount(string fileName);
        Task<int> DeleteLines(string fileName, int? count);
    }
}
