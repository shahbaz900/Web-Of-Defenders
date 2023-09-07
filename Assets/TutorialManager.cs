using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class TutorialManager : MonoBehaviour
{
    public GameObject fingerIcon;
    public DraggableItem draggableItem;
    public List<DraggableItem> draggableItems = new List<DraggableItem>();
    public float tutorialTime;
    private float currentTime;
    private float startTutorialTime;

    public Animator fingerIconAnimator;

    private bool interactionOccurred = false;

    private void Start()
    {
        currentTime = Time.time;
        foreach (DraggableItem item in draggableItems)
        {
            item.tutorialManager = this;
        }
    }

    private IEnumerator ShowTutorial()
    {
        yield return new WaitForSeconds(tutorialTime);

        if (!interactionOccurred)
        {
            ShowFingerIcon();
        }
    }

    private void Update()
    {
        startTutorialTime = Time.time;
    }

    /*private void Update()
    {
        bool itemInteracted = false;
        foreach (DraggableItem item in draggableItems)
        {
            if (item.interacted)
            {
                itemInteracted = true;
                break;
            }
        }

        if (!itemInteracted && currentTime + 5f  < startTutorialTime) 
        {
            ShowFingerIcon();
        }

        else
        {
            HideFingerIcon();
        }
    }*/

    public void OnDraggableItemInteract()
    {
        bool allItemsInteracted = false;
        foreach (DraggableItem item in draggableItems)
        {
            if (item.interacted)
            {
                allItemsInteracted = true;
                break;
            }
        }

        float endtime = currentTime + 5f;


        if (!allItemsInteracted)
        {
            ShowFingerIcon();
        }
        else
        {
            HideFingerIcon();
        }
    }

    private void ShowFingerIcon()
    {
        fingerIcon.SetActive(true);
        fingerIconAnimator.SetBool("Show", true);
        fingerIcon.transform.position = draggableItem.transform.position + new Vector3(0, 0.5f, 0);
        // Start animation here if desired
        SpriteRenderer spriteRenderer = fingerIcon.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.sortingLayerName = "New Layer 5";
            spriteRenderer.sortingOrder = 5; // Change the sorting order if needed
        }
        Vector3 newPosition = new Vector3(-2.5f, -6.5f, 0f); // Replace with actual values
        fingerIcon.transform.position = newPosition;
    }

    private void HideFingerIcon()
    {
        fingerIconAnimator.SetBool("Show", false);
        fingerIcon.SetActive(false);
    }
}
