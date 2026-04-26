using System;
using System.Collections.Generic;
using UnityEngine;
public class Inventories : MonoBehaviour
{
    public static Inventories Instance;

    [Header("Starter Set")]
    [SerializeField] List<ValueItem> starterList;

    [SerializeField] UIManagers uIManagers;

    public int bagSize = 55;

    public List<InventorySlot> inventoriesList;

    public InventorySlot mouseSlot = new InventorySlot();

    public event Action UpdateUIAction;

    public Queue<int> indexItemNeeded = new Queue<int>();
    //public int indexItemNeeded = -1;

    public bool isCanCraft = true;
    void Awake()
    {
        Instance = this;
        inventoriesList = new List<InventorySlot>();
        Setup();
    }

    void Start()
    {

    }

    void Setup()
    {
        for (int i = 0; i < bagSize; i++)
        {
            InventorySlot newSlot = new InventorySlot();
            if (starterList.Count > 0 && i < starterList.Count)
            {
                newSlot.itemData = starterList[i];
                newSlot.stack = 1;
            }

            inventoriesList.Add(newSlot);
        }
    }

    public void SubtractItem(int index, int amount)
    {
        int currentQuantity = inventoriesList[index].stack - amount;
        if (currentQuantity <= 0)
        {
            inventoriesList[index].itemData = null;
        }

        inventoriesList[index].stack = currentQuantity;

        UpdateUIAction?.Invoke();
    }

    public void AddItem(ValueItem newItem, int amount)
    {
        if (inventoriesList.Count == 0) // new item => Add
        {
            Setup();
        }

        foreach (InventorySlot slot in inventoriesList)
        {
            if (slot.itemData == newItem) // If find exits item data
            {
                if (slot.stack < newItem.maxStack) // If this slot has not enough stack 
                {
                    int temp = slot.stack + amount; // temp hold the sum amount
                    if (temp > newItem.maxStack) // If temp greater than max stack
                    {
                        slot.stack += amount - (temp - newItem.maxStack); // Add the enough amount to the current stack
                        amount = temp - newItem.maxStack;                 // Add the residual to the amount
                        break;
                    }
                    slot.stack = temp;                                   // Add mount to stack
                    amount = 0;                                          // reset amount because added succesfully
                    break;
                }
            }
        }

        if (amount > 0) // If amount still has value => add the next temp slot
        {
            foreach (InventorySlot slot in inventoriesList)
            {
                if (slot.itemData == null)
                {
                    slot.itemData = newItem;
                    slot.stack = amount;
                    break;
                }
            }
        }
        UpdateUIAction?.Invoke();
        return;
    }

    public void SwapItem(DraggableItem drag, DraggableItem drop)
    {
        mouseSlot = inventoriesList[drag.boxIndex];

        inventoriesList[drag.boxIndex] = inventoriesList[drop.boxIndex];
        inventoriesList[drop.boxIndex] = mouseSlot;
        UpdateUIAction?.Invoke();
    }

    public bool HasEnoughItem(CrafIngredient itemCheck) // Find the item
    {
        //indexItemNeeded = -1;
        isCanCraft = true;
        for (int i = 0; i < inventoriesList.Count; i++)
        {
            if (inventoriesList[i].itemData == itemCheck.item) // if same name
            {
                if (inventoriesList[i].stack - itemCheck.amount >= 0) // if enough quantity
                {
                    indexItemNeeded.Enqueue(i);
                    return true;
                }
            }
        }
        isCanCraft = false;
        return false;
    }

    public void SubtractItemAfterCraft(CrafIngredient[] itemForCraft)
    {
        foreach (var item in itemForCraft)
        {
            SubtractItem(indexItemNeeded.Dequeue(), item.amount);
        }

    }
}