using Common;
using UnityEngine;
using UnityEngine.UI;

public class ImageClickHandler : MonoBehaviour
{
    [SerializeField]
    private Image displayImage; // The image component where the clicked image will be displayed
    [SerializeField]
    private sss imageType; // The type of image that will be displayed

    // This method will be called when an image slot is clicked
    public void OnImageClick(Image clickedImage)
    {
        displayImage.sprite = clickedImage.sprite;
    }

    public void OnClickEvent()
    {
        Debug.Log("Image type: " + imageType);
    }
}