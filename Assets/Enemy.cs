using Pathfinding;
using Pathfinding.Ionic.Zip;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int health = 100;
    [SerializeField] int damage = 1;
    [SerializeField] int pausedelay = 1000;
    [SerializeField] int currentdelay = 0;
    [SerializeField] int moneygiven = 1;

    [SerializeField] float fireAnimationDelay = 1f;
    [SerializeField] float waterAnimationDelay = 1f;
    [SerializeField] float windAnimationDelay = 1f;

    public GameObject MoneyCounter;
    public Animator animator;

    public string deathCause;
    public string hitBy;

    [SerializeField] GameObject towersList;

    private Vector3 baseFixedPosition = new Vector3(-0.05f, 0.11f, 0f);

    void Start()
    {
        towersList.GetComponent<TowersList>().Add(gameObject);
    }


    // Update is called once per frame
    void Update()
    {
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

        else if (currentPosition.x <= 6 && currentPosition.x >=1 && currentPosition.y < 3 && currentPosition.y > 2)
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
            //Destroy(gameObject);
            //Debug.Log("Starting DelayedAction coroutine");
        }

        gameObject.GetComponent<Seeker>().IsDone();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Collision Enter occured");
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Castle>().takeDamage(damage);
            Destroy(gameObject);
        }
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        //Debug.Log("Collision Stay occured");
        //Ranged enemies do not have this function, which means they cannot break down walls
        if (other.gameObject.tag == "Obstacle" && currentdelay == 0)
        {
            other.GetComponent<Wall>().reduceHealth(damage);
            animator.SetBool("isMoving", false);
            animator.SetBool("isStopped", true);
            currentdelay = 500;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Obstacle" && currentdelay == 0)
        {
            // Reset animator parameters to allow movement animation
            animator.SetBool("isMoving", true);
            animator.SetBool("isStopped", false);
        }
    }

    public void takeDamage(int damage, string tag)
    {
        health -= damage;
        hitBy = tag;
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
