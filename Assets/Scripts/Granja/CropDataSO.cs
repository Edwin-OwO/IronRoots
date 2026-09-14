using Granja;
using UnityEngine;

[CreateAssetMenu(fileName = "New Crop", menuName = "Scriptable Objects/CropData")]
public class CropDataSO : ScriptableObject
{
    [SerializeField] private CropBase cropPrefab;
    [SerializeField] private float cost;
    
    public CropBase Prefab => cropPrefab;
    public float Cost => cost;
}
