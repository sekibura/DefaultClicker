using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace SekiburaGames.DefaultClicker.UI
{
    public class UI_LanguageChooseController : MonoBehaviour
    {
        [SerializeField]
        private LanguageButton[] _languageButtons;

        private void Start()
        {
            InitButtons();
            UpdateButtonStates();
        }

        private void InitButtons()
        {
            foreach (var btn in _languageButtons)
            {
                btn._btn.onClick.AddListener(() =>
                {
                    string language = btn._language;
                    Lean.Localization.LeanLocalization.SetCurrentLanguageAll(language);
                    UpdateButtonStates();
                    Debug.Log($"Set language {language}");
                });
            }
        }

        public void UpdateButtonStates()
        {
            string currentLanguage = Lean.Localization.LeanLocalization.GetFirstCurrentLanguage();
            if (!string.IsNullOrEmpty(currentLanguage))
            {
                foreach (var btn in _languageButtons)
                {
                    btn.SetSelected(btn._language == currentLanguage);
                }
            }
        }


        [Serializable]
        private class LanguageButton
        {
            public Button _btn;
            public CanvasGroup _selectedCanvas;
            public string _language;          
            
            public void SetSelected(bool isSelected)
            {
                _selectedCanvas.alpha = isSelected ? 1 : 0;
            }
        }
    }
}
