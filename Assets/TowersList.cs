using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowersList : MonoBehaviour
{
    // Start is called before the first frame update

    public List<GameObject> towersList = new List<GameObject>();
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Add(GameObject obj)
    {
        towersList.Add(obj);
    }
    public void Delete()
    {
        foreach (GameObject obj in towersList)
        {
            Destroy(obj);
        }
        towersList.Clear();
    }
}
