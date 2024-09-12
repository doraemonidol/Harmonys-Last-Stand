using System;
using Presentation.Manager;
using UnityEngine;

namespace Presentation.Maestro
{
    public class GraveInfo : MonoBehaviour
    {
        public int Index;
        
        public void Initialize(int index)
        {
            Index = index;
        }

        private void OnMouseDown()
        {
            Debug.Log("Grave " + Index + " is clicked");
            GraveManager.Instance.SetFound(Index);
        }
    }
}