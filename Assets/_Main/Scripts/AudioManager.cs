using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundClips
{
    Shoot,
    Steps,
    RunningSteps,
    UIPopUp,
    InteractableClick,
    SlimeWalk,
    SlimeGetHit,
    GoldenSlime,
    AttackSound,
    PlayerTakesDamage,
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Music")]
    [SerializeField] private AudioSource musicAudioSource;
    [SerializeField, Range(0, 1)] private float musicInitialVolumen;
    [SerializeField] private AudioClip music;

    [Header("Sounds")]
    [SerializeField] private AudioSource soundsAudioSource;
    [SerializeField] private AudioClip shoot;
    [SerializeField] private List<AudioClip> footStepsSounds;
    [SerializeField] private AudioClip attackSound;
    [SerializeField] private AudioClip playerTakesDamage;
    [SerializeField] private AudioClip slimeWalkSound;
    [SerializeField] private AudioClip slimeGetHit;
    [SerializeField] private AudioClip goldenSlime;

    [Header("UI Sounds")]
    [SerializeField] private AudioClip interactableClick;

    public void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }

        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        musicAudioSource.volume = musicInitialVolumen;
        musicAudioSource.clip = music;
        //musicAudioSource.Play(); //TODO: Poner musica
    }

    public void PlaySound(SoundClips soundClip)
    {
        switch (soundClip)
        {
            case SoundClips.Shoot:
                soundsAudioSource.volume = 0.5f;
                soundsAudioSource.PlayOneShot(shoot);
                break;

            case SoundClips.Steps:
                soundsAudioSource.volume = 0.3f;
                PlaySoundSteps();
                break;      
                
            case SoundClips.RunningSteps:
                soundsAudioSource.volume = 0.3f;
                PlaySoundSteps();
                break;

            case SoundClips.AttackSound:
                soundsAudioSource.volume = 0.5f;
                soundsAudioSource.PlayOneShot(attackSound);
                break;

            case SoundClips.PlayerTakesDamage:
                soundsAudioSource.volume = 1f;
                soundsAudioSource.PlayOneShot(playerTakesDamage);
                break;

            case SoundClips.SlimeGetHit:
                soundsAudioSource.volume = 1f;
                soundsAudioSource.PlayOneShot(slimeGetHit);
                break;

            case SoundClips.SlimeWalk:
                soundsAudioSource.volume = 1f;
                soundsAudioSource.PlayOneShot(slimeWalkSound);
                break;

            case SoundClips.GoldenSlime:
                soundsAudioSource.volume = 1f;
                soundsAudioSource.PlayOneShot(goldenSlime);
                break;

            case SoundClips.InteractableClick:
                soundsAudioSource.volume = 1f;
                soundsAudioSource.PlayOneShot(interactableClick);
                break;

            default:
                break;
        }
    }

    private void PlaySoundSteps()
    {
        int randomStep = Random.Range(0, footStepsSounds.Count);
        soundsAudioSource.PlayOneShot(footStepsSounds[randomStep]);
    }
}
