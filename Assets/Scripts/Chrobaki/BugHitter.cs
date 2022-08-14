using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Chrobaki
{
    [RequireComponent(typeof(AudioSource))]
    public class BugHitter : MonoBehaviour
    {
        public bool hit;
        public bool canHit = true;
        public Animator playerAnimator;

        private void CoolDown() => canHit = true;

        public List<GameObject> allObjectsInCollider = new List<GameObject>();
        [HideInInspector] [SerializeField] private AudioSource hitSoundSource;
        [SerializeField] private float hitCooldownTime = 1f;

        private void OnValidate()
        {
            hitSoundSource = GetComponent<AudioSource>();
        }

        public void HitButton(InputAction.CallbackContext _ctx)
        {
            if (!_ctx.started | !canHit) return;
            playerAnimator.SetTrigger("Hit");
            for (var index = 0; index < allObjectsInCollider.Count; index++)
            {
                var variable = allObjectsInCollider[index];
                if (variable == null)
                {
                    allObjectsInCollider.Remove(variable);
                    
                }
                else if (variable.CompareTag("Matka"))
                {
                    variable.GetComponent<BugMother>().Hit(10f);
                    hitSoundSource.PlayDelayed(0.25f);
                }
                else if (variable.CompareTag("Bush"))
                {
                    variable.GetComponent<Bush>().StopHitting();
                }
            }

            canHit = false;
            Invoke("CoolDown", hitCooldownTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            allObjectsInCollider.Add(other.gameObject);
        }

        private void OnTriggerExit(Collider other)
        {
            allObjectsInCollider.Remove(other.gameObject);
        }
    }
}