namespace FactApp.Application.Commands
{
    public class NewFactCommand
    {
        public string FactContent { get; set; } = null!;
        public override string ToString()
        {
            return $"{FactContent}";
        }
    }
}
