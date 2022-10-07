using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class ExtensionMethods
{
    public static List<Transform> GetChildren(this GameObject gameObject)
    {
        List<Transform> children = new List<Transform>();
        foreach (Transform child in gameObject.transform)
        {
            children.Add(child);
        }
        return children;
    }
    public static void SetTransparency(this Image image, byte transparency)
    {
        if (image != null)
        {
            Color32 alpha = image.color;
            alpha.a = transparency;
            image.color = alpha;
        }
    }
    public static int GetRandomSystemRandom(int min, int max)
    {
        System.Random getrandom = new System.Random();
        lock (getrandom)
        {
            return getrandom.Next(min, max);
        }
    }
}
