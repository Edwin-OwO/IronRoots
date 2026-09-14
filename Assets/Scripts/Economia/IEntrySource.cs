namespace Economia
{
    public interface IEntrySource
    {
        event System.Action<float> OnEntryGenerated;
    }
}