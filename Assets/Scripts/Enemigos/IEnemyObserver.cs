namespace Enemigos
{
    public interface IEnemyObserver
    {
        void OnDie(Enemy enemy);
        void OnFinalStep(Enemy enemy);
    }
}