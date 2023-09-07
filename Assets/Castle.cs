using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class Castle : MonoBehaviour
{
    [SerializeField] public int health = 100;
    [SerializeField] GameObject GameOverMenu;
    [SerializeField] GameObject GameLogic;


    public Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 100 && health > 85)
        {
            animator.SetInteger("Health", 100);
        }

        else if (health <= 85 && health > 70)
        {
            animator.SetInteger("Health", 85);
        }

        else if (health <= 70 && health > 55)
        {
            animator.SetInteger("Health", 70);
        }

        else if (health <= 55 && health > 30)
        {
            animator.SetInteger("Health", 55);
        }

        else if (health <= 30 && health > 15) 
        {
            animator.SetInteger("Health", 30);
        }

        else
        {
            animator.SetInteger("Health", 15);
        }


        if (health <= 0)
        {
            //Change this with something else once the other scenes are implemented
            GameOverMenu.SetActive(true);
            GameLogic.SetActive(false);
        }
    }

    public void takeDamage(int damage)
    {
        health -= damage;
        transform.Find("Canvas").transform.Find("Health").GetComponent<TextMeshProUGUI>().text = health.ToString();
    }

    public void resetHealth()
    {
        int damage = 100 - health;
        int penalty = (int)(damage / 2.5f);
        health = 100 - penalty;
        transform.Find("Canvas").transform.Find("Health").GetComponent<TextMeshProUGUI>().text = health.ToString();
    }

    public void resetMoney()
    {
        string money;
        money = transform.Find("Canvas").transform.Find("Money").GetComponent<TextMeshProUGUI>().text;
        //Debug.Log("Money is: " + money);
        int temp = int.Parse(money);

        int money_to_give = (int)(temp / 1.5f);

        //Debug.Log("Money now is: " + money_to_give);
        transform.Find("Canvas").transform.Find("Money").GetComponent<TextMeshProUGUI>().text = money_to_give.ToString();
    }
}
