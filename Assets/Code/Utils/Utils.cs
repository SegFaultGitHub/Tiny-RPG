using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Linq;

public static class Utils {
    public static T RandomRange<T>(List<(float, T)> o) {
        float total = 0;
        foreach ((float value, T _) in o) {
            total += value;
        }
        float choice = Random.Range(0, total);
        float index = 0;
        foreach ((float value, T item) in o) {
            index += value;
            if (choice <= index)
                return item;
        }

        return o[^1].Item2;
    }
}
