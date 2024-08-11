using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SekiburaGames.DefaultClicker.System
{
    public static class Extensions
    {
        public static void Shuffle<T>(this List<T> list)
        {
            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = Random.Range(0, i + 1);
                T temp = list[i];
                list[i] = list[j];
                list[j] = temp;
            }
        }
        public static void Shuffle<T>(this T[] arr)
        {
            for (int i = arr.Length - 1; i > 0; i--)
            {
                int j = Random.Range(0, i + 1);
                T temp = arr[i];
                arr[i] = arr[j];
                arr[j] = temp;
            }
        }

        public static T GetRandom<T>(this T[] arr)
        {
            return arr[Random.Range(0, arr.Length)];
        }
        public static int GetRandomExclude(int min, int max, List<int> excludedValue)
        {
            var indexes = Enumerable.Range(0, max).Where(i => !excludedValue.Contains(i)).ToArray();
            int index = Random.Range(0, indexes.Count());
            return indexes[index];
        }

        public static string ToPrintString<T>(this List<T> list, string divider) 
        {
            StringBuilder sb = new StringBuilder();
            foreach (T item in list)
            {
                sb.Append(item.ToString()+ divider);
            }
            return sb.ToString();
        }
    }
}
