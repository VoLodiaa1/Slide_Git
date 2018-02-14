using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions {

    public static Quaternion[] AddItemToArray(this Quaternion[] original, Quaternion itemToAdd)
    {
        Quaternion[] finalArray = new Quaternion[original.Length + 1];
        for (int i = 0; i < original.Length; i++)
        {
            finalArray[i] = original[i];
        }
        finalArray[finalArray.Length - 1] = itemToAdd;
        return finalArray;
    }
}
