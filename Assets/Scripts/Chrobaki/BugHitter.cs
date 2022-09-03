using System.Collections;
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

        private IEnumerator HitProcedure(GameObject _hitObject)
        {
            yield return new WaitForSeconds(0.25f);
            if (_hitObject == null)
            {
                allObjectsInCollider.Remove(_hitObject);
                    
            }
            else if (_hitObject.CompareTag("Matka"))
            {
                _hitObject.GetComponent<BugMother>().Hit(10f);
                hitSoundSource.Play();
            }
            else if (_hitObject.CompareTag("Bush"))
            {
                _hitObject.GetComponent<Bush>().StopHitting();
            }
        }
        
        public void HitButton(InputAction.CallbackContext _ctx)
        {
            if (!_ctx.started | !canHit) return;
            playerAnimator.SetTrigger("Hit");
            for (var index = 0; index < allObjectsInCollider.Count; index++)
            {
                var variable = allObjectsInCollider[index];
                StartCoroutine("HitProcedure", variable);
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