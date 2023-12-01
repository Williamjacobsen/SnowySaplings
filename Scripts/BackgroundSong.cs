using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSong : MonoBehaviour
{
    AudioSource audioData;

    void Start()
    {
        audioData = GetComponent<AudioSource>();
        audioData.Play(0);
    }
}
