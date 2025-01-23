using UnityEngine;

public class ImpactSound1 : MonoBehaviour
{

    public AudioSource myAudioSource;
    public string ignoredTag ="";
    [SerializeField] int time = 1;
    private float counter = 0;

    private void Update()
    {
        if (counter < time)
        {
            counter += Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(ignoredTag))
        {
            return; // Do nothing if the tag matches the ignored tag
        }

        if (counter >= time)
        {
            myAudioSource.Play();
        }
    }
}
