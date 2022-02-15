using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Project.Classes {
    public static class Utils {
        
    }

    public static class Vector2Extensions {
        public static Vector2 Limit(this Vector2 vect, float value) {
            if (vect.magnitude <= value) {
                return vect;
            }

            return vect.normalized * value;
        }
    }

    public static class ListExtensions {
        public static float Average(this List<float> list) => list.Sum() / list.Count;
    }
}