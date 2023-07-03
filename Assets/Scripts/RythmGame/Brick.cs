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

        public void Initialize(BrickParameters _parameters)
        {
            brickParameters = _parameters;
            Invoke("TimeDeath", 12);
        }

        private void TimeDeath() => Destroy(gameObject);
        
        public void FixedUpdate()
        {
            transform.Translate(Vector3.right * (brickParameters.speed * Time.fixedDeltaTime));
        }

        public void Break()
        {
            foreach (var x in brickParts)
            {
                x.isKinematic = false;
                x.AddExplosionForce(800, ExplosionPoint.position, 10);
            }
        }

        public void Miss()
        {
            
        }
    }
    [Serializable]
    public class BrickParameters
    {
        public SoundTypes soundType;
        public float speed;
    }
}