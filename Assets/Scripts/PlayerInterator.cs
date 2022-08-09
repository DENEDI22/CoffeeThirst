using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInterator : MonoBehaviour
{
    public IInteractable selection;

    public void Interact(InputAction.CallbackContext _ctx)
    {
        if (_ctx.performed)
        {
            if(selection.OnInteract())
            {
                if (selection != null) selection.Deselect();
                selection = null;
            }
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IInteractable _interface))
        {
            if (selection != null) selection.Deselect();
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