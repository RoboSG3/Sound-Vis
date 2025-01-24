using UnityEngine;

public class PlayAudioTrigger : MonoBehaviour
{
    public XRToggleButton xRToggleButton;
    public SocketChecker socketChecker;
    public AudioSource audioSource;
    [SerializeField] NoteUpdater updater;

    private bool isPlaying = false;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (xRToggleButton.isToggled && socketChecker.isObjectInside && !isPlaying)
        {
            audioSource.loop = true;
            audioSource.Play();
            isPlaying = true;
            updater.CompleteQuest("Plattenspieler abspielen");
        }

        else if ((!xRToggleButton.isToggled || !socketChecker.isObjectInside) && isPlaying)
        {
            audioSource.loop = false;
            audioSource.Stop();
            isPlaying = false;
        }
    }
}
