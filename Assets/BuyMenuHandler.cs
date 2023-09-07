using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UIElements;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;
using static UnityEditor.PlayerSettings;

public class BuyMenuHandler : MonoBehaviour
{
    [SerializeField] GameObject Money;
    private int currentcost;
    private GameObject towerinHand;
    [SerializeField] int rotatation = 0;
    [SerializeField] GameObject towersList;
    [SerializeField] GameObject highlightPrefab; // Highlight prefab for valid placement
    private GameObject highlightIndicator;
    public bool placementValid = false;
    public Material newMaterial;
    public Color originalcolor;
    [SerializeField] GameObject rangeIndicator;

    void Start()
    {
        towerinHand = null;
        rangeIndicator.SetActive(false);
        //newMaterial = new Material(towerinHand.GetComponent<SpriteRenderer>().material);
        //originalcolor = GetComponent<SpriteRenderer>().material.color; // Store the original color
        //highlightIndicator = Instantiate(highlightPrefab, Vector3.zero, Quaternion.identity);
        //highlightIndicator.SetActive(false);
    }

    void Update()
    {
        if (Input.touchCount != 1)
            return;

        Touch touch = Input.touches[0];
        Vector3 touchpos = touch.position;

        

        if (towerinHand != null)
        {
            towerinHand.transform.position = Camera.main.ScreenToWorldPoint(touchpos) + new Vector3(0, 0, 1);

            if (towerinHand.tag == "Obstacle")
            {
                RotateObstacle(towerinHand);
            }
            else
            {
                towerinHand.transform.position = Camera.main.ScreenToWorldPoint(touchpos) + new Vector3(0, 0, 1);
                bool isValidPlacement = CheckValidPlacement(placementValid);
                SetHighlightColor(isValidPlacement);


                Bananatower bananatemp = towerinHand.GetComponent<Bananatower>();

                rangeIndicator.SetActive(true);
                rangeIndicator.transform.position = towerinHand.transform.position;

                if (bananatemp != null)
                {
                    rangeIndicator.transform.localScale = new Vector3(towerinHand.GetComponent<Bananatower>().radius * 2, towerinHand.GetComponent<Bananatower>().radius * 2, 1);
                }
                else
                {
                    rangeIndicator.transform.localScale = new Vector3(towerinHand.GetComponent<MelonShooterBehavior>().radius * 2, towerinHand.GetComponent<MelonShooterBehavior>().radius * 2, 1);
                }
            }
            //highlightIndicator.SetActive(true);
        }
        else if (touch.phase == TouchPhase.Began && towerinHand == null)
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(touchpos), Camera.main.transform.forward);
            if (hit.collider != null && hit.transform.tag == "Menu" && hit.collider.GetComponent<BuyMenuScript>().getCost() <= int.Parse(Money.GetComponent<TextMeshProUGUI>().text))
            {
                towerinHand = Instantiate(hit.transform.GetComponent<BuyMenuScript>().getToBuy(), Camera.main.ScreenToWorldPoint(touchpos), transform.rotation);
                towerinHand.transform.rotation = Quaternion.Euler(0, 0, rotatation);
                currentcost = hit.transform.GetComponent<BuyMenuScript>().getCost();
                
                //rangeIndicator.GetComponent<SpriteRenderer>().
                towersList.GetComponent<TowersList>().Add(towerinHand);
            }
        }

        if (touch.phase == TouchPhase.Ended && towerinHand != null)
        {
            Collider2D[] collided = Physics2D.OverlapCircleAll(towerinHand.transform.position, (float)0.4);
            bool isValidPlacement = true;
            rangeIndicator.SetActive(false);

            foreach (Collider2D col in collided)
            {
                if ((col.tag == "Path" && towerinHand.tag != "Obstacle") || (col.tag == "Tower" && col.transform.position != towerinHand.transform.position))
                {
                    isValidPlacement = false;
                    break;
                }
            }
            placementValid = isValidPlacement;
            if (isValidPlacement)
            {
                if (towerinHand.GetComponent<Bananatower>() != null)
                    towerinHand.GetComponent<Bananatower>().getTarget();

                towerinHand.GetComponent<SpriteRenderer>().color = Color.white;
                towerinHand = null;
                Money.GetComponent<TextMeshProUGUI>().text = (int.Parse(Money.GetComponent<TextMeshProUGUI>().text) - currentcost).ToString();
                //highlightIndicator.SetActive(false);  
            }
            else
            {
                Destroy(towerinHand);
                towerinHand = null;
            }
            // No need to change the color here
            //Debug.Log("Im here");
            //towerinHand.GetComponent<SpriteRenderer>().material.color = Color.white;
            
        }
    }

    bool CheckValidPlacement(bool placement_valid)
    {
        // Perform raycasting or collision checks to determine if the position is valid
        // Return true if valid, false if not
        Collider2D[] collided = Physics2D.OverlapCircleAll(towerinHand.transform.position, (float)0.4);
        bool isValidPlacement = true;

        foreach (Collider2D col in collided)
        {
            if ((col.tag == "Path" && towerinHand.tag != "Obstacle") || (col.tag == "Tower" && col.transform.position != towerinHand.transform.position))
            {
                isValidPlacement = false;
                break;
            }
        }

        return isValidPlacement;
    }

    void SetHighlightColor(bool isValid)
    {
        if (isValid)
        {
            towerinHand.GetComponent<SpriteRenderer>().color = Color.green;
        }
        else
        {
            towerinHand.GetComponent<SpriteRenderer>().color = Color.red;
        }
    }

    public void rotate90()
    {
        rotatation += 90;
        if (rotatation == 360)
            rotatation = 0;
    }

    void RotateObstacle(GameObject obstacle)
    {

        float x = obstacle.transform.position.x;
        float y = obstacle.transform.position.y;

        if ((x <= -1f && y < 3 && y > 1) || (x >= 1f && y < 3 && y > 2))
        {
            Debug.Log("Inside if");
            obstacle.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
        }
        else if ((y <= 7f && y >= 3.5f && x < 1 && x > -1) || (y >= -5 && y <= 2 && x < 1 && x > -1))
        {
            Debug.Log("Inside elif");
            obstacle.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }



}
