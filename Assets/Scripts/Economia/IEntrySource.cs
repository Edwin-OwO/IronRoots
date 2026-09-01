namespace Economia
{
    public interface IEntrySource
    {
        event System.Action<int> OnEntryGenerated;
    }
}