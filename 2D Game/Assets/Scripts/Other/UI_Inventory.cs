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

    private static bool UIExists;

    // Start is called before the first frame update
    void Start()
    {

        if (!UIExists)
        {
            UIExists = true;
            DontDestroyChildOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // goes up the parent chain and sets the DontDestroyOnLoad flag on the root object.
    public static void DontDestroyChildOnLoad(GameObject child)
    {
        Transform parentTransform = child.transform;

        // If this object doesn't have a parent then its the root transform.
        while (parentTransform.parent != null)
        {
            // Keep going up the chain.
            parentTransform = parentTransform.parent;
        }
        GameObject.DontDestroyOnLoad(parentTransform.gameObject);
    }

    /**
     * This function Awake runs as soon as the gameScene is loaded
     */

    
    private void Awake()
    {
        itemSlotContainer = transform.Find("itemSlotContainer");
        itemSlotTemplate = itemSlotContainer.Find("itemSlotTemplate");
        itemSlotTemplate.gameObject.SetActive(false);
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
        itemSlotContainer = transform.Find("itemSlotContainer");
        itemSlotTemplate = itemSlotContainer.Find("itemSlotTemplate");

        foreach (Transform child in itemSlotContainer)
        {
            if (child == itemSlotTemplate) continue;
            Destroy(child.gameObject);
        }

        itemSlotContainer = transform.Find("itemSlotContainer");
        itemSlotTemplate = itemSlotContainer.Find("itemSlotTemplate");

        // Space between each item (in inventory)
        int x = 0;
        int y = 0;
        float itemSlotCellSize = 100f;

        // Create an item "copy" from template for each item inside the player's inventory
        foreach (Item item in inventory.GetItemList())
        {
            // Create an item "copy" and display it
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);

            // When Mouse Left Click on item in inventory
            itemSlotRectTransform.GetComponent<Button_UI>().ClickFunc = () => {
                // Use item
                inventory.UseItem(item);
            };
            // When Mouse Right Click on item in inventory
            itemSlotRectTransform.GetComponent<Button_UI>().MouseRightClickFunc = () => {
                // Drop item
                Item duplicateItem = new Item { itemType = item.itemType, amount = item.amount };
                inventory.RemoveItem(item);
                ItemWorld.DropItem(player.GetPosition(), duplicateItem);
            };

            // Space between each item (in inventory)
            itemSlotRectTransform.anchoredPosition = new Vector2((x * itemSlotCellSize) + 20, (y * itemSlotCellSize) + 50);

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
