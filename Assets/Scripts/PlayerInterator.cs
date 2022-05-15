using UnityEngine;

public class PlayerInterator : MonoBehaviour
{
    public IInteractable selection;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IInteractable _interface))
        {
            selection.Deselect();
            selection = _interface;
            selection.Select();
            Debug.Log(other.name + " selected.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out IInteractable _interface) && _interface == selection)
        {
            selection.Deselect();
        }
    }
}