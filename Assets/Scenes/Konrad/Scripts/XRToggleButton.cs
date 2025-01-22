using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit;

public class XRToggleButton : MonoBehaviour
{
    public Transform objectToRotate;  // The object that will rotate
    public Vector3 rotationAngle;    // The angle to rotate by (e.g., 0, 45, 0)
    public bool isToggled = false;   // The toggle state

    private XRGrabInteractable grabInteractable;

    void Awake()
    {
        // Get the XRGrabInteractable component
        grabInteractable = GetComponent<XRGrabInteractable>();

        // Register event listener
        grabInteractable.selectEntered.AddListener(OnButtonPressed);
    }

    void OnDestroy()
    {
        // Unregister event listener
        grabInteractable.selectEntered.RemoveListener(OnButtonPressed);
    }

    private void OnButtonPressed(SelectEnterEventArgs args)
    {
        // Toggle the state
        isToggled = !isToggled;

        // Rotate the object based on the toggle state
        if (isToggled)
        {
            Debug.Log("ON");
            objectToRotate.Rotate(rotationAngle);
        }
        else
        {
            Debug.Log("OFF");
            objectToRotate.Rotate(-rotationAngle);
        }
    }
}
