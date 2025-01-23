using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class DoorBell : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] int time = 60;
    [SerializeField] int time2 = 4;
    [SerializeField] HingeJoint joint;
    [SerializeField] XRGrabInteractable interactable;

    private float counter = 0;
    private float counter2 = 0;
    private bool doorBell = false;
    private bool loop = false;

    private void Update()
    {
        if (counter < time && !doorBell)
        {
            counter += Time.deltaTime;
        }

        if (counter >= time && !doorBell)
        {
            audioSource.Play();
            audioSource.loop = true;
            doorBell = true;
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
            interactable.enabled = true;
            JointLimits limits = joint.limits;
            limits.min = -120f;
            limits.max = 120f;
            joint.limits = limits;

        }
    }
}
