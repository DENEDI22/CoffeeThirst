using UnityEngine;
using UnityEngine.InputSystem;

namespace Moles
{
    public class LinerPlayerMoving : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private Animator playerAnimator;
        [SerializeField] private Rigidbody playerRigidbody;
        
        public void MovePlayer(InputAction.CallbackContext _ctx)
        {
            var movementVector = _ctx.ReadValue<Vector2>().normalized;
            playerRigidbody.velocity = movementVector * speed;
            if (movementVector.magnitude > 0)
            {
                playerAnimator.SetBool("StrafeMoving", true);
            }
            else
            {
                playerAnimator.SetBool("StrafeMoving", false);
            }
        }
    }
}