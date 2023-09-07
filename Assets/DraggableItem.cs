using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableItem : MonoBehaviour
{
    public TutorialManager tutorialManager; // Reference to the TutorialManager script
    public bool interacted = false;

    private void Update()
    {
        tutorialManager.OnDraggableItemInteract();
    }

    private void OnMouseDown()
    {

        interacted = true;
        //tutorialManager.OnDraggableItemInteract();

    }
}


