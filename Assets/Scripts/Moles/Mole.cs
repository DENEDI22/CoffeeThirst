using System.Collections.Generic;
using UnityEngine;

namespace Moles
{
    public class Mole : MonoBehaviour
    {
        public MolesItem molesItem;
        public Transform molesItemSpawnPoint;
        public Animator moleAnims;
        [SerializeField] private List<MolesItem> referenceMoleItems;
        [SerializeField] private float moleDeathAnimDuration;
        
        public void Appear()
        {
            moleAnims.SetBool("isUp", true);
        }

        public void Die()
        {
            // moleAnims.SetTrigger("Die");
            Invoke("Disappear", moleDeathAnimDuration);
        }

        public void Disappear()
        {
            Instantiate(molesItem.gameObject, molesItemSpawnPoint);
            // moleAnims.SetBool("isUp", false);
        }
    }
}