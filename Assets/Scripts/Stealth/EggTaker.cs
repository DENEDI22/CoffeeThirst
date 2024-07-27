using System.Collections;
using System.Collections.Generic;
using Stealth;
using TMPro;
using UnityEngine;

public class EggTaker : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject interactionIndicator;
    [SerializeField] private Transform playersHandPivotPoint;
    
    public bool OnInteract()
    {
        if (playersHandPivotPoint.childCount != 0)
        {
            playersHandPivotPoint.GetChild(0).gameObject.SetActive(false);
            playersHandPivotPoint.DetachChildren();
            FindObjectOfType<WinLoseLogic>().OnEggTaken();
            return true;
        }
        return false;
    }

    public void Select()
    {
        if (playersHandPivotPoint.childCount == 0)
        {
            interactionIndicator.GetComponent<TextMeshPro>().text = "Find an egg!";
        }
        else
        {
            interactionIndicator.GetComponent<TextMeshPro>().text = "Press E to drop the egg";
        }
        interactionIndicator.SetActive(true);
    }

    public void Deselect()
    {
        interactionIndicator.SetActive(false);
    }
}
