namespace FactApp.Domain.Interfaces.Services
{
    public interface IFileService
    {
        /// <summary>
        /// Retrieves the entire content of the specified file as a list of strings, where each string represents a line in the file.
        /// </summary>
        /// <param name="fileName">The name of the file to read.</param>
        /// <returns>A list of strings containing all lines in the file.</returns>
        /// <exception cref="NullReferenceException">Thrown when the specified file does not exist.</exception>
        /// <exception cref="InvalidOperationException">Thrown when an error occurs while reading the file.</exception>
        Task<IList<string>> GetFullFileContent(string fileName);

        /// <summary>
        /// Gets the full file path of a file located in the directory managed by the service.
        /// </summary>
        /// <param name="fileName">The name of the file to locate.</param>
        /// <returns>The full file path as a string.</returns>
        string GetFilePath(string fileName);

        /// <summary>
        /// Appends the specified content to the specified file, optionally adding a new line after the content.
        /// </summary>
        /// <param name="fileName">The name of the file to save to.</param>
        /// <param name="content">The content to append to the file.</param>
        /// <param name="toNewLine">Specifies whether to add a new line after the content. Default is true.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="InvalidOperationException">Thrown when an error occurs while saving to the file.</exception>
        Task SaveToFileAsync(string fileName, string content, bool toNewLine = true);

        /// <summary>
        /// Retrieves a specified number of lines from the beginning of the specified file.
        /// If the count parameter is null or zero, retrieves the entire file content.
        /// </summary>
        /// <param name="fileName">The name of the file to read.</param>
        /// <param name="count">The number of lines to retrieve. Default is null, which retrieves all lines.</param>
        /// <returns>A list of strings containing the retrieved lines.</returns>
        /// <exception cref="ArgumentException">Thrown when the count parameter is less than zero.</exception>
        /// <exception cref="NullReferenceException">Thrown when the specified file does not exist.</exception>
        /// <exception cref="InvalidOperationException">Thrown when an error occurs while reading the file.</exception>
        Task<IList<string>> GetFileContent(string fileName, int? count);

        /// <summary>
        /// Counts the number of lines in the specified file.
        /// </summary>
        /// <param name="fileName">The name of the file to count lines in.</param>
        /// <returns>The total number of lines in the file.</returns>
        /// <exception cref="NullReferenceException">Thrown when the specified file does not exist.</exception>
        /// <exception cref="InvalidOperationException">Thrown when an error occurs while counting lines in the file.</exception>
        Task<int> GetLineCount(string fileName);

        /// <summary>
        /// Deletes a specified number of lines from the beginning of the file. If the count parameter is null, deletes all lines in the file.
        /// </summary>
        /// <param name="fileName">The name of the file to modify.</param>
        /// <param name="count">The number of lines to delete. If null, all lines are deleted.</param>
        /// <returns>The number of lines removed from the file.</returns>
        /// <exception cref="NullReferenceException">Thrown when the specified file does not exist.</exception>
        /// <exception cref="InvalidOperationException">Thrown when an error occurs while modifying the file.</exception>
        Task<int> DeleteLines(string fileName, int? count);
        Task SaveToFileAsync(string fileName, IEnumerable<string> content);
    }
}
