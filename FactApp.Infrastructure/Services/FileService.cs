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

        public async Task SaveToFileAsync(string fileName, IEnumerable<string> content)
        {
            try
            {
                var filePath = GetFilePath(fileName);
                await File.AppendAllLinesAsync(filePath, content);
            }
            catch (Exception)
            {
                throw new InvalidOperationException("Error occured while saving to file.");
            }
        }

        public async Task<IList<string>> GetFullFileContent(string fileName)
        {
            if (!IsFileExists(fileName))
            {
                throw new NullReferenceException($"File {fileName} does not exists.");
            }
            var content = await File.ReadAllLinesAsync(GetFilePath(fileName));

            return content.ToList();
        }

        public async Task<IList<string>> GetFileContent(string fileName, int? count = null)
        {
            if (!count.HasValue || count == 0)
            {
                return await GetFullFileContent(fileName);
            }
            if (count < 0)
            {
                throw new ArgumentException("The number of lines to retrieve must be greater or equals 0.", nameof(count));
            }
            if (!IsFileExists(fileName))
            {
                throw new NullReferenceException($"File {fileName} does not exists.");
            }

            try
            {
                var filePath = GetFilePath(fileName);

                var lines = new List<string>();
                using (var reader = new StreamReader(filePath))
                {
                    for (int i = 0; i < count; i++)
                    {
                        var line = await reader.ReadLineAsync();
                        if (line == null)
                        {
                            break;
                        }

                        lines.Add(line);
                    }
                }

                return lines;
            }
            catch (Exception)
            {
                throw new InvalidOperationException("Error occurred while reading the file.");
            }
        }

        public bool IsFileExists(string fileName)
        {
            return File.Exists(GetFilePath(fileName));
        }

        public async Task<int> DeleteLines(string fileName, int? count)
        {
            if (!count.HasValue)
            {
                var linesCount = await GetLineCount(fileName);
                await File.WriteAllTextAsync(GetFilePath(fileName), string.Empty);
                return linesCount;
            }
            var content = await GetFileContent(fileName);
            var newContent = content.Skip(count.Value);

            await File.WriteAllLinesAsync(GetFilePath(fileName), newContent);

            return content.Count - newContent.Count();
        }

        public async Task<int> GetLineCount(string fileName)
        {
            if (!IsFileExists(fileName))
            {
                throw new NullReferenceException($"File {fileName} does not exist.");
            }

            try
            {
                var filePath = GetFilePath(fileName);
                int lineCount = 0;

                using (var reader = new StreamReader(filePath))
                {
                    while (await reader.ReadLineAsync() != null)
                    {
                        lineCount++;
                    }
                }

                return lineCount;
            }
            catch (Exception)
            {
                throw new InvalidOperationException("Error occurred while counting lines in the file.");
            }
        }

    }
}
