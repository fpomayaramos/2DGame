using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtEnemy : MonoBehaviour
{
    public int damageToGive = 1;
    public GameObject burstDamage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyHealthManager>().HurtEnemy(damageToGive);

            EnemyHealthManager eHealthMan;
            eHealthMan = collision.gameObject.GetComponent<EnemyHealthManager>();
            eHealthMan.HurtEnemy(damageToGive);

            Instantiate(burstDamage, transform.position, transform.rotation);
        }
        if (collision.gameObject.tag == "Boss") {
            collision.gameObject.GetComponent<BossHealthManager>().HurtEnemy(damageToGive);

            BossHealthManager eHealthMan;
            eHealthMan = collision.gameObject.GetComponent<BossHealthManager>();
            eHealthMan.HurtEnemy(damageToGive);

            Instantiate(burstDamage, transform.position, transform.rotation);
        }

    }
}
