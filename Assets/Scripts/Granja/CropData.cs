using UnityEngine;

[CreateAssetMenu(fileName = "CropData", menuName = "Scriptable Objects/CropData")]
public class CropData : ScriptableObject
{
    [SerializeField] private int moneyPerCycle = 2;
    [SerializeField] private float timePerCycle = 5f;
    
    public int MoneyPerCycle => moneyPerCycle;
    public float TimePerCycle => timePerCycle;
}
