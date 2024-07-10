using Common;
using UnityEngine;

namespace Presentation.GUI
{
    public class BuyItem : MonoBehaviour
    {
        [SerializeField] public ItemType itemType;

        public void Buy()
        {
            Debug.Log("Buying item of type: " + itemType);
        }
    }
}