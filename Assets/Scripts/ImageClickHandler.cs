using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ImageClickHandler : MonoBehaviour
{
    [SerializeField]
    public Image displayImage; // The image component where the clicked image will be displayed
    public TextMeshProUGUI displayName; 
    public TextMeshProUGUI detail;
    public TextMeshProUGUI money;
    [SerializeField]

    private Item item; // The item to be handled
    private Image childImage;

    public void SetItem(Item item)
    {
        this.item = item;
        gameObject.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().sprite = item.image;
    }

    public void InitializeDisplayer(TextMeshProUGUI displayName, TextMeshProUGUI detail, TextMeshProUGUI money, Image displayImage)
    {
        this.displayName = displayName;
        this.detail = detail;
        this.money = money;
        this.displayImage = displayImage;
    }

    private void Start()
    {
        // Find the child image (assumes there's only one child with an Image component)
        childImage = GetComponentInChildren<Image>(true); // 'true' to include inactive objects
    }

    // This method will be called when an image slot is clicked
    public void OnImageClick()
    {
        this.transform.parent.GetComponent<ImageClickManager>().SetCurrentItem(item);
        displayImage.enabled = true;
        displayImage.sprite = item.image;
        displayName.text = item.name; 
        detail.text = item.detail;
        money.text = item.money.ToString();
        // if (childImage != null)
        // {
        //     childImage.sprite = item.image;
        //     childImage.gameObject.SetActive(true);
        // }
        Debug.Log("Item Details: " + item.detail);
        Debug.Log("Item Money: " + item.money);
        Debug.Log("Item Type: " + item.itemType);
    }
}