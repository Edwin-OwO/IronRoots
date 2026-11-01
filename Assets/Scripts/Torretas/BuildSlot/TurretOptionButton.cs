using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Torretas
{
    public class TurretOptionButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private TMP_Text turretName;

        public void Setup(TurretSO turretData, Action onClickAction)
        {
            turretName.text = turretData.name;

            button.onClick.AddListener(
                () => onClickAction?.Invoke()
            );
        }
    }
}