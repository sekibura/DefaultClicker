using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;
namespace SekiburaGames.DefaultClicker.UI
{
    public class DebugPanel : MonoBehaviour
    {
        [SerializeField]
        private Button _resetSavesButton;

        private void Start()
        {
            _resetSavesButton.onClick.AddListener(() =>
            {
                YandexGame.ResetSaveProgress();
            });
        }

    }
}
