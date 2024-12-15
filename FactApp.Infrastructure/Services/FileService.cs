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

        public async Task SaveToFileAsync(string fileName, string content)
        {
            try
            {
                var filePath = Path.Combine(_directoryPath, fileName);
                await File.AppendAllTextAsync(filePath, content);
            }
            catch (Exception)
            {
                throw new InvalidOperationException("Error occured while saving to file.");
            }
        }
    }
}
