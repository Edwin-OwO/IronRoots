using System;
using System.Collections.Generic;
using UnityEngine;
using Granja;
using Random = UnityEngine.Random;

namespace Nucleo
{
    public class FarmBase : MonoBehaviour
    {
        public static event  Action OnBaseDestroyed;
        public static event Action<int> OnEnemieEnter;
       
        public static FarmBase Instance { get; private set; }
        
        [SerializeField] private int health = 20;
        [SerializeField] private List<FarmSlot> farmSlots;
        
        public int Health => health;
        
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }
        
       public void TakeDamage()
      {
         health  -= 1;
         OnEnemieEnter?.Invoke(health);
         
            List<FarmSlot> slotsTaked = farmSlots.FindAll(slot => slot.Taked);
           if (slotsTaked.Count > 0)
           {
               FarmSlot chosed = slotsTaked[Random.Range(0, slotsTaked.Count)];
              chosed.DestroyCrop();
           }
          
           if (health <= 0) 
           {
             Debug.Log("Game Over: la base fue destruida."); 
           }
      }
    }
}

