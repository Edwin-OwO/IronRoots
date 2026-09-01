using System.Collections.Generic;
using UnityEngine;
using Granja;

namespace Nucleo
{
    public class FarmBase : MonoBehaviour
    {
        [SerializeField] private int health = 100;
        [SerializeField] private List<FarmSlot> farmSlots;

       public int Health => health;

       public void TakeDamage(int amount)
      {
         health -= amount;

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

