using SekiburaGames.DefaultClicker.System;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

namespace SekiburaGames.DefaultClicker.Controllers
{
    public class SaveLoadController : IInitializable
    {
        YGSaveLoadManager saveLoadManager;
        public event Action<SavesYG> LoadEvent;
        public void Initialize()
        {
            saveLoadManager = new YGSaveLoadManager();
            YandexGame.GetDataEvent += () =>
            {
                Debug.Log("GetDataEvent");
                LoadEvent.Invoke(Load());
            };
        }

        public SavesYG Load()
        {
            //Debug.Log("LOAD "+JsonUtility.ToJson(saveLoadManager.Load()).ToString());
            return saveLoadManager.Load();
        }

        public void Save(SavesYG saveData)
        {
            Debug.Log("SAVE "+JsonUtility.ToJson(saveData).ToString());
            saveLoadManager.Save(saveData);
        }
        public void Dispose()
        {
            saveLoadManager.Save();
            YandexGame.GetDataEvent -= () => Load();
        }
    }
}
