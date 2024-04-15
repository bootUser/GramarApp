using System;
using System.Collections.Generic;

namespace Extensions
{
    public static class ListExtensions
    {
        public static T Random<T>(this List<T> list)
        {
            var rnd = new Random();
            var index = rnd.Next(0, list.Count);
            return list[index];
        }
    }
}