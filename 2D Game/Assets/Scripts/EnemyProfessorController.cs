using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyProfessorController: MonoBehaviour
{
    // Animator
    private Animator myAnimator;
    private Transform target;
    public Transform homePosition;
    // SerializeField = is private but can still change it in the editor
    [SerializeField]
    private float moveSpeed = 2f;
    private float maxRange = 5f;
    private float minRange = 0.5f;

    // Time to wait to reload after player dies
    public float waitToReload;
    private bool reloading;
    private GameObject thePlayer;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        // Find the player
        target = FindObjectOfType<Player>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        // Follow target (player) at range
        if (Vector3.Distance(target.position, transform.position) <= maxRange && Vector3.Distance(target.position, transform.position) >= minRange)
        {
            FollowPlayer();
        }
        else if (Vector3.Distance(target.position, transform.position) >= maxRange)
        {
            GoHomePosition();
        }

        if (reloading)
        {
            Debug.Log(waitToReload);

            waitToReload -= Time.deltaTime;
            if (waitToReload < 0)
            {
                SceneManager.LoadScene("Campus");
                thePlayer.SetActive(true);
            }
        }
    }

    public void FollowPlayer()
    {
        // Follow player
        myAnimator.SetBool("isMoving", true);

        myAnimator.SetFloat("MoveX", target.position.x - transform.position.x);
        myAnimator.SetFloat("MoveY", target.position.y - transform.position.y);

        // (This object's position, Target's position, at speed)
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
    }

    public void GoHomePosition()
    {
        myAnimator.SetFloat("MoveX", homePosition.position.x - transform.position.x);
        myAnimator.SetFloat("MoveY", homePosition.position.y - transform.position.y);
        transform.position = Vector3.MoveTowards(transform.position, homePosition.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, homePosition.position) == 0)
        {
            myAnimator.SetBool("isMoving", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "MyWeapon")
        {
            Vector2 difference = transform.position - collision.transform.position;
            transform.position = new Vector2(transform.position.x + difference.x, transform.position.y + difference.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        /*
        if (other.gameObject.name == "Player")
        {
            // Destroy (collision.gameObject);
            other.gameObject.SetActive(false);
            reloading = true;
            thePlayer = other.gameObject;
        }
        */


    }
}
