using UnityEngine;

public class PlayAudioTrigger : MonoBehaviour
{
    public XRToggleButton xRToggleButton;
    public SocketChecker socketChecker;
    public AudioSource audioSource;

    private bool isPlaying = false;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (xRToggleButton.isToggled && socketChecker.isObjectInside && !isPlaying)
        {
            audioSource.loop = true;
            audioSource.Play();
            isPlaying = true;
        }

        else if ((!xRToggleButton.isToggled || !socketChecker.isObjectInside) && isPlaying)
        {
            audioSource.loop = false;
            audioSource.Stop();
            isPlaying = false;
        }
    }
}
