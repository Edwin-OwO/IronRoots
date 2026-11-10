using System.Collections.Generic;
using Enemigos;
using UnityEngine;

namespace Torretas
{
    public interface ITurretStrategy
    {
        void Shoot(Turret turret, Enemy objective);

        void OnTargetAcquired(Turret turret, Enemy objective);

        public void OnShotFired(Turret turret, Enemy objective)
        {
            turret.RequestShotSound();
        }

        public void OnUpgraded(Turret turret, int newLevel)
        {
            turret.RequestUpgradeSound();
        }

        public void OnUpgradeFailed(Turret turret)
        {
            turret.RequestUpgradeFailedSound();
        }


        Enemy SelectTarget(IReadOnlyList<Enemy> enemies, Vector2 turretPosition);
    }
}