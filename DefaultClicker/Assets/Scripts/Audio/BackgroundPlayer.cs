using SekiburaGames.DefaultClicker.System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using YG;

namespace SekiburaGames.DefaultClicker.Controllers
{
    public class BackgroundPlayer : MonoBehaviour
    {
        public List<AudioClip> audioClips; // список аудиофайлов, заданный в инспекторе
        private AudioSource audioSource;

        private int currentClipIndex = 0;

        private float startVolume = 0.1f; // начальная громкость
        private float fadeTime = 1.0f; // время нарастания и затухания звука

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
            audioClips.Shuffle();

            //YandexGame.OpenFullAdEvent += PauseMusic;
            //YandexGame.CloseFullAdEvent += PlayMusic;
            //YandexGame.OpenVideoEvent += PauseMusic;
            //YandexGame.CloseVideoEvent += PlayMusic;
            //YandexGame.onHideWindowGame += PauseMusic;
            //YandexGame.onShowWindowGame += PlayMusic;
            //YandexGame.ErrorFullAdEvent += PlayMusic;
            //YandexGame.ErrorVideoEvent += PlayMusic;
            SystemManager.Get<ClickerGameController>().OnClickEvent += PlayMusic;


   

            PlayNextClip();
        }

        private void PauseMusic()
        {
            //audioSource.Pause();
            AudioListener.pause = true;
            Debug.Log($"AudioListener.pause = {AudioListener.pause} - audioSource.isPlaying = {audioSource.isPlaying}");
        }

        private void PlayMusic()
        {
           // AudioListener.pause = false;

            //if (!audioSource.isPlaying)
            //    audioSource.UnPause();
            Debug.Log($"AudioListener.pause = {AudioListener.pause} - audioSource.isPlaying = {audioSource.isPlaying}");
            StartCoroutine(ChangeMusicState(true));        
        }

        IEnumerator ChangeMusicState(bool state)
        {
            yield return new WaitForSeconds(2);

            if (state)
            {
                AudioListener.pause = false;

                if (!audioSource.isPlaying)
                    audioSource.UnPause();
            }
            else
            { 
                audioSource.Pause();
                AudioListener.pause = true;
            }

            if(!audioSource.isPlaying)
                audioSource.Play();
            Debug.Log($"AudioListener.pause = {AudioListener.pause}"); 

        }

        public static void DisplayList(List<AudioClip> list)
        {
            string s = "";
            foreach (var item in list)
            {
                s += $"{item.name} ";
            }
            Debug.Log(s);
        }

        private void PlayNextClip()
        {
            if (audioClips.Count == 0) return;

            audioSource.clip = audioClips[currentClipIndex];
            StartCoroutine(PlayWithFade());
            currentClipIndex = (currentClipIndex + 1) % audioClips.Count;
        }

        private IEnumerator PlayWithFade()
        {
            audioSource.volume = 0f;
            audioSource.Play();

            float timer = 0f;
            while (timer < fadeTime)
            {
                timer += Time.deltaTime;
                audioSource.volume = Mathf.Lerp(0f, startVolume, timer / fadeTime);
                yield return null;
            }

            yield return new WaitForSeconds(audioSource.clip.length - fadeTime * 2);

            timer = 0f;
            while (timer < fadeTime)
            {
                timer += Time.deltaTime;
                audioSource.volume = Mathf.Lerp(startVolume, 0f, timer / fadeTime);
                yield return null;
            }

            audioSource.Stop();
            PlayNextClip();
        }


    }
}