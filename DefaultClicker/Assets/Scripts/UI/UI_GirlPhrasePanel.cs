using Lean.Localization;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
namespace SekiburaGames.DefaultClicker.UI
{
    public class UI_GirlPhrasePanel : MonoBehaviour
    {
        [SerializeField]
        private LeanLocalizedTextMeshProUGUI _leanLocalizedText;

        public void ShowText(string translationName, float duration)
        {
            _leanLocalizedText.TranslationName = translationName;
            StartCoroutine(ShowText(duration));
        }

        private IEnumerator ShowText(float duration)
        {
            StartFadeIn();
            yield return new WaitForSeconds(duration);
            StartFadeOut();
        }

        private float fadeDuration = 0.5f; // длительность плавного перехода, в секундах

        private CanvasGroup canvasGroup;
        private float fadeTimer;
        private bool isFading;
        private bool isFadingIn;

        private void Start()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public void StartFadeIn()
        {
            fadeTimer = 0f;
            isFading = true;
            isFadingIn = true;
        }

        public void StartFadeOut()
        {
            fadeTimer = 0f;
            isFading = true;
            isFadingIn = false;
        }

        private void Update()
        {
            if (isFading)
            {
                fadeTimer += Time.deltaTime;

                if (fadeTimer <= fadeDuration)
                {
                    if (isFadingIn)
                    {
                        // Плавно делаем объект непрозрачным
                        float alpha = Mathf.Lerp(0f, 1f, fadeTimer / fadeDuration);
                        canvasGroup.alpha = alpha;
                    }
                    else
                    {
                        // Плавно делаем объект прозрачным
                        float alpha = Mathf.Lerp(1f, 0f, fadeTimer / fadeDuration);
                        canvasGroup.alpha = alpha;
                    }
                }
                else
                {
                    // Объект полностью непрозрачный или прозрачный, останавливаем плавное изменение
                    isFading = false;

                    if (isFadingIn)
                    {
                        canvasGroup.alpha = 1f;
                    }
                    else
                    {
                        canvasGroup.alpha = 0f;
                    }
                }
            }
        }
    }
}
