using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.XR.Interaction.Toolkit;

public class SocketChecker : MonoBehaviour
{
    public XRSocketInteractor socket;  // Reference to the socket

    public bool isObjectInside = false;

    void Awake()
    {
        // Ensure the socket reference is set
        if (socket != null)
        {
            // Register events to detect when an object is placed or removed
            socket.selectEntered.AddListener(OnObjectPlaced);
            socket.selectExited.AddListener(OnObjectRemoved);
        }
    }

    void OnDestroy()
    {
        // Unregister events to avoid memory leaks
        if (socket != null)
        {
            socket.selectEntered.RemoveListener(OnObjectPlaced);
            socket.selectExited.RemoveListener(OnObjectRemoved);
        }
    }

    private void OnObjectPlaced(SelectEnterEventArgs args)
    {
        isObjectInside = true; // Object is in the socket
        Debug.Log("Object placed in the socket.");
    }

    private void OnObjectRemoved(SelectExitEventArgs args)
    {
        isObjectInside = false; // Object is no longer in the socket
        Debug.Log("Object removed from the socket.");
    }
}
