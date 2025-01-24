using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class DoorBell : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] int time = 60;
    [SerializeField] int time2 = 4;
    [SerializeField] HingeJoint joint;
    [SerializeField] XRGrabInteractable interactable;
    [SerializeField] NoteUpdater updater;
 
    private float counter = 0;
    private float counter2 = 0;
    private bool doorBell = false;
    private bool loop = false;

    private void Update()
    {
        Debug.Log(updater.twoQuests);
        if (counter < time && !doorBell && updater.twoQuests)
        {
            counter += Time.deltaTime;
        }

        if (counter >= time && !doorBell)
        {
            audioSource.Play();
            audioSource.loop = true;
            doorBell = true;
            interactable.enabled = true;
            JointLimits limits = joint.limits;
            limits.min = -120f;
            limits.max = 120f;
            joint.limits = limits;
        }

        if (counter2 < time2 && doorBell && !loop)
        {
            counter2 += Time.deltaTime;
        }

        if (counter2 >= time2 && doorBell && !loop)
        {
            audioSource.Stop();
            audioSource.loop = false;
            loop = true;


        }
    }
}
