using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMusic : MonoBehaviour
{
    [SerializeField] AudioClip sceneDefaultMusic;

    void Start() 
    {
        Game.MusicService.SetMusicTrack(sceneDefaultMusic); 
    }
}
