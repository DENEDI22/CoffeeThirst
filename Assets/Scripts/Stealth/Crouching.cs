using UnityEngine;
using UnityEngine.InputSystem;

public class Crouching : MonoBehaviour
{

    [SerializeField] private CharController player;
    [SerializeField] private float playerNormalSpeed, playerCrouchingSpeed;
    [SerializeField] private Animator playerAnimator;
    
    public void ChangeCrouchState(InputAction.CallbackContext _ctx)
    {
        CapsuleCollider capsuleCollider = player.GetComponent<CapsuleCollider>();
        var colliderhight = capsuleCollider.height;
        if (_ctx.started)
        {
            player.speed = playerCrouchingSpeed;
            playerAnimator.SetBool("isCrouching", true);
            capsuleCollider.center -= new Vector3(0, (colliderhight - colliderhight * 0.75f) * 0.5f, 0);
            capsuleCollider.height *= 0.75f;
        }
        else if(_ctx.canceled)
        {
            player.speed = playerNormalSpeed;
            playerAnimator.SetBool("isCrouching", false);
            capsuleCollider.center += new Vector3(0, (colliderhight / 0.75f - colliderhight) * 0.5f, 0);
            capsuleCollider.height /= 0.75f;
        }
    }
}
