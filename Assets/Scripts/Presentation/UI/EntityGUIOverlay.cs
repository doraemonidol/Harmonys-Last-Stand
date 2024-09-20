using System;
using UnityEngine;

namespace Presentation.UI
{
    public class EntityGUIOverlay : MonoBehaviour
    {
        private GameObject _parent;

        private void Start()
        {
            _parent = this.transform.parent.gameObject;
        }
        
        private void Update()
        {
            this.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, -_parent.transform.rotation.y, 0);
        }
    }
}