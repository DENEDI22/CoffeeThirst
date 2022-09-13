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
            playerRigidbody.velocity = _ctx.ReadValue<Vector2>().normalized * speed;
        }

        public void Aim(InputAction.CallbackContext _ctx)
        {
            
        }
    }
}