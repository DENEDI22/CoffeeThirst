using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.InputSystem;
using UnityEngine;

[RequireComponent(typeof(CharController))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour
{

    [SerializeField]
    float rotationSmoothSpeed = 1f;

    [SerializeField] [HideInInspector] CharController player;
    [SerializeField] [HideInInspector] PlayerInput playerInput;

    Vector2 moveDir;
    Vector3 lookDir;
    float lookAngle;


    private void OnValidate()
    {
        player = gameObject.GetComponent<CharController>();
        playerInput = gameObject.GetComponent<PlayerInput>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, moveDir);
    }

    private void FixedUpdate()
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(moveDir), rotationSmoothSpeed * Time.fixedDeltaTime);
        player.Move(new Vector3(moveDir.x, 0, moveDir.y), player.speed);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveDir = context.ReadValue<Vector2>();
    }

}