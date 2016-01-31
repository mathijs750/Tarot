using UnityEngine;
using System.Collections;

public class AudioSystem : MonoBehaviour {

    public float stepTime;
    private float maxPitch = 1.5f;
    private float minPitch = 0.5f;
    public bool soundNotPlaying = true;

    public AudioClip[] GrassSounds;
    public AudioClip[] PathSounds;
    public AudioClip Slash;

    public static AudioSource Source;

    IEnumerator PlayGrassAndWait()
    {
        soundNotPlaying = false;
        yield return new WaitForSeconds(stepTime);
        Source.pitch = Random.Range(minPitch, maxPitch);
        AudioHelpers.LoadAndplayClip(Source, GrassSounds[Random.Range(0, GrassSounds.Length)]);
        soundNotPlaying = true;
    }

    IEnumerator PlayPathAndWait()
    {
        soundNotPlaying = false;
        yield return new WaitForSeconds(stepTime);
        AudioHelpers.LoadAndplayClip(Source, GrassSounds[Random.Range(0, PathSounds.Length)]);
        soundNotPlaying = true;
    }

    void Start ()
    {
        Source = GetComponent<AudioSource>();
    }

    void Update ()
    {
        RaycastHit hit = new RaycastHit();
        string floortag;

        if (Input.GetAxis("Horizontal") > 0.1 || Input.GetAxis("Horizontal") < -0.1 || Input.GetAxis("Vertical") > 0.1 || Input.GetAxis("Vertical") < -0.1)
        {
            if (Physics.Raycast(transform.position, Vector3.down, out hit))
            {
                floortag = hit.collider.gameObject.tag;
                if (floortag == "GroundGrass" && soundNotPlaying == true)
                {
                    StartCoroutine(PlayGrassAndWait());
                }
                else if (floortag == "GroundPath" && soundNotPlaying == true)
                {
                    StartCoroutine(PlayPathAndWait());
                }
            }
        }
	}
    public static void SlashSound(AudioSource source, AudioClip Slash)
    {
        Slash.LoadAudioData();
        source.clip = Slash;
        source.Play();
    }
}

public class AudioHelpers
{
  
    public static void LoadAndplayClip(AudioSource source, AudioClip Clip)
    {
        Clip.LoadAudioData();
        source.clip = Clip;
        source.Play();
    }
}
