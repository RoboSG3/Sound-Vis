using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.XR.Interaction.Toolkit;

public class SocketActivator : MonoBehaviour
{
    public XRSocketInteractor firstSocket;  // Reference to the first socket
    public GameObject secondSocket;        // Reference to the second socket
    public TopfWasser topfWasser;

    void Awake()
    {
        // Register event listeners for the first socket
        if (firstSocket != null)
        {
            firstSocket.selectEntered.AddListener(OnObjectPlaced);
            firstSocket.selectExited.AddListener(OnObjectRemoved);
        }
    }

    void OnDestroy()
    {
        // Unregister event listeners to avoid memory leaks
        if (firstSocket != null)
        {
            firstSocket.selectEntered.RemoveListener(OnObjectPlaced);
            firstSocket.selectExited.RemoveListener(OnObjectRemoved);
        }
    }

    private void OnObjectPlaced(SelectEnterEventArgs args)
    {
        // Check the condition before activating the second socket
        if (topfWasser != null && topfWasser._meshActive)
        {
            if (secondSocket != null)
            {
                secondSocket.SetActive(true);
            }
        }
    }

    private void OnObjectRemoved(SelectExitEventArgs args)
    {
        // Check the condition before deactivating the second socket
        if (topfWasser != null && topfWasser._meshActive)
        {
            if (secondSocket != null)
            {
                secondSocket.SetActive(false);
            }
        }
    }
}
