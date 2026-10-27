using System.Collections.Generic;
using Granja;
using UnityEngine;

[CreateAssetMenu(fileName = "New Crop", menuName = "Scriptable Objects/CropDataSO")]
public class CropDataSO : ScriptableObject
{
    [SerializeField] private CropBase cropPrefab;
    [SerializeField] private float cost;
    [SerializeField] private string cropName;
    [SerializeField] private Sprite cropIcon;
    [SerializeField] private List<CropDataSO> aviableUpgrades;
    
    public CropBase Prefab => cropPrefab;
    public float Cost => cost;
    public string CropName => cropName;
    public Sprite CropIcon => cropIcon;
    public List<CropDataSO> Upgrades => aviableUpgrades;
}
