using System.Collections;
using UnityEngine;

public class Wasser : MonoBehaviour
{
    public ParticleSystem myParticleSystem;
    public AudioSource audioSource;
    public int playCount = 3; // Number of times to play the sound
    public float delayBetweenPlays = 0f; // Optional delay between plays

    public void StartParticalSystem()
    {
        if (!myParticleSystem.isPlaying)
        {
            myParticleSystem.Play();
            StartCoroutine(PlaySoundRepeatedly(playCount, delayBetweenPlays));
        }
    }

    private IEnumerator PlaySoundRepeatedly(int times, float delay)
    {
        for (int i = 0; i < times; i++)
        {
            audioSource.Play();
            yield return new WaitForSeconds(audioSource.clip.length + delay);
        }
    }
}
