using UnityEngine;

public class ImpactSound : MonoBehaviour
{

    public AudioSource myAudioSource;

    private void OnCollisionEnter(Collision collision)
    {
        myAudioSource.Play();
    }
}
