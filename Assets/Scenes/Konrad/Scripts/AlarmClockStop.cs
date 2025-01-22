using UnityEngine;

public class AlarmClockStop : MonoBehaviour
{
    [SerializeField] AudioSource myAudio;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wecker"))
        {
            myAudio.enabled = false;
        }
    }
}
