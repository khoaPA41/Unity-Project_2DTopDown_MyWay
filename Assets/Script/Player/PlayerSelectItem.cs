using UnityEngine;

public class PlayerSelectItem : MonoBehaviour
{
    [SerializeField] InputReader input;
    [SerializeField] PlayerStateMachine playerStateMachine;


    public int currentIndex = -1;
    int prevIndex = -1;
    void OnEnable()
    {
        input.SelectItemAction += SelectItem;
    }

    void OnDisable()
    {
        input.SelectItemAction -= SelectItem;
    }

    public void SelectItem(int selectIndex)
    {
        if (currentIndex == selectIndex) return;

        prevIndex = currentIndex;
        currentIndex = selectIndex;

        UIManagers.Instance.ActiveSelectUIHotbar(prevIndex, currentIndex);

        if (Inventories.Instance.inventoriesList[currentIndex]?.itemData != null)
        {
            playerStateMachine.toolType = Inventories.Instance.inventoriesList[currentIndex].itemData.type;
            playerStateMachine.toolName = Inventories.Instance.inventoriesList[currentIndex].itemData.itemName;
        }
    }
}
