
namespace FactApp.Domain.Interfaces.Services
{
    public interface IFileService
    {
        Task SaveToFileAsync(string fileName, string content);
    }
}
