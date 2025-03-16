using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    ItemData itemToDisplay;

    public Image itemDisplayImage;

    public enum InventoryType
    {
        Item, Tool
    }
    // Determines which inventory sectin this slot is aprart of slot
    public InventoryType inventoryType;

    int slotIndex;
    public void Display(ItemData itemToDisplay)
    {
        // Check if there is an item to display
        if (itemToDisplay != null)
        {
            // Switch the thumbnail over
            itemDisplayImage.sprite = itemToDisplay.thumbnails;
            this.itemToDisplay = itemToDisplay;

            itemDisplayImage.gameObject.SetActive(true);
            return;
        }
        itemDisplayImage.gameObject.SetActive(false);
    }

    public void AsignIndex(int slotIndex)
    {
        this.slotIndex = slotIndex;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        UIManager.Instance.DisplayItemInfo(itemToDisplay);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIManager.Instance.DisplayItemInfo(null);
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        // movement item from inventory to hand slot 
        InventoryManager.Instance.InventoryToHand(slotIndex, inventoryType);
    }
}
