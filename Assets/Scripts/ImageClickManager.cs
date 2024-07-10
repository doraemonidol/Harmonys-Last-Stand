using UnityEngine;
using UnityEngine.UI;

public class ImageClickManager : MonoBehaviour
{
    [SerializeField]
    private GameObject imageClickHandlerPrefab; // Prefab of the game object with ImageClickHandler script

    [SerializeField]
    private Transform parentTransform; // Parent transform to which the game objects will be added

    [SerializeField]
    private Image displayImage; // Reference to the Image component in the scene

    [SerializeField]
    private Sprite[] itemSprites; // Array of sprites to assign to items

    private Item[] items = new Item[12]; // Array of 12 items

    private void Start()
    {
        InitializeItems();
        InitializeImageClickHandlers();
    }

    private void InitializeItems()
    {
        for (int i = 0; i < items.Length; i++)
        {
            // Initialize each item with dummy data for demonstration purposes
            items[i] = new Item(
                Random.Range(0, 100), // Random money amount
                "Detail for item " + i, // Item detail
                itemSprites[i % itemSprites.Length], // Assign your sprite here
                (ItemType)Random.Range(0, System.Enum.GetValues(typeof(ItemType)).Length) // Random item type
            );
        }
    }

    private void InitializeImageClickHandlers()
    {
        for (int i = 0; i < items.Length; i++)
        {
            GameObject newImageClickHandler = Instantiate(imageClickHandlerPrefab, parentTransform);
            ImageClickHandler handler = newImageClickHandler.GetComponent<ImageClickHandler>();
            handler.item = items[i];
            handler.displayImage = displayImage; // Assign the display image
        }
    }
}