using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class LockChildOnGrab : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;
    private Rigidbody rb;
    private Vector3 initialLocalPosition;
    private Quaternion initialLocalRotation;

    void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        rb = GetComponent<Rigidbody>();

        // Save the initial local position and rotation
        initialLocalPosition = transform.localPosition;
        initialLocalRotation = transform.localRotation;

        // Disable physics movement
        if (rb != null)
        {
            rb.isKinematic = true;
        }

        // Register grab event listeners
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    void OnDestroy()
    {
        // Unregister grab event listeners
        grabInteractable.selectEntered.RemoveListener(OnGrab);
        grabInteractable.selectExited.RemoveListener(OnRelease);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        // Lock position and rotation
        transform.localPosition = initialLocalPosition;
        transform.localRotation = initialLocalRotation;
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        // Reset position and rotation to ensure it doesn't move
        transform.localPosition = initialLocalPosition;
        transform.localRotation = initialLocalRotation;
    }
}