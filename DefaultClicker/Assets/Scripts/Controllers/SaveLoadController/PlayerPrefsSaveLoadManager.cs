using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SekiburaGames.DefaultClicker.Controllers
{
    public class PlayerPrefsSaveLoadManager : BaseSaveLoadManager
    {
        public override PlayerProgressData Load()
        {
            if (currentProgressData != null)
                return currentProgressData;

            string progressDataString = PlayerPrefs.GetString("PlayerData", "");
            PlayerProgressData progressData;
            try
            {
                progressData = (PlayerProgressData)JsonUtility.FromJson(progressDataString, typeof(PlayerProgressData));
            }
            catch(Exception e)
            {
                Debug.LogException(e);
                progressData = new PlayerProgressData();
            }
            
            currentProgressData = progressData;
            return progressData;
        }

        public override void Save(PlayerProgressData progressData)
        {
            string progressDataString = JsonUtility.ToJson(progressData);
            PlayerPrefs.SetString("PlayerData", progressDataString);
            currentProgressData = progressData;
        }
    }
}
