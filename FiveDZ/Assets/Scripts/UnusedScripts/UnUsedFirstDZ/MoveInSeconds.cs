using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveInSeconds : MonoBehaviour
{
    private RectTransform rectTransform;
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z)) 
        { 
            StartCoroutine(DoMove(3f, Vector2.zero)); 
        }
    }
    private IEnumerator DoMove(float time, Vector2 targetPosition) 
    { 
        Vector2 startPosition = rectTransform.anchoredPosition; 
        float startTime = Time.realtimeSinceStartup; 
        float fraction = 0f; 
        while (fraction < 1f) 
        { 
            fraction = Mathf.Clamp01((Time.realtimeSinceStartup - startTime) / time); 
            rectTransform.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, fraction); 
            yield return null; 
        } 
    }
}
