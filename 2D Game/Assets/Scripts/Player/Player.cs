using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // private = we don't want to change this
    private float movementSpeed = 5f;

    // Inventory
    [SerializeField] private UI_Inventory uiInventory;
    private Inventory inventory;

    // Animator
    private Animator animator;
    private bool playerMoving;
    public Vector2 lastMove;

    public string startPoint;

    private Rigidbody2D myRigidbody2D;

    // To stop creating duplicates when player moves around scenes
    private static bool playerExists;

    // attack
    private float attackTime = .2f;
    private float attackCounter = .2f;
    private bool isAttacking;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        myRigidbody2D = GetComponent<Rigidbody2D>();

        // Check if player already exist in the new scene
        if (!playerExists)
        {
            playerExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

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
        playerMoving = false;

        // Vector 3 takes 3 parameters x, y, and z
        // Moves Horizontally, right or left
        if (Input.GetAxisRaw("Horizontal") > 0.5f || Input.GetAxisRaw("Horizontal") < -0.5f)
        {
            // transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * movementSpeed * Time.deltaTime, 0f, 0f));
            myRigidbody2D.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * movementSpeed, myRigidbody2D.velocity.y);
            playerMoving = true;
            lastMove = new Vector2(Input.GetAxisRaw("Horizontal"), 0f);
        }
        // Moves Vertically, up or down
        if (Input.GetAxisRaw("Vertical") > 0.5f || Input.GetAxisRaw("Vertical") < -0.5f)
        {
            // transform.Translate(new Vector3(0f, Input.GetAxisRaw("Vertical") * movementSpeed * Time.deltaTime, 0f));
            myRigidbody2D.velocity = new Vector2(myRigidbody2D.velocity.x, Input.GetAxisRaw("Vertical") * movementSpeed);
            playerMoving = true;
            lastMove = new Vector2(0f, Input.GetAxisRaw("Vertical"));
        }



        if (isAttacking)
        {
            attackCounter -= Time.deltaTime;
            if (attackCounter <= 0)
            {
                animator.SetBool("isAttacking", false);
                isAttacking = false;
            }
        }

        // Attack animation
        if (Input.GetKeyDown(KeyCode.T))
        {
            attackCounter = attackTime;
            animator.SetBool("isAttacking", true);
            isAttacking = true;
        }





        if (Input.GetAxisRaw("Horizontal") < 0.5f && Input.GetAxisRaw("Horizontal") > -0.5f)
        {
            myRigidbody2D.velocity = new Vector2(0f, myRigidbody2D.velocity.y);
        }
        if (Input.GetAxisRaw("Vertical") < 0.5f && Input.GetAxisRaw("Vertical") > -0.5f)
        {
            myRigidbody2D.velocity = new Vector2(myRigidbody2D.velocity.x, 0f);
        }

        // Animation in whichever direction
        animator.SetFloat("MoveX", Input.GetAxisRaw("Horizontal"));
        animator.SetFloat("MoveY", Input.GetAxisRaw("Vertical"));
        animator.SetBool("PlayerMoving", playerMoving);
        animator.SetFloat("LastMoveX", lastMove.x);
        animator.SetFloat("LastMoveY", lastMove.y);
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
