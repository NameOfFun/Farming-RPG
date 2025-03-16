using System;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance {  get; private set; }
    [Header("Status Bar")]
    // Tool equip slot on the status bar
    public Image toolEquipSlot;

    [Header("Inventory System")]
    // the inventory Panel
    public GameObject inventoryPanel;

    // The tool equip slot UI on the inventory panel
    public HandInventorySlot toolHandSlot;
    // the tool slot Uis
    public InventorySlot[] toolSlots;

    // The item equip slot UI on the inventory panel
    public HandInventorySlot itemHandSlot;
    // the item slot Uis
    public InventorySlot[] itemSlots;

    [Header("Inventory Info")]
    // item info box
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
        AssignSlotIndex();
    }

    public void AssignSlotIndex()
    {
        for (int i = 0; i < toolSlots.Length; i++)
        {
            toolSlots[i].AsignIndex(i);
            itemSlots[i].AsignIndex(i);
        }
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

        // Render the equip slots
        itemHandSlot.Display(InventoryManager.Instance.equippedItem);
        toolHandSlot.Display(InventoryManager.Instance.equippedTool);
        
        // Get Tool Equip from inventory Manager
        ItemData equippedTool = InventoryManager.Instance.equippedTool;

        // Check if there is an item to display
        if (equippedTool != null)
        {
            // Switch the thumbnail over
            toolEquipSlot.sprite = equippedTool.thumbnails;

            toolEquipSlot.gameObject.SetActive(true);
            return;
        }
        toolEquipSlot.gameObject.SetActive(false);
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