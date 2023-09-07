using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Bananatower : MonoBehaviour
{
    public float radius = 1.5f;
    [SerializeField] int delay = 2000;
    [SerializeField] GameObject Projectile;
    [SerializeField] List<GameObject> target = new List<GameObject>();

    private int currentdelay = 0;
    // Start is called before the first frame update
    void Start()
    {
        target.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0)
        { }

        else if (currentdelay <= 0 && target.Count != 0)
        {

            Vector3 spawnposition = new Vector3();
            int index = Random.Range(0, target.Count - 1);
            spawnposition.x = target[index].transform.position.x + Random.Range((float)-0.2, (float)0.2);
            spawnposition.y = target[index].transform.position.y + Random.Range((float)-0.2, (float)0.2);
            spawnposition.z = 0;

            GameObject child = Instantiate(Projectile, spawnposition, transform.rotation);

            child.transform.parent = null;
            currentdelay = delay;
        }

        else
        {
            currentdelay--;
        }


    }

    public void getTarget()
    {

        Collider2D[] collided = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach (Collider2D c in collided)
        {
            if (c.tag == "Path")
                target.Add(c.gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}