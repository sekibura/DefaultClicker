
namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;

        // Тестовые сохранения для демо сцены
        // Можно удалить этот код, но тогда удалите и демо (папка Example)
        //public int money = 1;                       // Можно задать полям значения по умолчанию
        //public string newPlayerName = "Hello!";
        //public bool[] openLevels = new bool[3];

        // Ваши сохранения

        // ...
        public float Score = 0;

        public float ScorePower = 1;
        public int ScorePowerIndex = 0;

        public int ScorePerSecond = 0;
        public int ScorePerSecondIndex = 0;

        public int CurrentBackGroundIndex = 0;
        public int OpenedBackGroundIndex = 0;

        public int CurrentCharacterIndex = 0;
        public int OpenedCharacterIndex = 0;

        public int CurrentOutfitIndex = 0;
        public int OpenedOutfitIndex = 0;

        // Поля (сохранения) можно удалять и создавать новые. При обновлении игры сохранения ломаться не должны


        // Вы можете выполнить какие то действия при загрузке сохранений
        public SavesYG()
        {
            // Допустим, задать значения по умолчанию для отдельных элементов массива
            Score = 0;

            ScorePower = 1;
            ScorePowerIndex = 0;

            ScorePerSecond = 0;
            ScorePerSecondIndex = 0;

            CurrentBackGroundIndex = 0;
            OpenedBackGroundIndex = 0;

            CurrentCharacterIndex = 0;
            OpenedCharacterIndex = 0;

            CurrentOutfitIndex = 0;
            OpenedOutfitIndex = 0;
        }
    }
}
