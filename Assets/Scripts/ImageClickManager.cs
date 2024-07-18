#if UNITY_EDITOR
using System.Collections.Generic;
using TMPro;
using UnityEditor;
#endif
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
    public TextMeshProUGUI displayName; 
    public TextMeshProUGUI detail;
    public TextMeshProUGUI money;
    [SerializeField]
    private List<Item> items;

    private void Start()
    {
        InitializeItems();
        InitializeImageClickHandlers();
    }

    private void InitializeItems()
    {
        //  for (int i = 0; i < 12; i++)
        // {
        //     // Sprite itemSprite = null;

        //     // #if UNITY_EDITOR
        //     // // Load the sprite from the Assets/Itemlist folder using AssetDatabase
        //     // string path = $"Assets/Sprites/Itemlist/Item1.png";
        //     // itemSprite = AssetDatabase.LoadAssetAtPath<Sprite>(path);
        //     // Debug.Log("Path: " + path);
        //     // #endif

        //     // if (itemSprite == null)
        //     // {
        //     //     Debug.LogWarning($"Assets/Itemlist/Item{i + 1}.png not found in Assets folder");
        //     //     continue;
        //     // }

        //     // Initialize each item with dummy data for demonstration purposes
        //     items.Add(new Item(
        //         Random.Range(0, 100), // Random money amount
        //         "Detail for item " + i, // Item detail
        //         itemSprites[i], // Assign your sprite here
        //         (ItemType)Random.Range(0, System.Enum.GetValues(typeof(ItemType)).Length) // Random item type
        //     ));
        // }
    }

    private void InitializeImageClickHandlers()
    {
        for (int i = 0; i < items.Count; i++)
        {
            GameObject newImageClickHandler = Instantiate(imageClickHandlerPrefab, parentTransform);
            ImageClickHandler handler = newImageClickHandler.GetComponent<ImageClickHandler>();
            handler.SetItem(items[i]);
            handler.displayImage = displayImage; // Assign the display image
            handler.displayName.text = items[i].name;

        }
    }
}