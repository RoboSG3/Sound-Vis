using UnityEngine;

public class ImpactSound : MonoBehaviour
{

    public AudioSource myAudioSource;
    [SerializeField] int time = 1;
    private float counter = 0;

    private void Update()
    {
        if(counter <  time)
        {
            counter += Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (counter >= time)
        {
            myAudioSource.Play();
        }
    }
}
