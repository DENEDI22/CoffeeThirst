using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class LocationChanginTrigger : MonoBehaviour, IInteractable
    {
        [SerializeField] private GameObject hint;
        [SerializeField] private string sceneName;

        public bool OnInteract()
        {
            LocationChanger _locationChanger = FindObjectOfType<LocationChanger>();
            PlayerInput player = FindObjectOfType<PlayerInput>();
            _locationChanger.SaveProperties(player.transform.position, player.transform.rotation,
                SceneManager.GetActiveScene().name);
            SceneManager.LoadScene(sceneName);
            return true;
        }

        public void Select()
        {
            hint.SetActive(true);
        }

        public void Deselect()
        {
            hint.SetActive(false);
        }
    }
}