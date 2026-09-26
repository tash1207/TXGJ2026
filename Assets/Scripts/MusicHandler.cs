using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicHandler : MonoBehaviour 
{
    public static MusicHandler Instance {get; private set;}

    public AudioSource musicSource;
    public AudioClip gameMusic;
    public AudioClip endingMusic;
    

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        } 
        else
        {
            Destroy(gameObject);
        }

        musicSource = GetComponent<AudioSource>();
        gameMusic = Resources.Load<AudioClip>("Audio/final-game-music");
        endingMusic = Resources.Load<AudioClip>("Audio/final-end-song");
    }

    private void Start()
    {
        if(gameMusic != null)
        {
            PlayMusic(gameMusic);
        }
    }

    public void PlayMusic(AudioClip clip)
    {
        if (musicSource == null || clip == null) return;

        if (musicSource.clip == clip && musicSource.isPlaying) return;

        musicSource.Stop();
        musicSource.clip = clip;
        musicSource.Play();
    }

    public void PlayEndingMusic()
    {
        PlayMusic(endingMusic);
    }
}
