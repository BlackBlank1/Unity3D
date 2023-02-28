using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

namespace TS.Commons
{
    public class AudioManager : Singleton<AudioManager>
    {
        //要替换的音乐
        [SerializeField]
        private AudioClip battleClip;

        [SerializeField]
        private AudioClip titleClip;

        [SerializeField]
        private AudioMixer audioMixer;

        private AudioSource audioSource;

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
            PlayTitleMusic();
        }


        public void PlayTitleMusic()
        {
            audioSource.clip = titleClip;
            audioSource.Play();
        }

        public void PlayBattleMusic()
        {
            audioSource.clip = battleClip;
            audioSource.Play();
        }

        public float GetMusicVolume()
        {
            audioMixer.GetFloat("MusicVolume", out var value);
            return value;
        }

        public void SetMusicVolume(float value)
        {
            audioMixer.SetFloat("MusicVolume", value);
        }

        public float GetSoundVolume()
        {
            audioMixer.GetFloat("SoundVolume", out var value);
            return value;
        }

        public void SetSoundVolume(float value)
        {
            audioMixer.SetFloat("SoundVolume", value);
        }
    }
}