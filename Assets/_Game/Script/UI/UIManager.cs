using System;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance {  get; private set; }

    [Header("Inventory System")]

    public GameObject inventoryPanel;
    public InventorySlot[] toolSlots;
    public InventorySlot[] itemSlots;

    [Header("Inventory Info")]
    public Text ItemName;
    public Text Description;
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(Instance);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        RenderInventory();
    }

    // Render the inventory screen to reflect the Player's Inventory
    public void RenderInventory()
    {
        // Get the inventory tool slots from Inventory Manager
        ItemData[] inventoryToolSlots = InventoryManager.Instance.tools;

        // Get the inventory item slots from Inventory Manager
        ItemData[] inventoryItemSlots = InventoryManager.Instance.items;

        // Render the tool section
        RenderInventoryPanel(inventoryToolSlots, toolSlots);

        // Render the item section
        RenderInventoryPanel(inventoryItemSlots, itemSlots);
    }

    // Iterate through a slot in a section and display them in the Ui
    private void RenderInventoryPanel(ItemData[] slots, InventorySlot[] uiSlots)
    {
        for(int i = 0; i < uiSlots.Length; i++)
        {
            // Display them accordingly
            uiSlots[i].Display(slots[i]);
        }
    }

    public void ToggleInventoryPanel()
    {
        // If the panel is hidden, shown it and vice versa
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);

        RenderInventory();
    }

    public void DisplayItemInfo(ItemData item)
    {
        if(item == null)
        {
            ItemName.text = "";
            Description.text = "";
            return; 
        }
        ItemName.text = item.name;
        Description.text = item.descrtiption;
    }
}