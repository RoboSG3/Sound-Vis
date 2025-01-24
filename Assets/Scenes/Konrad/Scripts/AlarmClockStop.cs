using UnityEngine;

public class AlarmClockStop : MonoBehaviour
{
    [SerializeField] AudioSource myAudio;
    [SerializeField] NoteUpdater updater;
    [SerializeField] Animator myAnimator;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wecker"))
        {
            myAudio.enabled = false;
            updater.CompleteQuest("Schalte den Wecker aus");
            myAnimator.enabled = false;
        }
    }
}
