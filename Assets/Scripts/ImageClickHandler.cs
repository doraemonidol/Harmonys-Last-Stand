using UnityEngine;
using UnityEngine.UI;

public class ImageClickHandler : MonoBehaviour
{
    [SerializeField]
    public Image displayImage; // The image component where the clicked image will be displayed

    public Item item; // The item to be handled

    // This method will be called when an image slot is clicked
    public void OnImageClick()
    {
        displayImage.sprite = item.image;
        Debug.Log("Item Details: " + item.detail);
        Debug.Log("Item Money: " + item.money);
        Debug.Log("Item Type: " + item.itemType);
    }
}