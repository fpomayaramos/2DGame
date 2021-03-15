using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/**
 * UI Inventory
 */
public class UI_Inventory : MonoBehaviour
{
    private Inventory inventory;
    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;
    private Player player;

    /**
     * This function Awake runs as soon as the gameScene is loaded
     */
    private void Awake()
    {
        itemSlotContainer = transform.Find("itemSlotContainer");
        itemSlotTemplate = itemSlotContainer.Find("itemSlotTemplate");
    }

    public void SetPlayer (Player player)
    {
        this.player = player;
    }

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;

        // Subscribers listener
        inventory.OnItemListChanged += Inventory_OnItemListChanged;

        RefreshInventoryItems();
    }

    private void Inventory_OnItemListChanged(object sender, System.EventArgs e)
    {
        RefreshInventoryItems();
    }

    /**
     * Inventory refreshes when an item is picked up
     * Inventory refreshes when an item is dropped
     * Inventory refreshes when an item is used/consumed
     */
    private void RefreshInventoryItems()
    {
        // Space between each item (in inventory)
        int x = 0;
        int y = 0;
        float itemSlotCellSize = 83f;

        if (itemSlotContainer == null)
        {
            itemSlotContainer = transform.Find("itemSlotContainer");
        }
        if (itemSlotTemplate == null)
        {
            itemSlotTemplate = itemSlotContainer.Find("itemSlotTemplate");
        }

        foreach (Transform child in itemSlotContainer)
        {
            if (child == itemSlotTemplate) continue;
            Destroy(child.gameObject);
        }

        // Create an item "copy" from template for each item inside the player's inventory
        foreach (Item item in inventory.GetItemList())
        {
            // Create an item "copy" and display it
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);

            // When Mouse Left Click on item in inventory
            itemSlotRectTransform.GetComponent<Button_UI>().ClickFunc = () =>
            {
                // Use item in inventory
                inventory.UseItem(item);
            };
            // When Mouse Right Click on item in inventory
            itemSlotRectTransform.GetComponent<Button_UI>().MouseRightClickFunc = () => 
            {
                // Drop item from inventory
                // Create a copy so that the item amount is not affected
                Item duplicateItem = new Item { itemType = item.itemType, amount = item.amount };
                inventory.RemoveItem(item);
                ItemWorld.DropItem(player.GetPosition(), duplicateItem);
            };

            // Space between each item (in inventory)
            itemSlotRectTransform.anchoredPosition = new Vector2((x * itemSlotCellSize) - 83, (y * itemSlotCellSize) + 40);

            // Get sprite for each item
            Image image = itemSlotRectTransform.Find("image").GetComponent<Image>();
            image.sprite = item.GetSprite();

            // Get amount for each item (if stackle)
            TextMeshProUGUI uiText = itemSlotRectTransform.Find("amountText").GetComponent<TextMeshProUGUI>();
            // If the amount is greater than 1, then display it along with the item
            if (item.amount > 1)
            {
                uiText.SetText(item.amount.ToString());
            }
            else
            {
                uiText.SetText("");
            }

            // Inventory column
            x++;

            // Inventory row
            if (x > 2)
            {
                x = 0;
                y--;
            }
        }
    }
}
