using UnityEngine;

public class AudioDuringParticle : MonoBehaviour
{
    [SerializeField] ParticleSystem myParticleSystem;
    [SerializeField] AudioSource mySource;


    void Update()
    {
        if (myParticleSystem != null)
        {
            if (myParticleSystem.isPlaying)
            {
                mySource.Play();
                mySource.loop = true;
            }
            else
            {
                mySource.Stop();
                mySource.loop = false;
            }
        }
    }
}
