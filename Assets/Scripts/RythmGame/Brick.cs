using System;
using System.Collections.Generic;
using UnityEngine;

namespace RythmGame
{
    public class Brick : MonoBehaviour
    {
        [SerializeField] public BrickParameters brickParameters;
        [SerializeField] private Transform ExplosionPoint;
        [SerializeField] private List<Rigidbody> brickParts;
        [HideInInspector] public bool isBroken = false;
        [SerializeField] private ParticleSystem flinders;
        public void Initialize(BrickParameters _parameters)
        {
            brickParameters = _parameters;
            Invoke("TimeDeath", 2.8f);
        }

        private void TimeDeath() => Destroy(gameObject);
        
        public void FixedUpdate()
        {
            transform.Translate(Vector3.right * (brickParameters.speed * Time.fixedDeltaTime));
        }

        public void Break()
        {
            isBroken = true;
            flinders.Play();
            foreach (var x in brickParts)
            {
                x.isKinematic = false;
                x.GetComponent<Collider>().enabled = true;
                x.AddExplosionForce(800, ExplosionPoint.position, 10);
            }
        }
    }
    
    [Serializable]
    public class BrickParameters
    {
        public SoundTypes soundType;
        public float speed;
    }
}