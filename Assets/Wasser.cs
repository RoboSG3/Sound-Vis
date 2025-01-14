using UnityEngine;

public class Wasser : MonoBehaviour
{
    public ParticleSystem ParticleSystem;

    public void StartParticalSystem()
    {
        if (!ParticleSystem.isPlaying)
        {
            ParticleSystem.Play();
        }
    }
}
