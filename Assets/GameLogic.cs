using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    [SerializeField] GameObject[] Enemies;
    [SerializeField] GameObject[] SpawnPoints;

    [SerializeField] GameObject EnemyDestination;
    [SerializeField] GameObject Money;

    [SerializeField] GameObject PausePlay;
    [SerializeField] GameObject GameOver;

    [SerializeField] int Roundlimit;
    [SerializeField] int currentRound = 0;

    [SerializeField] int RoundLength;
    [SerializeField] int currentRoundTimer;

    [SerializeField] int SpawnDelay;
    [SerializeField] int currentDelay;

    [SerializeField] int level;

    [SerializeField] GameObject towersList;



    // Start is called before the first frame update
    void Start()
    {
        level = PlayerPrefs.GetInt("Level");
        currentRoundTimer = 0;
        transform.Find("Castle").Find("Canvas").Find("Current Round").GetComponent<TextMeshProUGUI>().text = currentRound.ToString() + "/" + Roundlimit.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0)
        { }

        else if (currentDelay == 0 && currentRoundTimer != 0)
        {
            Vector3 spawnposition = SpawnPoints[Random.Range(0, 4)].transform.position;

            GameObject toSpawn;
            //Spawns a new enemy for every level upto and below 4, then just spawns them all
            if (level <= 5)
                toSpawn = Enemies[Random.Range(0, level - 1)];
            else
                toSpawn = Enemies[Random.Range(0, 4)];

            GameObject child = Instantiate(toSpawn, spawnposition, transform.rotation, transform);

            child.GetComponent<AIDestinationSetter>().target = EnemyDestination.transform;

            if (toSpawn.tag == "Ranged Enemy")
            {
                child.GetComponent<RangedEnemy>().MoneyCounter = Money;
                child.GetComponent<RangedEnemy>().target = EnemyDestination;
            }
            else
                child.GetComponent<Enemy>().MoneyCounter = Money;

            currentDelay = SpawnDelay;
        }

        if (currentRoundTimer == 0 && currentDelay == 0)
        {
            newRound();
            level++;
        }

        if (currentDelay != 0)
            currentDelay--;

        if (currentRoundTimer != 0)
            currentRoundTimer--;

    }

    public void newRound()
    {
        if (currentRound < Roundlimit && currentRoundTimer == 0 && currentDelay == 0)
        {
            //castle.health = 100;
            EnemyDestination.GetComponent<Castle>().resetHealth();
            EnemyDestination.GetComponent<Castle>().resetMoney();
            towersList.GetComponent<TowersList>().Delete();
            //castle.GetComponent<Castle>().resetHealth();
            currentRoundTimer = RoundLength + 500;
            currentRound++;
            SpawnDelay -= 50; // Adjust this value based on how much you want to decrease the spawn delay in each round.
            transform.Find("Castle").Find("Canvas").Find("Current Round").GetComponent<TextMeshProUGUI>().text = currentRound.ToString() + "/" + Roundlimit.ToString();
        }
        //Change this so it only runs once all enemies are dead.
        else if (currentRound == Roundlimit)
        {
            bool Enemiesdead = true;
            foreach (Transform t in transform)
                if (t.tag == "Enemy" || t.tag == "Ranged Enemy")
                {
                    Enemiesdead = false;
                    break;
                }

            if (Enemiesdead)
            {
                transform.Find("Menu").gameObject.SetActive(false);
                GameOver.SetActive(true);
                PausePlay.SetActive(false);
            }
        }
    }
    public void setRoundlimit(int limit)
    {
        Roundlimit = limit;
    }

    public void setRoundLength(int length)
    {
        RoundLength = length;
    }

    public void setSpawnDelay(int delay)
    {
        SpawnDelay = delay;

    }

    public void setLevel(int lev)
    {
        level = lev;
    }
}