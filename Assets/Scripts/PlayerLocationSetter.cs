using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class PlayerLocationSetter : MonoBehaviour
    {
        private void Awake()
        {
            FindObjectOfType<LocationChanger>().SetPlayersLocation(new LocationOnTheScene
            {
                position = transform.position,
                rotation = Quaternion.identity,
                sceneName = SceneManager.GetActiveScene().name
            });
        }
    }
}