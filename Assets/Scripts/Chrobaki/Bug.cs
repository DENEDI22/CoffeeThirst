using System;
using System.Collections;
using UnityEngine;

namespace Chrobaki
{
    public class Bug : MonoBehaviour
    {
        public Vector3 startingPoint, endingPoint;
        public float maxHeight, speed, groundLevel;
        public bool flyingInTheAir;
        [SerializeField] [HideInInspector] private ParticleSystem bugDeathParticles;
        [SerializeField] private AnimationCurve heightCurve, speedCurve;
        public Vector3 MiddleOfTheWay => new Vector3((startingPoint.x + endingPoint.x) / 2,
            (startingPoint.y + endingPoint.y) / 2, (startingPoint.z + endingPoint.z) / 2);

        public void BugDeath()
        {
            FindObjectOfType<BugSpawner>().BugDeath();
            Destroy(gameObject);
        }

        private IEnumerator BugDeathCor()
        {
            bugDeathParticles.Play();
            yield break;
        }

        private IEnumerator Fly()
        {
            yield return new WaitForSecondsRealtime(0.5f);
            heightCurve.MoveKey(0, new Keyframe(0, endingPoint.y));
            heightCurve.MoveKey(1, new Keyframe(0.4f, maxHeight));
            heightCurve.MoveKey(2, new Keyframe(1f, startingPoint.y));
            heightCurve.AddKey(new Keyframe(0.05f, endingPoint.y));
            while (flyingInTheAir)
            {
                transform.position =
                    Vector3.Lerp(new Vector3(transform.position.x, endingPoint.y, transform.position.z), endingPoint,
                        Time.fixedDeltaTime * speed * speedCurve.Evaluate(PassedDistanceMultiplier));
                transform.position = new Vector3(transform.position.x, heightCurve.Evaluate(PassedDistanceMultiplier),
                    transform.position.z);
                yield return new WaitForFixedUpdate();
            }
        }

        private float PassedDistanceMultiplier =>
            Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z),
                new Vector3(endingPoint.x, 0, endingPoint.z)) / Vector3.Distance(startingPoint, endingPoint);

        // private void OnTriggerEnter(Collider other)
        // {
        //     flyingInTheAir = false;
        // }
        //
        // private void OnCollisionEnter(Collision collision)
        // {
        //     if (collision.transform.CompareTag("Bush")) flyingInTheAir = false;
        // }
    }
}