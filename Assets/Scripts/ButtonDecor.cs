using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[ExecuteAlways] // Ensures this runs in the editor as well
public class ButtonHoverHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private TextMeshProUGUI buttonText;
    [SerializeField]
    private GameObject leftImage;
    [SerializeField]
    private GameObject rightImage;
    private RectTransform textRectTransform; 

    void Start()
    {
        textRectTransform = buttonText.GetComponent<RectTransform>();
        Debug.Log("Text: " + buttonText.text);
        UpdateImagePositions();
    }

    public void SetText(string newText)
    {
        buttonText.text = newText;
        UpdateImagePositions();
    }

    private void UpdateImagePositions()
    {
        if (textRectTransform == null)
        {
            textRectTransform = buttonText.GetComponent<RectTransform>();
        }

        float textWidth = LayoutUtility.GetPreferredWidth(textRectTransform);

        // Set positions for left and right images
        RectTransform leftImageRectTransform = leftImage.GetComponent<RectTransform>();
        RectTransform rightImageRectTransform = rightImage.GetComponent<RectTransform>();

        leftImageRectTransform.anchoredPosition = new Vector2(-textWidth / 2 - 100, 0);
        rightImageRectTransform.anchoredPosition = new Vector2(textWidth / 2 + 100, 0);

        Debug.Log("Text width: " + textWidth);
    }

    private void OnValidate()
    {
        if (buttonText != null)
        {
            textRectTransform = buttonText.GetComponent<RectTransform>();
            UpdateImagePositions();
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