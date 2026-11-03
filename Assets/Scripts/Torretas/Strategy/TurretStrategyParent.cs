using System.Collections.Generic;
using Enemigos;
using UnityEngine;

namespace Torretas
{
    public abstract class TurretStrategyParent : ScriptableObject, ITurretStrategy
    {
        public abstract void Shoot(Turret turret, Enemy objective);

        public virtual void OnTargetAcquired(Turret turret, Enemy objective)
        {
        }

        public virtual void OnShotFired(Turret turret, Enemy objective)
        {
        }

        public virtual void OnUpgraded(Turret turret, int newLevel)
        {
        }

        public virtual void OnUpgradeFailed(Turret turret)
        {
        }

        public virtual Enemy SelectTarget(IReadOnlyList<Enemy> enemies, Vector2 turretPosition)
        {
            return null;
        }
    }
}