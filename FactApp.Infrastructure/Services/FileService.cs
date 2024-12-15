using FactApp.Domain.Interfaces.Services;

namespace FactApp.Infrastructure.Services
{
    internal class FileService : IFileService
    {
        private readonly string _directoryPath;

        public FileService(string directoryPath)
        {
            _directoryPath = directoryPath;

            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }
        }

        public string GetFilePath(string fileName)
        {
            return Path.Combine(_directoryPath, fileName);
        }
        public async Task SaveToFileAsync(string fileName, string content, bool toNewLine = true)
        {
            try
            {
                var filePath = GetFilePath(fileName);
                await File.AppendAllTextAsync(filePath, toNewLine ? content + '\n' : content);
            }
            catch (Exception)
            {
                throw new InvalidOperationException("Error occured while saving to file.");
            }
        }

        public async Task<IList<string>> GetFileContent(string fileName)
        {
            if (!IsFileExists(fileName))
            {
                throw new NullReferenceException($"File {fileName} does not exists.");
            }
            var content = await File.ReadAllLinesAsync(GetFilePath(fileName));
            return content.ToList();
        }

        public bool IsFileExists(string fileName)
        {
            return File.Exists(GetFilePath(fileName));
        }

        public async Task<int> DeleteLines(string fileName, int count)
        {
            var content = await GetFileContent(fileName);
            var newContent = content.Skip(count);

            await File.WriteAllLinesAsync(GetFilePath(fileName), newContent);

            return content.Count - newContent.Count();
        }
    }
}
