using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusicOnStart : MonoBehaviour
{
    [SerializeField] private AudioSource m_audioSource;

    private void OnValidate()
    {
        m_audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // Invoke("PlayAfterDealay", 0f);
    }

    private void PlayAfterDealay()
    {
        m_audioSource.Play();
    }
}