using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace Moles
{
    public class Mole : MonoBehaviour
    {
        public MolesItem molesItem;
        public Transform molesItemSpawnPoint;
        public Animator moleAnims;
        [SerializeField] private List<MolesItem> referenceMoleItems;
        
        public void Appear()
        {
            moleAnims.SetBool("isUp", true);
        }

        public void Disappear() 
        {
            Instantiate(molesItem.gameObject, molesItemSpawnPoint);
            moleAnims.SetBool("isUp", false);
        }
    }
}