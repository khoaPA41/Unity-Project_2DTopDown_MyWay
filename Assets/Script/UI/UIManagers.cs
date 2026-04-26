using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManagers : MonoBehaviour
{
    public static UIManagers Instance;

    [Header("System UI Object")]
    [SerializeField] GameObject system;

    [Header("Inventory Script")]
    [SerializeField] Inventories inventories;


    [Header("Scroll View Panel")]
    [SerializeField] GameObject content;

    [Header("Panel Quantity Text")]
    [SerializeField] TextMeshProUGUI panelQuantityText;

    [Header("Box List UI")]
    [SerializeField] List<GameObject> box;

    [Header("Panel Box List")]
    [SerializeField] List<GameObject> panelBoxList;

    [Header("Hotbar UI")]
    [SerializeField] List<GameObject> hotbarObject;

    [Header("Formula Scriptable Object")]
    [SerializeField] List<RecipeData> recipeDatas;

    [Header("Craft UI")]
    [SerializeField] List<GameObject> panelformulaList; // Book Of Formula panel

    [SerializeField] List<GameObject> fomulaBoxList;   // Book Of Formula box

    [SerializeField] List<GameObject> craftBox;       // Craft Box

    [Header("Sprite For Empty Box")]
    [SerializeField] Sprite emptySlot;


    [Header("Input")]
    [SerializeField] InputReader input;
    int currentPanelIndex = 0;

    int previewProductIndex = -1;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Setup();
        UpdateInventoryUI();
        UpdateFormulaBook();
        inventories.UpdateUIAction += UpdateInventoryUI;
        //system.SetActive(false);
        input.OpenUIAction += ActiveUISystem;
    }

    public void ActiveUISystem()
    {
        if (system.activeInHierarchy)
        {
            system.SetActive(false);
        }
        else
        {
            system.SetActive(true);
        }
    }

    public void OffTheSystem()
    {
        system.SetActive(false);
    }

    void Setup()
    {
        for (int j = 0; j < panelBoxList.Count; j++) // Active false all the panels
        {
            panelBoxList[j].SetActive(false);
        }


        for (int i = 0; i < inventories.inventoriesList.Count; i++)
        {
            GameObject boxObject = box[i];
            Image newBoxImage = boxObject.transform.GetChild(0).GetComponent<Image>();
            DraggableItem draggableItem = newBoxImage.gameObject.GetComponent<DraggableItem>();
            draggableItem.boxIndex = i;
        }

        foreach (var selectUI in hotbarObject)
        {
            selectUI.SetActive(false);
        }

        panelBoxList[currentPanelIndex].SetActive(true); // Active true first panel
        panelQuantityText.SetText($"{currentPanelIndex + 1}/{panelBoxList.Count}");
        ResetCraft();
    }


    public void UpdateInventoryUI()
    {

        if (inventories.inventoriesList.Count == 0) // if don't have item
        {
            Debug.Log("The inventories is null!");
            return;
        }

        for (int i = 0; i < inventories.inventoriesList.Count; i++)
        {
            if (inventories.inventoriesList[i].itemData != null)
            {
                GameObject boxObject = box[i];

                Image newBoxImage = boxObject.transform.GetChild(0).GetComponent<Image>();
                TextMeshProUGUI newBoxText = boxObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
                DraggableItem draggableItem = newBoxImage.gameObject.GetComponent<DraggableItem>();


                newBoxImage.sprite = inventories.inventoriesList[i].itemData.itemSprite;
                newBoxText.SetText(inventories.inventoriesList[i].stack.ToString());

                draggableItem.valueItem = inventories.inventoriesList[i].itemData;
                draggableItem.amount = inventories.inventoriesList[i].stack;
            }
            else
            {
                GameObject boxObject = box[i];
                Image newBoxImage = boxObject.transform.GetChild(0).GetComponent<Image>();
                TextMeshProUGUI newBoxText = boxObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>();

                DraggableItem draggableItem = newBoxImage.gameObject.GetComponent<DraggableItem>();

                newBoxImage.sprite = emptySlot;
                newBoxText.SetText("0");

                draggableItem.valueItem = null;
                draggableItem.amount = 0;

            }
        }
    }


    public void ActiveSelectUIHotbar(int prevIndex, int nextIndex)
    {
        if (prevIndex > -1)
        {
            hotbarObject[prevIndex].SetActive(false);
        }

        if (nextIndex > -1)
        {
            hotbarObject[nextIndex].SetActive(true);
        }
    }

    public void UpdateFormulaBook()
    {
        for (int i = 0; i < recipeDatas.Count; i++)
        {
            if (recipeDatas[i].product.item != null)
            {
                GameObject boxObject = fomulaBoxList[i];

                Image newBoxImage = boxObject.transform.GetChild(0).GetComponent<Image>();

                newBoxImage.sprite = recipeDatas[i].product.item.itemSprite;
            }
            else
            {
                GameObject boxObject = box[i];
                Image newBoxImage = boxObject.transform.GetChild(0).GetComponent<Image>();
                newBoxImage.sprite = emptySlot;

            }
        }
    }


    void ResetCraft()
    {
        foreach (var box in craftBox)
        {
            box.transform.GetChild(0).GetComponent<Image>().sprite = emptySlot;
        }
    }

    /*BUTTON*/
    public void PreviewProduct(int index)
    {
        ResetCraft();
        previewProductIndex = index;
        foreach (var material in recipeDatas[index].itemNeeded) // Loop through an item needed array
        {
            GameObject boxObject = craftBox[material.itemPlacement];
            Image newBoxImage = boxObject.transform.GetChild(0).GetComponent<Image>();
            TextMeshProUGUI newText = boxObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            newBoxImage.sprite = material.item.itemSprite;
            string quantityText;
            if (Inventories.Instance.HasEnoughItem(material)) // if enough quantity
            {
                //quantityText = (Inventories.Instance.inventoriesList[Inventories.Instance.indexItemNeeded].stack - material.amount).ToString();
                //newText.SetText(quantityText);
                //newText.color = Color.white;
                continue;
            }
            else
            {
                //(Inventories.Instance.inventoriesList[Inventories.Instance.indexItemNeeded].stack - material.amount).ToString()
                quantityText = "Not Engouh";
                newText.SetText(quantityText);
                newText.color = Color.red;
            }


        }
        craftBox[craftBox.Count - 1].transform.GetChild(0).GetComponent<Image>().sprite = recipeDatas[index].product.item.itemSprite;
    }

    public void PrevButton()
    {
        panelBoxList[currentPanelIndex].SetActive(false);

        currentPanelIndex = Mathf.Max(currentPanelIndex - 1, 0);

        panelBoxList[currentPanelIndex].SetActive(true);

        panelQuantityText.SetText($"{currentPanelIndex + 1}/{panelBoxList.Count}");
    }

    public void NextButton()
    {
        panelBoxList[currentPanelIndex].SetActive(false);

        currentPanelIndex = Mathf.Min(currentPanelIndex + 1, panelBoxList.Count - 1);

        panelBoxList[currentPanelIndex].SetActive(true);

        panelQuantityText.SetText($"{currentPanelIndex + 1}/{panelBoxList.Count}");
    }

    public void CraftButton()
    {
        if (Inventories.Instance.isCanCraft)
        {
            Inventories.Instance.AddItem(recipeDatas[previewProductIndex].product.item, 1);
            Inventories.Instance.SubtractItemAfterCraft(recipeDatas[previewProductIndex].itemNeeded);
        }
        else
        {
            Debug.Log("Not enough material");
        }
    }
    /***************************************************************************************/
}

