using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WordWithImagesSetup : MonoBehaviour
{
    public TextMeshProUGUI wordText; // Reference to the Text component
    public Image leftImage; // Reference to the left Image component
    public Image rightImage; // Reference to the right Image component
    public float spacing = 100f; // Distance from the word to the images
    private RectTransform textRectTransform; 

    private void Start()
    {
       textRectTransform = wordText.GetComponent<RectTransform>();
       Debug.Log("Title: " + wordText.text);
       UpdateImagePositions();
    }

    private void UpdateImagePositions()
    {
        // if (wordText == null || leftImage == null || rightImage == null)
        //     {
        //         Debug.LogError("Please assign the WordText, LeftImage, and RightImage in the Inspector.");
        //         return;
        //     }

        //     // Position the left image
        //     RectTransform leftRectTransform = leftImage.GetComponent<RectTransform>();
        //     leftRectTransform.anchoredPosition = new Vector2(-spacing, 0);

        //     // Position the right image
        //     RectTransform rightRectTransform = rightImage.GetComponent<RectTransform>();
        //     rightRectTransform.anchoredPosition = new Vector2(spacing, 0);
        // }

        if(textRectTransform == null)
        {
            textRectTransform = wordText.GetComponent<RectTransform>();
        }

        float textWidth = LayoutUtility.GetPreferredWidth(textRectTransform);

        RectTransform leftImageRectTransform = leftImage.GetComponent<RectTransform>();
        RectTransform rightImageRectTransform = rightImage.GetComponent<RectTransform>();

        // leftImageRectTransform.anchoredPosition = new Vector2(-textWidth / 2 - 100, 0);
        // rightImageRectTransform.anchoredPosition = new Vector2(textWidth / 2 + 100, 0);

        Debug.Log("Text width: " + textWidth);
    }
}
