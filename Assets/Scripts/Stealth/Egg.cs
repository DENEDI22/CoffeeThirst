using System;
using UnityEngine;

public class Egg : MonoBehaviour, IInteractable
{
    private bool m_isTaken;
    [SerializeField] private GameObject interactionIndicator;
    [SerializeField] private Transform playersHandPivotPoint;
    public bool OnInteract()
    {
        if (m_isTaken || playersHandPivotPoint.childCount != 0) return false;
        transform.SetParent(playersHandPivotPoint);
        transform.localPosition = Vector3.zero;
        m_isTaken = true;
        return true;
    }

    public void Select()
    {
        if(m_isTaken) return;
        interactionIndicator.SetActive(true);
    }

    public void Deselect()
    {
        interactionIndicator.SetActive(false);
    }
}
