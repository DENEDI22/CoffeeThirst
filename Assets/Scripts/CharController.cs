using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class MovementLockParameters
{
    [SerializeField] internal bool lockX;

    [SerializeField] internal bool lockY;
    [SerializeField] internal bool lockZ;

    [SerializeField] [Tooltip("X - minimal value, Y - maximal Value")]
    internal Vector2 lockXValues;

    [SerializeField] [Tooltip("X - minimal value, Y - maximal Value")]
    internal Vector2 lockYValues;

    [SerializeField] [Tooltip("X - minimal value, Y - maximal Value")]
    internal Vector2 lockZValues;
}

[RequireComponent(typeof(Rigidbody))]
public class CharController : MonoBehaviour
{
    [HideInInspector] [SerializeField] public Rigidbody rb;
    [SerializeField] float rotationSmoothSpeed = 1f;
    [Header("General movement")] public float speed = 10f;
    [SerializeField] Camera m_camera;
    [SerializeField] private MovementLockParameters movementLockParameters;
    private Vector3 m_movementDirection;
    [SerializeField] private bool isCursorLocked;


    private void OnValidate()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    public void FixedUpdate()
    {
        if (m_movementDirection != Vector3.zero)
            transform.rotation = Quaternion.RotateTowards(transform.rotation,
                Quaternion.LookRotation(m_movementDirection.normalized, Vector3.up),
                rotationSmoothSpeed * Time.fixedDeltaTime);
        transform.rotation.SetLookRotation(new Vector3(0, transform.rotation.eulerAngles.y, 0));
    }

    private void Start()
    {
        if (isCursorLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void Move(Vector3 direction, float amount)
    {
        var forward = m_camera.transform.forward;
        var right = m_camera.transform.right;

        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        m_movementDirection = direction.x * right + direction.z * forward;
        m_movementDirection *= amount;
        if (movementLockParameters.lockX)
        {
            if (transform.position.x < movementLockParameters.lockXValues.x && m_movementDirection.x < 0)
            {
                m_movementDirection.x = 0;
            }
            else if (transform.position.x > movementLockParameters.lockXValues.y && m_movementDirection.x > 0)
            {
                m_movementDirection.x = 0;
            }
        }

        if (movementLockParameters.lockY)
        {
            if (transform.position.y < movementLockParameters.lockYValues.x && m_movementDirection.y < 0)
            {
                m_movementDirection.y = 0;
            }
            else if (transform.position.y > movementLockParameters.lockYValues.y && m_movementDirection.y > 0)
            {
                m_movementDirection.y = 0;
            }
        }

        if (movementLockParameters.lockZ)
        {
            if (transform.position.z < movementLockParameters.lockZValues.x && m_movementDirection.z < 0)
            {
                m_movementDirection.z = 0;
            }
            else if (transform.position.z > movementLockParameters.lockZValues.y && m_movementDirection.z > 0)
            {
                m_movementDirection.z = 0;
            }
        }

        rb.velocity = new Vector3(m_movementDirection.x, rb.velocity.y, m_movementDirection.z);
    }
}