using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Moles
{
    public class MoleItemsPool : MonoBehaviour
    {
        [SerializeField] private List<MolesItem> moleItemsVariants;
        [SerializeField] private List<MolesItem> inUseMoleItems, freeMoleItems;
        [SerializeField] [Range(1, 10)] private int poolSize;

        private void Awake()
        {
            for (int i = 0; i < poolSize; i++)
                freeMoleItems.Add(Instantiate(moleItemsVariants[Random.Range(0, moleItemsVariants.Count)].gameObject,
                    transform).GetComponent<MolesItem>());
        }

        public void ReturnToPool(MolesItem _itemToReturn)
        { 
            freeMoleItems.Add(_itemToReturn);
            inUseMoleItems.Remove(_itemToReturn);
            _itemToReturn.transform.position = Vector3.zero;
            _itemToReturn.transform.rotation = Quaternion.identity;
            _itemToReturn.transform.SetParent(transform);
            _itemToReturn.gameObject.SetActive(false);
        }

        public MolesItem SelectMoleItem()
        {
            MolesItem selectMoleItem = freeMoleItems[Random.Range(0, freeMoleItems.Count-1)];
            freeMoleItems.Remove(selectMoleItem);
            inUseMoleItems.Add(selectMoleItem);
            return selectMoleItem;
        }
    }
}