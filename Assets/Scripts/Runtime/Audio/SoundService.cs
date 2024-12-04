using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

public class SoundService : MonoBehaviour
{
    //[SerializeField] GameSettings gameSettings;

    [SerializeField] int maxSounds = 10;
    [SerializeField] float soundsIntervalSec = 0.1f;
    [SerializeField] AudioMixerGroup mixerGroup;
    [SerializeField] AudioSource ambient;

    List<AudioSource> audioSourcesCache = new();
    //public GameSettings GameSettings => gameSettings;

    public AudioSource PlaySound(AudioClip clip, Vector3 position, AudioMixerGroup customMixerGroup = null)
    {
        var viewPortPos = Camera.main.WorldToViewportPoint(position);

        if (viewPortPos.x > 0.0f && viewPortPos.x < 1.0f && viewPortPos.y > 0.0f && viewPortPos.y < 1.0f)
        {
            AudioSource emptyAudioSource = null;
            int playingSameClip = 0;
            foreach (var audioSource in audioSourcesCache)
            {
                // Length can be equal only when clip the same
                if (audioSource.isPlaying && Mathf.Approximately(audioSource.clip.length, clip.length) && audioSource.time < soundsIntervalSec)
                    playingSameClip++;

                if (!audioSource.isPlaying)
                    emptyAudioSource = audioSource;
            }

            if (playingSameClip < 5 && emptyAudioSource != null)
            {
                //emptyAudioSource.volume = gameSettings.SoundVolumePercent() / 100.0f;
                emptyAudioSource.volume = 100 / 100.0f;

                emptyAudioSource.clip = clip;
                emptyAudioSource.Play();
                emptyAudioSource.transform.position = position;
                emptyAudioSource.outputAudioMixerGroup = customMixerGroup == null ? mixerGroup : customMixerGroup;
            }

            return emptyAudioSource;
        }
        return null;
    }

    void OnSettingsChanged()
    {
       //ambient.volume = gameSettings.SoundVolumePercent() / 100.0f;
       ambient.volume = 100 / 100.0f;
    }

    void OnEnable()
    {
        //gameSettings.settingsChangedEvent += OnSettingsChanged;
        ambient.volume = 100 / 100.0f;
    }

    void OnDisable()
    {
        //gameSettings.settingsChangedEvent -= OnSettingsChanged;
    }

    public void PlaySound(IReadOnlyList<AudioClip> clips, Vector3 position)
    {
        if (clips.Any())
            PlaySound(clips[Random.Range(0, clips.Count)], position);
    }

    void Awake()
    {
        for (int i = 0; i < maxSounds; i++)
        {
            var obj = new GameObject("AudioSource");
            obj.transform.SetParent(transform);
            var source = obj.AddComponent<AudioSource>();
            source.loop = false;
            audioSourcesCache.Add(source);
        }
    }
}
