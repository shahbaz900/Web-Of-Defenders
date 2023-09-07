using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PomegranateProjectile: MonoBehaviour
{
    [SerializeField] int velocity = 1;
    [SerializeField] int damage = 80;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = new Vector3();
        movement.x = velocity * Time.deltaTime * Mathf.Cos((float)(transform.rotation.eulerAngles.z * Mathf.PI / 180));
        movement.y = velocity * Time.deltaTime * Mathf.Sin((float)(transform.rotation.eulerAngles.z * Mathf.PI / 180));
        transform.position += movement;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ranged Enemy")
            collision.gameObject.GetComponent<RangedEnemy>().takeDamage(damage);

        else if (collision.gameObject.tag == "Enemy")
            collision.gameObject.GetComponent<Enemy>().takeDamage(damage, gameObject.tag);

        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Ranged Enemy")
        {
            //Putting code here so that it damages all enemies around a certain radius
            Collider2D[] collided = Physics2D.OverlapCircleAll(transform.position, 1);
            foreach (Collider2D c in collided)
            {
                if (c.tag == "Enemy")
                    c.gameObject.GetComponent<Enemy>().takeDamage(damage, gameObject.tag);
                else if (c.tag == "Ranged Enemy")
                    c.gameObject.GetComponent<RangedEnemy>().takeDamage(damage);
            }


            Destroy(gameObject);
        }
    }
}
