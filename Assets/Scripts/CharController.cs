using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharController : MonoBehaviour
{

    [HideInInspector] [SerializeField] public Rigidbody rb;

    [Header("Movement")]
    public float speed = 10f;
    
    private void OnValidate()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    public void Move(Vector3 direction, float amount)
    {   
        Vector3 newVel = direction * amount;
        rb.velocity = new Vector3(newVel.x, rb.velocity.y, newVel.z);
    }

}
