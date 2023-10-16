using SekiburaGames.DefaultClicker.ShopItems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemBackground : BaseShopItem
{
    [SerializeField]
    private Button _button;
    protected override void OnBuyAction()
    {

    }

    protected override void SetAvaiable(bool Avaiable)
    {
        _button.interactable = Avaiable;
    }
}
