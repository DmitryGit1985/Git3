using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetChildrenCanvas : MonoBehaviour
{
    private List<Transform> children;
    void Start()
    {
        List<Transform> children = GetChildren(transform);
        foreach (Transform child in children)
        {
            Debug.Log(child.name);
        }
    }
    private List<Transform> GetChildren(Transform parent)
    {
        List<Transform> children = new List<Transform>();
        foreach (Transform child in parent)
        {
            children.Add(child);
        }
        return children;
    }
}
