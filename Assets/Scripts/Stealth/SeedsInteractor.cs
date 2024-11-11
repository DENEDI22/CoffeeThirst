using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Stealth
{
    public class SeedsInteractor : MonoBehaviour, IInteractable
    {
        [SerializeField] private GameObject interactionIndicator;
        [SerializeField] private bool isReadyToUse;
        [SerializeField] private float cooldownTime = 10f;
        [SerializeField] private Image cooldownCircle;
        
        
        public bool OnInteract()
        {
            if (!isReadyToUse) return false;
            FindObjectsOfType<ChickenNavigation>().ToList().ForEach(x => x.Distract());
            return true;
        }

        private IEnumerator Cooldown()
        {
            for (float i = 0; i < cooldownTime; i+=0.1f)
            {
                
                
                yield return new WaitForSeconds(0.1f);
            }
            yield break;
        }
        
        public void Select()
        {
            interactionIndicator.SetActive(true);
        }

        public void Deselect()
        {
            interactionIndicator.SetActive(false);
        }
    }
}