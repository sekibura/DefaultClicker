using SekiburaGames.DefaultClicker.Controllers;
using SekiburaGames.DefaultClicker.System;
using UnityEngine;
using UnityEngine.UI;

namespace SekiburaGames.DefaultClicker.UI
{
    public class UI_MainClickObject : MonoBehaviour
    {
        [SerializeField]
        private Button _btnMainClickButton;
        private ClickerGameController _gameController;

        [SerializeField]
        private Animator _animator;

        private void Start()
        {
            _gameController = SystemManager.Get<ClickerGameController>();
            _btnMainClickButton.onClick.AddListener(()=> 
            {
                _gameController.OnClick();
                _animator.Play("GirlClickAnimnation");
                SoundManager.instance.PlaySound(SoundManager.Sound.Click);
            }
            );
        
        }
    }
}
