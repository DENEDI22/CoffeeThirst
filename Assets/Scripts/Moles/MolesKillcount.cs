using TMPro;
using UnityEngine;

namespace Moles
{
    public class MolesKillcount : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI killcountMonitor;
        private int killcount;

        public void MoleDied()
        {
            killcount++;
            killcountMonitor.text = killcount.ToString() + "moles killed";
        }
    }
}