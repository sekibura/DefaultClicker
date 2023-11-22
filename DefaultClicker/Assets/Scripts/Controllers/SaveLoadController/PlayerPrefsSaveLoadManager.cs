using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

namespace SekiburaGames.DefaultClicker.Controllers
{
    public class PlayerPrefsSaveLoadManager : BaseSaveLoadManager
    {
        public override void Init()
        {
        }

        public override SavesYG Load()
        {
            if (currentProgressData != null)
                return currentProgressData;

            string progressDataString = PlayerPrefs.GetString("PlayerData", "");
            SavesYG progressData;
            try
            {
                progressData = (SavesYG)JsonUtility.FromJson(progressDataString, typeof(PlayerProgressData));
            }
            catch(Exception e)
            {
                Debug.LogException(e);
                progressData = new SavesYG();
            }
            
            currentProgressData = progressData;
            return progressData;
        }

        public override void Save(SavesYG progressData)
        {
            string progressDataString = JsonUtility.ToJson(progressData);
            PlayerPrefs.SetString("PlayerData", progressDataString);
            currentProgressData = progressData;
        }
    }
}
