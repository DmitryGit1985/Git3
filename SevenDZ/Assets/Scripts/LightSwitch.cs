using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    [SerializeField] private Collider coll;
    [SerializeField] private Light anylight;
    [SerializeField] private float lightLifeTimer =0.01f;
    [SerializeField] private bool playAudioSource = false;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip1;
    [SerializeField] private AudioClip audioClip2;
    private float direction;


    void Start()
    {
        anylight.gameObject.SetActive(false);
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("test");
        direction = other.transform.position.z - transform.position.z;
        anylight.gameObject.SetActive(true);
        StartCoroutine(Countdown2());
        StartCoroutine(playSoundInSeries());
    }
    private IEnumerator Countdown2()
    {
        float duration = lightLifeTimer;
        float normalizedTime = 0;
        while (normalizedTime <= 1f)
        {
            //countdownImage.fillAmount = normalizedTime;
            normalizedTime += Time.deltaTime / duration;
            yield return null;
        }
        anylight.gameObject.SetActive(false);
    }
    IEnumerator playSoundInSeries()
    {
        audioSource.clip = audioClip1;
        audioSource.Play();
        yield return new WaitForSeconds(audioClip1.length);
        audioSource.clip = audioClip2;
        audioSource.Play();
    }
}
