using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHoverHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private GameObject leftImage;
    [SerializeField]
    private GameObject rightImage;

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