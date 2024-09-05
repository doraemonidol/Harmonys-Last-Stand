using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[ExecuteAlways] // Ensures this runs in the editor as well
public class ButtonHoverHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private TextMeshProUGUI buttonText; // The text component on the button

    [SerializeField]
    private GameObject leftImage; // The left image object

    [SerializeField]
    private GameObject rightImage; // The right image object

    private RectTransform textRectTransform; 

    void Start()
    {
        textRectTransform = buttonText.GetComponent<RectTransform>();
        Debug.Log("Text: " + buttonText.text);
        // UpdateImagePositions();
    }

    public void SetText(string newText)
    {
        buttonText.text = newText;
        // UpdateImagePositions();
    }

    private void UpdateImagePositions()
    {
        if (textRectTransform == null)
        {
            textRectTransform = buttonText.GetComponent<RectTransform>();
        }

        // Use LayoutUtility to get the preferred width of the text
        float textWidth = LayoutUtility.GetPreferredWidth(textRectTransform);

        // Set positions for left and right images
        RectTransform leftImageRectTransform = leftImage.GetComponent<RectTransform>();
        RectTransform rightImageRectTransform = rightImage.GetComponent<RectTransform>();

        leftImageRectTransform.anchoredPosition = new Vector2(-textWidth / 2 - 100, 0);
        rightImageRectTransform.anchoredPosition = new Vector2(textWidth / 2 + 100, 0);

        leftImage.SetActive(false);
        rightImage.SetActive(false);
        Debug.Log("Text width: " + textWidth);
    }

    private void OnValidate()
    {
        if (buttonText != null)
        {
            textRectTransform = buttonText.GetComponent<RectTransform>();
            // UpdateImagePositions();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (leftImage != null)
            leftImage.SetActive(true);
        if (rightImage != null)
            rightImage.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (leftImage != null)
            leftImage.SetActive(false);
        if (rightImage != null)
            rightImage.SetActive(false);
    }
}
