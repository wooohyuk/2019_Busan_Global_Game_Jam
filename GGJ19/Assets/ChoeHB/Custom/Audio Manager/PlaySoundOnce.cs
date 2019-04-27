using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnce : MonoBehaviour {

    public bool isEnabled = true;

    [SerializeField] AudioClip clip;
    public void Play()
    {
        if(isEnabled)
            AudioManager.PlaySound(clip);
    }

    public void PlayAsClip(AudioClip clip)
    {
        if (isEnabled)
            AudioManager.PlaySound(clip);
    }

    public void PlayAsClipName(string clipName)
    {
        if(isEnabled)
            AudioManager.PlaySound(clipName);
    }

}
