using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    bool playingBasementMusic;
    bool playingFireMusic;

    float defaultVolume;

    [SerializeField]
    AudioClip basementOneShot;

    [SerializeField]
    AudioClip basementTheme;

    [SerializeField]
    AudioClip mainThemeOneShot;

    [SerializeField]
    AudioClip mainThemeLoop;

    [SerializeField]
    AudioClip eveningTheme;

    [SerializeField]
    AudioClip SleepTheme;

    AudioSource audioSource;
    SoundTransition sTrans;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        sTrans = GetComponent<SoundTransition>();

        defaultVolume = audioSource.volume;

        Teleporter.onPlayerTeleport += BasementMusic;
        OnClickItems.onBedClicked += PlaySleepTheme;
    }

    void OnDestroy()
    {
        Teleporter.onPlayerTeleport -= BasementMusic;
        OnClickItems.onBedClicked -= PlaySleepTheme;
    }

    void Update()
    {
        FireStartMusic();
        BasementLoop();
    }
    
    private void PlaySleepTheme()
    {
        audioSource.clip = SleepTheme;
        audioSource.Play();
    }

    private void FireStartMusic()
    {
        if (Fire.flameCount > 0 && !playingBasementMusic && (audioSource.volume == 0 || !audioSource.isPlaying) && !playingFireMusic)
        {
            audioSource.clip = mainThemeOneShot;
            audioSource.Play();
            sTrans.TransitionToFullVolume(defaultVolume);
            playingFireMusic = true;
        }
        else if (playingFireMusic && !audioSource.isPlaying)
        {
            audioSource.clip = mainThemeLoop;
            audioSource.loop = true;
            audioSource.Play();
        }
        else if (playingFireMusic && audioSource.isPlaying && Fire.flameCount <= 0)
        {
            sTrans.TransitionToZeroVolume(defaultVolume);
            playingFireMusic = false;
            audioSource.loop = false;
        }
    }

    private void BasementLoop()
    {
        if (playingBasementMusic && !audioSource.isPlaying)
        {
            audioSource.clip = basementTheme;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    private void BasementMusic()
    {
        if(!playingBasementMusic)   PlayBasementTheme();
        else                        StopBasementTheme();

        playingBasementMusic = !playingBasementMusic;
    }

    private void PlayBasementTheme()
    {
        playingFireMusic = false;
        audioSource.clip = basementOneShot;
        audioSource.Play();
        sTrans.TransitionToFullVolume(defaultVolume);
    }

    private void StopBasementTheme()
    {
        sTrans.TransitionToZeroVolume(defaultVolume);
        audioSource.loop = false;
    }
}
