using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Project.Classes {
    public static class Utils { }

    public static class FloatExtensions {
        public static bool Equals(this float num1, float num2, float accuracy = Consts.EPSILON)
        {
            return Math.Abs(num1 - num2) < accuracy;
        }

        public static bool Equals(this float num1, int num2, float accuracy = Consts.EPSILON)
        {
            return Math.Abs(num1 - num2) < accuracy;
        }
    }

    public static class Vector2Extensions {
        public static Vector2 Limit(this Vector2 vect, float value) {
            if (vect.magnitude <= value) {
                return vect;
            }

            return vect.normalized * value;
        }

        public static bool ApproximatelyEquals(this Vector2 vect1, Vector2 vect2, float aproximization = 0.05f) {
            return vect1.x.Equals(vect2.x, aproximization) &&
                   vect1.y.Equals(vect2.y, aproximization);
        }
    }

    public static class ListExtensions {
        public static float Average(this List<float> list) => list.Sum() / list.Count;
        public static Vector2 Average(this List<Vector2> list) {
            var sum = list.Aggregate(Vector2.zero, (current, vector2) => current + vector2);
            return sum / list.Count;
        }

        public static List<T> Except<T>(this List<T> list, T elem) {
            var result = new List<T>(list);
            result.RemoveAll(t => t.Equals(elem));
            return result;
        }
    }
}