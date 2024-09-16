using System;
using Common;
using Presentation.Manager;
using UnityEngine;
using UnityEngine.UI;

namespace Presentation.UI
{
    public class WeaponAssignerUI : MonoBehaviour
    {
        [SerializeField] private Text weapon1Text;
        [SerializeField] private Text weapon2Text;

        public void Update()
        {
            weapon1Text.text = EntityType.GetEntityName(DataManager.Instance.LoadData().Weapon1);
            weapon2Text.text = EntityType.GetEntityName(DataManager.Instance.LoadData().Weapon2);
        }
    }
}