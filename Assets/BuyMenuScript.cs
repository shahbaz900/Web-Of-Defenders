using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyMenuScript : MonoBehaviour
{
    [SerializeField] GameObject toBuy;
    [SerializeField] int cost;

    public void Update()
    {

    }

    public GameObject getToBuy() { return toBuy; }
    public int getCost() { return cost; }

}