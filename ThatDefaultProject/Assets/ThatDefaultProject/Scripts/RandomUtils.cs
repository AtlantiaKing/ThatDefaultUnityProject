using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace that
{
    public class WeightedValue<T>
    {
        public WeightedValue(float weight = 1f, T val = default(T))
        {
            Weight = weight;
            Value = val;
        }
        public WeightedValue(T val = default(T), float weight = 1f)
        {
            Weight = weight;
            Value = val;
        }

        public float Weight {  get; set; }
        public T Value { get; set; }
    }

    public static class RandomUtils
    {
        public static T GetWeightedRandom<T>(params WeightedValue<T>[] weightedVals)
        {
            float totalWeight = weightedVals.Sum(obj => obj.Weight);

            float randomWeight = Random.Range(0f, totalWeight);

            foreach (WeightedValue<T> obj in weightedVals)
            {
                randomWeight -= obj.Weight;
                if (randomWeight <= 0)
                {
                    return obj.Value;
                }
            }

            throw new UnityException("Something went terribly wrong. TotalWeight might've been 0. Keep in mind that this does not support negative weights.");
        }

        public static bool YesOrNo(float yesWeight = 1f, float noWeight= 1f) 
        {
            return GetWeightedRandom(new WeightedValue<bool>(yesWeight,true),new WeightedValue<bool>(noWeight,false));
        }

        public static T GetRandomValue<T>(this List<T> list)
        {
            return list[Random.Range(0, list.Count)];
        }
        public static T GetRandomValue<T>(this T[] array)
        {
            return array[Random.Range(0, array.Length)];
        }
    }
}
