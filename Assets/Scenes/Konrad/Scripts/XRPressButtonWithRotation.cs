using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit;

public class XRPressButtonWithRotation : MonoBehaviour
{
    [SerializeField] Transform objectToRotate;
    [SerializeField] Vector3 newRotation;  // The object that will rotate
    [SerializeField] XRGrabInteractable grabInteractable;

    void Awake()
    {
        // Register event listener
        grabInteractable.selectEntered.AddListener(OnButtonPressed);
        grabInteractable.selectExited.AddListener(OnReleased);
    }

    void OnDestroy()
    {
        // Unregister event listener
        grabInteractable.selectEntered.RemoveListener(OnButtonPressed);
        grabInteractable.selectExited.RemoveListener(OnReleased);
    }

    private void OnButtonPressed(SelectEnterEventArgs args)
    {
        Debug.Log("ON");
        objectToRotate.Rotate(newRotation);
    }

    private void OnReleased(SelectExitEventArgs args)
    {
        Debug.Log("OFF");
        objectToRotate.Rotate(-newRotation);
    }
}
