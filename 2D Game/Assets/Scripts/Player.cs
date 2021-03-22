using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Player Movement
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Animator animator;


    // 
    [SerializeField] private UI_Inventory uiInventory;
    private Inventory inventory;

    Vector2 movement;

    // Start is called before the first frame update
    private void Awake()
    {
        inventory = new Inventory(UseItem);
        uiInventory.SetPlayer(this);
        uiInventory.SetInventory(inventory);

    }

    /**
     * This function removes an item from inventory when the item is used
     */
    private void UseItem(Item item)
    {
        switch (item.itemType)
        {
            case Item.ItemType.Apple:
                inventory.RemoveItem(new Item { itemType = Item.ItemType.Apple, amount = 1 });
                break;
        }
    }

    /*
     * This function allows the player to move around the game/map
     */
    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);



    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    /**
     * When Player collides with items
     * "Deletes" or "Destroys" them
     * and adds them to the player's inventory
     */
    private void OnTriggerEnter2D(Collider2D collider)
    {
        ItemWorld itemWorld = collider.GetComponent<ItemWorld>();
        if (itemWorld != null)
        {
            // Touching Item
            inventory.AddItem(itemWorld.GetItem());
            itemWorld.DestroySelf();
        }
    }

    // Get the player's x and y position
    public Vector3 GetPosition()
    {
        return transform.position;
    }
}
