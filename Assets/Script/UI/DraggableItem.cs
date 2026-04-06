using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [HideInInspector] public Transform originalParent;
    Image image;

    public ValueItem valueItem;
    public int amount;

    public int boxIndex;

    void Start()
    {
        image = GetComponent<Image>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        MouseItemController.Instance.ShowGhostItem(image.sprite);


        image.color = new Color(1f, 1f, 1f, 0.5f);
    }

    public void OnDrag(PointerEventData eventData)
    {

    }


    public void OnEndDrag(PointerEventData eventData)
    {
        MouseItemController.Instance.HideGhostItem();
        image.color = new Color(1f, 1f, 1f, 1f);
    }
}