using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/**
 * This class is for items in the game world/map
 * Spawned items as soon as the game begins
 */
public class ItemWorld : MonoBehaviour
{
    /*
     * Spawn an item at a specific position
     */
    public static ItemWorld SpawnItemWorld(Vector3 position, Item item)
    {
        Transform transform = Instantiate(ItemAssets.Instance.pfItemWorld, position, Quaternion.identity);
        ItemWorld itemWorld = transform.GetComponent<ItemWorld>();
        itemWorld.SetItem(item);
        return itemWorld;
    }

    /**
     * The item is dropped close to the player in a random direction + animation
     */
    public static ItemWorld DropItem (Vector3 dropPosition, Item item)
    {
        Vector3 randomDir = UtilsClass.GetRandomDir();
        ItemWorld itemWorld = SpawnItemWorld(dropPosition + randomDir * 1f, item);
        itemWorld.GetComponent<Rigidbody2D>().AddForce(randomDir * 1f, ForceMode2D.Impulse);
        return itemWorld;
    }

    private Item item;
    private SpriteRenderer spriteRenderer;
    private TextMeshPro textMeshPro;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        textMeshPro = transform.Find("Text").GetComponent<TextMeshPro>();
    }

    /** Prefab: a special type of component that allows fully configured GameObjects to be saved in the Project for reuse.
     *  -pfItemWorld
     *  For every prefab switch to the correct sprite and amount.
     */
    public void SetItem(Item item)
    {
        this.item = item;
        spriteRenderer.sprite = item.GetSprite();

        if (item.amount > 1)
        {
            textMeshPro.SetText(item.amount.ToString());
        }
        else
        {
            textMeshPro.SetText("");
        }
    }

    // Return item
    public Item GetItem()
    {
        return item;
    }

    /**
     * "Destroys" item from the map
     */
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
