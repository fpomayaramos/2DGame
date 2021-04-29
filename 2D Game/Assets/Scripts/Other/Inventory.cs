using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public event EventHandler OnItemListChanged;
    private List<Item> itemList;
    private Action<Item> useItemAction;

    public Inventory(Action<Item> useItemAction)
    {
        this.useItemAction = useItemAction;
        itemList = new List<Item>();

        //Inside inventory as soon as game starts
        //AddItem(new Item { itemType = Item.ItemType.Apple, amount = 1 });
        //AddItem(new Item { itemType = Item.ItemType.Book, amount = 1 });
        //AddItem(new Item { itemType = Item.ItemType.Book, amount = 1 });

        // Debug.Log(itemList.Count);
    }

    /**
     * This function adds an item to the list (inventory)
     * If the item is stackable, don't need to add an additional prefab
     * and stack them together,
     * else if the item is not stackable, add another prefab in the inventory
     */
    public void AddItem(Item item)
    {
        if (item.IsStackable())
        {
            bool itemAlreadyInInventory = false;
            foreach (Item inventoryItem in itemList)
            {
                if (inventoryItem.itemType == item.itemType)
                {
                    inventoryItem.amount += item.amount;
                    itemAlreadyInInventory = true;
                }
            }
            if (!itemAlreadyInInventory)
            {
                itemList.Add(item);
            }
        }
        else
        {
            itemList.Add(item);
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    /*
     * When removing an item
     * if the item is stackable (ex. apple(s))
     * remove all the stacks of that item (ex. remove all 5 apples)
     * else, just remove the unstackable item (ex. you have two swords, you select one, just remove one sword)
     */
    public void RemoveItem(Item item)
    {
        if (item.IsStackable())
        {
            Item itemInInventory = null;
            foreach (Item inventoryItem in itemList)
            {
                if (inventoryItem.itemType == item.itemType)
                {
                    inventoryItem.amount -= item.amount;
                    itemInInventory = inventoryItem;
                }
            }
            if (itemInInventory != null && itemInInventory.amount <= 0)
            {
                itemList.Remove(itemInInventory);
            }
        }
        else
        {
            itemList.Remove(item);
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public void UseItem(Item item)
    {
        useItemAction(item);
    }

    public List<Item> GetItemList()
    {
        return itemList;
    }
}
