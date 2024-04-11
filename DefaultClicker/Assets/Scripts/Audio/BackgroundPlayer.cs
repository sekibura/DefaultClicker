using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            PlayNextClip();
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