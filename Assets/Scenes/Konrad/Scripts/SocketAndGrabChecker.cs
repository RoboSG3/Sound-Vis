using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class SocketAndGrabChecker : MonoBehaviour
{
    public XRSocketInteractor socket;    // Reference to the XR Socket
    public XRGrabInteractable grabObject; // Reference to the object that can be grabbed
    public GameObject targetGameObject;   // The GameObject to turn on/off

    private bool isGrabbed = false;       // Tracks if the object is grabbed

    void Awake()
    {
        if (socket != null)
        {
            // Register event listeners for the socket
            socket.selectEntered.AddListener(OnSocketObjectPlaced);
            socket.selectExited.AddListener(OnSocketObjectRemoved);
        }

        if (grabObject != null)
        {
            // Register event listeners for the grab object
            grabObject.selectEntered.AddListener(OnObjectGrabbed);
            grabObject.selectExited.AddListener(OnObjectReleased);
        }

        // Ensure the target GameObject is initially off
        if (targetGameObject != null)
            targetGameObject.SetActive(false);
    }

    void OnDestroy()
    {
        if (socket != null)
        {
            socket.selectEntered.RemoveListener(OnSocketObjectPlaced);
            socket.selectExited.RemoveListener(OnSocketObjectRemoved);
        }

        if (grabObject != null)
        {
            grabObject.selectEntered.RemoveListener(OnObjectGrabbed);
            grabObject.selectExited.RemoveListener(OnObjectReleased);
        }
    }

    private void OnSocketObjectPlaced(SelectEnterEventArgs args)
    {
        UpdateGameObjectState();
    }

    private void OnSocketObjectRemoved(SelectExitEventArgs args)
    {
        UpdateGameObjectState();
    }

    private void OnObjectGrabbed(SelectEnterEventArgs args)
    {
        isGrabbed = true;
        UpdateGameObjectState();
    }

    private void OnObjectReleased(SelectExitEventArgs args)
    {
        isGrabbed = false;
        UpdateGameObjectState();
    }

    private void UpdateGameObjectState()
    {
        // Turn on the GameObject if the socket is empty and the object is grabbed
        if (targetGameObject != null)
        {
            bool isSocketEmpty = !socket.hasSelection;
            targetGameObject.SetActive(isSocketEmpty && isGrabbed);
        }
    }
}
