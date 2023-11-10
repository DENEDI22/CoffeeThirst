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

    [Header("General movement")] public float speed = 10f;
    [SerializeField] private MovementLockParameters movementLockParameters;


    private void OnValidate()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    public void Move(Vector3 direction, float amount)
    {
        Vector3 newVel = direction * amount;
        if (movementLockParameters.lockX)
        {
            if (transform.position.x < movementLockParameters.lockXValues.x && newVel.x < 0)
            {
                newVel.x = 0;
            }
            else if (transform.position.x > movementLockParameters.lockXValues.y && newVel.x > 0)
            {
                newVel.x = 0;
            }
        }

        if (movementLockParameters.lockY)
        {
            if (transform.position.y < movementLockParameters.lockYValues.x && newVel.y < 0)
            {
                newVel.y = 0;
            }
            else if (transform.position.y > movementLockParameters.lockYValues.y && newVel.y > 0)
            {
                newVel.y = 0;
            }
        }

        if (movementLockParameters.lockZ)
        {
            if (transform.position.z < movementLockParameters.lockZValues.x && newVel.z < 0)
            {
                newVel.z = 0;
            }
            else if (transform.position.z > movementLockParameters.lockZValues.y && newVel.z > 0)
            {
                newVel.z = 0;
            }
        }

        rb.velocity = new Vector3(newVel.x, rb.velocity.y, newVel.z);
    }
}