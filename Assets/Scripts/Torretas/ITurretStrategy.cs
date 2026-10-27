using Enemigos;
namespace Torretas
{
    public interface ITurretStrategy
    {
        void Shoot(Turret turret, Enemy objective);
        void OnTargetAcquired(Turret turret, Enemy objective);
        void OnShotFired(Turret turret, Enemy objective);
        void OnUpgraded(Turret turret, int newLevel);
        void OnUpgradeFailed(Turret turret);
    }
}