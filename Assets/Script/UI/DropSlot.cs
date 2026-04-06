using UnityEngine;
using UnityEngine.EventSystems;

public class DropSlot : MonoBehaviour, IDropHandler
{
    DraggableItem thisItem;

    void Start()
    {
        thisItem = GetComponent<DraggableItem>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            DraggableItem draggableItem = eventData.pointerDrag.GetComponent<DraggableItem>();
            if (draggableItem != null && draggableItem != thisItem)
            {

                Inventories.Instance.SwapItem(draggableItem, thisItem);
            }
        }
    }
}
