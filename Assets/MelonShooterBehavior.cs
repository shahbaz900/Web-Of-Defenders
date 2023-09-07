using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class MelonShooterBehavior : MonoBehaviour
{
    public float radius = 1.5f;
    [SerializeField] int delay = 30;
    [SerializeField] GameObject Projectile;

    public Animator animator;

    private GameObject target = null;
    private int currentdelay = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Resets the target of the tower to null so the tower stops shooting
        if (target.IsDestroyed())
            target = null;


        //Start of the main logic.
        if (Time.timeScale == 0)
        { }

        else if (target == null)
        {
            Collider2D[] collided = Physics2D.OverlapCircleAll(transform.position, radius);
            foreach (Collider2D c in collided)
                if ((c.tag == "Enemy" || c.tag == "Ranged Enemy") && (target == null || Vector3.Distance(transform.position, c.transform.position) < Vector3.Distance(transform.position, target.transform.position)))
                    target = c.gameObject;
        }

        else if (currentdelay == 0)
        {
            Vector3 direction = target.transform.position - transform.position;
            transform.rotation = Quaternion.Euler(0,0,(float)(Mathf.Atan2(direction.y,direction.x)*180/ 3.14));
            GameObject child = Instantiate(Projectile,transform.position, transform.rotation);

            child.transform.parent = null;
            transform.rotation = Quaternion.identity;
            currentdelay = delay;
        }

        else
            currentdelay--;

            
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
