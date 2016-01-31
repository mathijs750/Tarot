using UnityEngine;
using System.Collections;

public class DeadguyAudio : MonoBehaviour {

    public float waitTime;
    private float maxPitch = 1.1f;
    private float minPitch = 0.9f;
    public bool soundNotPlaying = true;
    public AudioSource Source;

    public AudioClip[] DeadguySounds;

    IEnumerator PlaySoundAndWait()
    {
        soundNotPlaying = false;
        yield return new WaitForSeconds(waitTime);
        Source.pitch = Random.Range(minPitch, maxPitch);
        AudioHelpers.LoadAndplayClip(Source, DeadguySounds[Random.Range(0, DeadguySounds.Length)]);
        soundNotPlaying = true;
    }

    IEnumerator PlaySoundAndWaitLess()
    {
        soundNotPlaying = false;
        yield return new WaitForSeconds(1);
        Source.pitch = Random.Range(minPitch, maxPitch);
        AudioHelpers.LoadAndplayClip(Source, DeadguySounds[Random.Range(0, DeadguySounds.Length)]);
        soundNotPlaying = true;
    }

    void Start ()
    {
        Source = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player" && soundNotPlaying == true)
        {
            StartCoroutine(PlaySoundAndWaitLess());
        }
    }

    void OnTriggerStay (Collider col)
    {
        if (col.tag == "Player" && soundNotPlaying == true)
        {
            StartCoroutine(PlaySoundAndWait());
        }
	}
}
