using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HurtPlayer : MonoBehaviour
{
    private PlayerHealthManager healthPlayer;
    private float waitToHurt = 2f;
    private bool isTouching;
    [SerializeField]
    public int damageToGive;

    // Start is called before the first frame update
    void Start()
    {
        healthPlayer = FindObjectOfType<PlayerHealthManager>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (reloading)
        {
            waitToLoad -= Time.deltaTime;

            if (waitToLoad <= 0)
            {
                SceneManager.LoadScene("Campus");
            }
        }
        */

        if (isTouching)
        {
            waitToHurt -= Time.deltaTime;
            if (waitToHurt <= 0)
            {
                healthPlayer.HurtPlayer(damageToGive);
                waitToHurt = 2f;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        /*
        if (other.collider.tag == "Player")
        {
            // Instead of destroying, it unchecks the checkbox (invisible, inactive)
            // other.gameObject.SetActive(false);
            other.gameObject.GetComponent<PlayerHealthManager>().HurtPlayer(damageToGive);
            reloading = true;
        }
        */
        if (other.gameObject.name == "Player")
        {
            other.gameObject.GetComponent<PlayerHealthManager>().HurtPlayer(damageToGive);
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.collider.tag == "Player")
        {
            isTouching = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.tag == "Player")
        {
            isTouching = false;
            waitToHurt = 2f;
        }
    }
}
