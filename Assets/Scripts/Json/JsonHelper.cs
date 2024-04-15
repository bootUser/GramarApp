using System.Collections.Generic;
using UnityEngine;

namespace Json
{
    public static class JsonHelper
    {
        public static T[] FromJson<T>(string jsonString)
        {
            var wrappedList = JsonUtility.FromJson<Wrapper<T>>(jsonString);
            return wrappedList.items;
        }
    }
}