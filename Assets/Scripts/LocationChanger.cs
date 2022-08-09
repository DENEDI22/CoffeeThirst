using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class LocationOnTheScene
{
    public Vector3 position;
    public Quaternion rotation;
    public string sceneName;
}

public class LocationChanger : MonoBehaviour
{
    public Transform player;
    public List<LocationOnTheScene> locationsSaved = new List<LocationOnTheScene>();

    public void SaveProperties(Vector3 _position, Quaternion _rotation, string _sceneName)
    {
        try
        {
            locationsSaved.Find(x => x.sceneName == _sceneName).position = _position;
            locationsSaved.Find(x => x.sceneName == _sceneName).rotation = _rotation;
        }
        catch (Exception e)
        {
            locationsSaved.Add(new LocationOnTheScene
            {
                position = _position,
                rotation = _rotation,
                sceneName = _sceneName
            });
        }
    }

    public void SetPlayersLocation([CanBeNull] LocationOnTheScene _defaultLocation)
    {
        try
        {
            FindPlayer();
            LocationOnTheScene _location = locationsSaved.Find(x => x.sceneName == SceneManager.GetActiveScene().name);
            player.SetPositionAndRotation(_location.position, _location.rotation);
        }
        catch (Exception e)
        {
            player.SetPositionAndRotation(_defaultLocation.position, _defaultLocation.rotation);
            Console.WriteLine("No saved locaiton for this scene");
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }


    public void FindPlayer() => player = FindObjectOfType<PlayerInput>().transform;
}