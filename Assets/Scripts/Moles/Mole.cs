using UnityEngine;
using UnityEngine.Events;

namespace Moles
{
    public class Mole : MonoBehaviour
    {
        public MolesItem molesItem;
        public Transform molesItemSpawnPoint;
        public Animator moleAnims;
        public Animator molePositionAnims;
        [SerializeField] private MoleItemsPool moleItemsPool;
        [SerializeField] private float moleDeathAnimDuration;
        private bool m_holdsMoleItem = false;
        public void StartLifeCycle(float _lifeTime)
        {
            Invoke("Disappear", _lifeTime);
        }
        
        public void Appear()
        {
            molePositionAnims.SetBool("isUp", true);
            m_holdsMoleItem = true;
            molesItem = moleItemsPool.SelectMoleItem();
            Transform molesItemTransform;
            (molesItemTransform = molesItem.transform).SetParent(molesItemSpawnPoint.transform, false);
            molesItemTransform.localPosition = Vector3.zero;
            molesItem.gameObject.SetActive(true);
        }

        public void Die()
        {
            m_holdsMoleItem = false;
            molesItem.GetComponent<Rigidbody>().isKinematic = false;
            molesItem.transform.SetParent(transform.root);
            moleAnims.SetTrigger("Death");
            Invoke("Disappear", moleDeathAnimDuration);
        }

        public void Disappear()
        {
            onMoleDisappear.Invoke(this);
            Invoke("DisableMoleItem", 2 * moleDeathAnimDuration);
            molePositionAnims.SetBool("isUp", false);
            onMoleDisappear = new UnityEvent<Mole>();
        }

        private void DisableMoleItem()
        {
            if (m_holdsMoleItem) molesItem.Disable();
        }

        [HideInInspector] public UnityEvent<Mole> onMoleDisappear = new UnityEvent<Mole>();
    }
}