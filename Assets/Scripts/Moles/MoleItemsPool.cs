using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Moles
{
    public class MoleItemsPool : MonoBehaviour
    {
        [SerializeField] private List<MolesItem> moleItemsVariants;
        [SerializeField] private List<MolesItem> inUseMoleItems, freeMoleItems;
        [SerializeField] [Range(1, 50)] private int poolSize;

        private void Awake()
        {
            for (int i = 0; i < poolSize; i++)
                freeMoleItems.Add(Instantiate(moleItemsVariants[Random.Range(0, moleItemsVariants.Count)].gameObject,
                    transform).GetComponent<MolesItem>()); //suffer bthchs
        }

        internal void ReturnToPool(MolesItem _itemToReturn)
        {
            freeMoleItems.Add(_itemToReturn);
            inUseMoleItems.Remove(_itemToReturn);
        }

        public MolesItem SelectMoleItem()
        {
            MolesItem selectMoleItem = freeMoleItems[Random.Range(0, freeMoleItems.Count)];
            freeMoleItems.Remove(selectMoleItem);
            return selectMoleItem;
        }
    }
}