using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSelector : MonoBehaviour {

    [SerializeField] PlayMusic[] musics;
    [SerializeField] int index;

    private void Awake()
    {
        musics[index].gameObject.SetActive(true);
    }

}
