using UnityEngine;

public class EierUhr1 : MonoBehaviour
{
    public XRToggleButton xRToggleButton;
    public SocketChecker socketChecker;
    public AudioSource audioSource;
    private float timer = 0;
    private float time = 0;
    [SerializeField] GameObject eierUhr;
    private bool playedAudio = false;

    // Update is called once per frame
    void Update()
    {
        if (socketChecker.isObjectInside && xRToggleButton.isToggled && !playedAudio)
        {
            timer += Time.deltaTime;
            Debug.Log(timer);
            
            if(timer > time)
            {
                audioSource.Play();
                playedAudio = true;
            }
        }
    }
}