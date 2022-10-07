using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExcitedFlash : MonoBehaviour
{
    [SerializeField] private Collider coll;
    [SerializeField] private Light anylight;
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
        direction = other.transform.position.z - transform.position.z;
        anylight.gameObject.SetActive(true);
        StartCoroutine(playSoundInSeries());
        anylight.gameObject.SetActive(true);
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
