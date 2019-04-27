using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusic : MonoBehaviour {

    [SerializeField] AudioClip music;

    private void Awake()
    {
        AudioManager.PlayMusic(music);
    }

}
