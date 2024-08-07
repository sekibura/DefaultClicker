using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SekiburaGames.DefaultClicker.Controllers
{
    public class TimersController : MonoBehaviour
    {
        public class TimerData
        {
            public Action Callback;
            public float Duration;
            public bool Repeat;
            public Coroutine Coroutine;

            public TimerData(Action callback, float duration, bool repeat)
            {
                Callback = callback;
                Duration = duration;
                Repeat = repeat;
            }
        }

        private static TimersController _instance;
        public static TimersController Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<TimersController>();
                    if (_instance == null)
                    {
                        GameObject go = new GameObject("TimersController");
                        _instance = go.AddComponent<TimersController>();
                    }
                }
                return _instance;
            }
        }

        private Dictionary<string, TimerData> _timers = new Dictionary<string, TimerData>();

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = this;
            }
        }

        private List<TimerData> timers = new List<TimerData>();

        public TimerData StartTimer(Action callback, float duration, bool repeat = false)
        {
            TimerData timerData = new TimerData(callback, duration, repeat);
            timerData.Coroutine = StartCoroutine(RunTimer(timerData));
            timers.Add(timerData);
            return timerData;
        }

        public void StopTimer(TimerData timerData)
        {
            if (!timers.Contains(timerData))
            {
                Debug.LogWarning($"Timer not found!");
                return;
            }

            StopCoroutine(timerData.Coroutine);
            timers.Remove(timerData);
        }

        private IEnumerator RunTimer(TimerData timerData)
        {
            while (true)
            {
                yield return new WaitForSeconds(timerData.Duration);
                timerData.Callback?.Invoke();
                if (!timerData.Repeat)
                {
                    timers.Remove(timerData);
                    yield break;
                }
            }
        }
    }
}
