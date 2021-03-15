using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/*
 * Item class
 * and their description such as
 * item name, sprites, is it stackle?
 */
[Serializable]
public class Item
{
    public enum ItemType
    {
        Apple,
        Book,
    }

    public ItemType itemType;
    public int amount;

    /**
     * Return sprites
     */
    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
            case ItemType.Apple:    return ItemAssets.Instance.appleSprite;
            case ItemType.Book:     return ItemAssets.Instance.bookSprite;
        }
    }

    /**
     * Return whether it is stackable or not
     */
    public bool IsStackable()
    {
        switch (itemType)
        {
            default:
            case ItemType.Apple:
                return true;
            case ItemType.Book:
                return false;
        }
    }
}
