using UnityEngine.InputSystem;
using UnityEngine;

[RequireComponent(typeof(CharController))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float rotationSmoothSpeed = 1f;
    [SerializeField] private Animator characterBodyAnimator;

    [SerializeField] [HideInInspector] CharController player;
    [SerializeField] [HideInInspector] PlayerInput playerInput;
    

    Vector2 moveDir;
    Vector3 lookDir() => new Vector3(moveDir.x, 0, moveDir.y);
    float lookAngle;
    [SerializeField] private float jumpForce;
    private static readonly int Jump1 = Animator.StringToHash("Jump");
    private static readonly int Walking = Animator.StringToHash("Walking");
    private bool canJump = true;
    [SerializeField] private float jumpCooldown = 0.1f;

    private void OnValidate()
    {
        player = gameObject.GetComponent<CharController>();
        playerInput = gameObject.GetComponent<PlayerInput>();
    }

    private void OnDrawGizmos()
    {
        var position = transform.position;
        Gizmos.DrawLine(position, new Vector3(position.x + moveDir.x, position.y, position.z + moveDir.y));
    }

    private void FixedUpdate()
    {
        if (moveDir != Vector2.zero)
            transform.rotation = Quaternion.RotateTowards(transform.rotation,
                Quaternion.LookRotation(GetComponent<Rigidbody>().velocity.normalized, Vector3.up), rotationSmoothSpeed * Time.fixedDeltaTime);
        player.Move(new Vector3(moveDir.x, 0, moveDir.y), player.speed);
        characterBodyAnimator.SetBool(Walking, moveDir != Vector2.zero);
    }

    private void JumpCooldown() => canJump = true;

    private bool CheckFloor()
    {
        Physics.Raycast(transform.position, Vector3.down * 0.01f, out RaycastHit _hit);
        return _hit.collider.CompareTag("Floor");
    }
    public void Jump(InputAction.CallbackContext _ctx)
    {
        if (_ctx.performed && canJump && CheckFloor())
        {
            player.rb.AddForce(Vector3.up * jumpForce);
            characterBodyAnimator.SetTrigger(Jump1);
            canJump = false;
            Invoke("JumpCooldown", jumpCooldown);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveDir = context.ReadValue<Vector2>();
    }
}