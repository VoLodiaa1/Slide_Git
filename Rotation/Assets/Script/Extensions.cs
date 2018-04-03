using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions {

    public static Vector3[] AddItemToArray(this Vector3[] original, Vector3 itemToAdd)
    {
        Vector3[] finalArray = new Vector3[original.Length + 1];
        for (int i = 0; i < original.Length; i++)
        {
            finalArray[i] = original[i];
        }
        finalArray[finalArray.Length - 1] = itemToAdd;
        return finalArray;
    }
}
