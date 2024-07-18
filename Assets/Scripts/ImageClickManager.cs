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

    public void Start()
    {
        InitializeImageClickHandlers();
    }

    private void InitializeImageClickHandlers()
    {
        for (int i = 0; i < items.Count; i++)
        {
            GameObject newImageClickHandler = Instantiate(imageClickHandlerPrefab, parentTransform);
            ImageClickHandler handler = newImageClickHandler.GetComponent<ImageClickHandler>();
            handler.SetItem(items[i]);
            handler.InitializeDisplayer(displayName, detail, money, displayImage);
        }
    }
}