using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class BananaProjectile : MonoBehaviour
{
    [SerializeField] int damage = 20;
    [SerializeField] int lifespan = 10000;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0)
        { }
        else
        {
            lifespan--;
            if (lifespan < 0)
                Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ranged Enemy")
        {
            collision.gameObject.GetComponent<RangedEnemy>().takeDamage(damage);
            collision.gameObject.GetComponent<RangedEnemy>().pauseEnemy();
            Destroy(gameObject);
        }

        else if (collision.gameObject.tag == "Enemy")
        {
            //Destroy(collision.gameObject);
            collision.gameObject.GetComponent<Enemy>().takeDamage(damage, gameObject.tag);
            collision.gameObject.GetComponent<Enemy>().pauseEnemy();
            Destroy(gameObject);
        }
    }
}
