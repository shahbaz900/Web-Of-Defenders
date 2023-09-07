using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class RangedEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator animator;
    [SerializeField] int health = 100;
    [SerializeField] int damage = 1;
    [SerializeField] int pausedelay = 1000;
    [SerializeField] int currentdelay = 0;
    [SerializeField] int moneygiven = 1;

    [SerializeField] float fireAnimationDelay = 1f;
    [SerializeField] float waterAnimationDelay = 1f;
    [SerializeField] float windAnimationDelay = 1f;

    public string deathCause;
    public string hitBy;

    public GameObject target;
    [SerializeField] GameObject Projectile;
    public GameObject MoneyCounter;

    [SerializeField] GameObject towersList;

    void Start()
    {
        towersList.GetComponent<TowersList>().Add(gameObject);
    }


    // Update is called once per frame
    void Update()
    {
        //If you raech the end of hte path but not your destination, then that means you have to break walls
        Vector3 currentPosition = gameObject.transform.position;


        // Check the x position to determine rotation
        if (currentPosition.y <= 7 && currentPosition.y >= 3.5 && currentPosition.x < 1 && currentPosition.x > -1)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 180f);
        }
        else if (currentPosition.y >= -5 && currentPosition.y <= 2 && currentPosition.x < 1 && currentPosition.x > -1)
        {

        }
        else if (currentPosition.x >= -7.5 && currentPosition.x <= -4.5 && currentPosition.y < 3 && currentPosition.y > 2)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, -90f);
        }

        else if (currentPosition.x <= 6 && currentPosition.x >= 1 && currentPosition.y < 3 && currentPosition.y > 2)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 90f);
        }
        //Sets whether hte object is active based on it being delayed or not
        if (currentdelay > 0)
        {
            gameObject.GetComponent<AIPath>().canMove = false;
            currentdelay--;
        }
        else
            gameObject.GetComponent<AIPath>().canMove = true;

        if (health <= 0)
        {
            MoneyCounter.GetComponent<TextMeshProUGUI>().text = (int.Parse(MoneyCounter.GetComponent<TextMeshProUGUI>().text) + moneygiven).ToString();
            deathCause = hitBy;
            //Debug.Log("Death cause is: " + deathCause);
            if (deathCause == "Fireball")
            {
                //Debug.Log("So im going into the if condition");
                animator.SetBool("isFire", true);
                animator.SetBool("isMoving", false);
                StartCoroutine(DelayedAction(fireAnimationDelay));
            }

            else if (deathCause == "WaterSpray")
            {
                //Debug.Log("");
                animator.SetBool("isWater", true);
                animator.SetBool("isMoving", false);
                //Destroy(gameObject, 2f);
                StartCoroutine(DelayedAction(waterAnimationDelay));
            }

            else if (deathCause == "Wind")
            {
                //Debug.Log("");
                animator.SetBool("isWind", true);
                animator.SetBool("isMoving", false);
                StartCoroutine(DelayedAction(windAnimationDelay));
            }

            else
            {
                StartCoroutine(DelayedAction(0.1f));
            }
        }

        if (gameObject.GetComponent<AIPath>().reachedDestination && Time.timeScale != 0)
        {
            if (currentdelay == 0)
            {
                Vector3 direction = target.transform.position - transform.position;
                transform.rotation = Quaternion.Euler(0, 0, (float)(Mathf.Atan2(direction.y, direction.x) * 180 / 3.14));
                GameObject child = Instantiate(Projectile, transform.position, transform.rotation);

                child.transform.parent = null;
                currentdelay = pausedelay;
            }
            else
                currentdelay--;
        }    
    }

    public void takeDamage(int damage)
    {
        health -= damage;
    }

    public void pauseEnemy()
    {
        this.currentdelay = pausedelay;
    }

    IEnumerator DelayedAction(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
        MoneyCounter.GetComponent<TextMeshProUGUI>().text = (int.Parse(MoneyCounter.GetComponent<TextMeshProUGUI>().text) + moneygiven).ToString();
    }
}
