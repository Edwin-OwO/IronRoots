namespace Granja
{
    public interface ICrop
    {
        int MoneyPerCycle { get; }
        void StartCycle();
        void Harvest();
    }
}