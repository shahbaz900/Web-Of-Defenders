using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MelonProjectile : MonoBehaviour
{
    [SerializeField] int velocity = 1;
    [SerializeField] int damage = 10;
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
        {
            collision.gameObject.GetComponent<RangedEnemy>().takeDamage(damage);
            Destroy(gameObject);
        }

        else if (collision.gameObject.tag == "Enemy")
        {
            //Destroy(collision.gameObject);
            collision.gameObject.GetComponent<Enemy>().takeDamage(damage, gameObject.tag);
            Destroy(gameObject);
        }
    }
}
