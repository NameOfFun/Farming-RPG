using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
        }
        else
        {
            Instance = this;
        }
    }
    // Tool slots
    public ItemData[] tools = new ItemData[8];
    // Tool in the playe's hand
    public ItemData equippedTool = null;

    // Item slots
    public ItemData[] items = new ItemData[8];
    // Item in the playe's hand
    public ItemData equippedItem = null;

    // Handles movement of item from Inventory to hand
    public void InventoryToHand(int slotIndex, InventorySlot.InventoryType inventoryType)
    {
        if (inventoryType == InventorySlot.InventoryType.Item)
        {
            // Cache the inventory slot ItemData from InventoryManager
            ItemData itemToEqup = items[slotIndex];

            // Change the Inventory slot to Hand's 
            items[slotIndex] = equippedItem;

            // Change the hand's slot to the onventory slot's
            equippedItem = itemToEqup;
        }
        else
        {
            // Cache the inventory slot ItemData from InventoryManager
            ItemData toolToEqup = tools[slotIndex];

            // Change the Inventory slot to Hand's 
            tools[slotIndex] = equippedTool;

            // Change the hand's slot to the onventory slot's
            equippedTool = toolToEqup;
        }
        // Update the changes to the UI
        UIManager.Instance.RenderInventory();
    }

    // Handles movement of the item from hand to inventory
    public void HandToInventory(InventorySlot.InventoryType inventoryType)
    {
        if (inventoryType == InventorySlot.InventoryType.Item)
        {
            // Iterate through each inventory slot and find an empty slot
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i] == null)
                {
                    // Send the equipped item over to its new slot
                    items[i] = equippedItem;
                    // Remove the item from the hand
                    equippedItem = null;
                    break;
                }                
            }
        }
        else
        {
            // Iterate through each inventory slot and find an empty slot
            for (int i = 0; i < tools.Length; i++)
            {
                if (tools[i] == null)
                {
                    // Send the equipped tool over to its new slot
                    tools[i] = equippedTool;
                    // Remove the tool from the hand
                    equippedTool = null;
                    break;
                }
                
            }
        }

        //Update changes in the inventory
        UIManager.Instance.RenderInventory();
    }
}
