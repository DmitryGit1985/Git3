using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ImageEnter : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    private string imageName;
    private bool imageEntered;
    public string ImageName { get => imageName; }
    public bool ImageEntered {get => imageEntered;}
    public void OnPointerEnter(PointerEventData eventData)
    {
        imageEntered = true;
        imageName = eventData.pointerEnter.gameObject.name;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        imageEntered = false;
    }
}
