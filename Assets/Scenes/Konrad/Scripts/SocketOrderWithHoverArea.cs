using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class SocketOrderWithHoverArea : MonoBehaviour
{
    [SerializeField] XRSocketInteractor[] sockets; // Array to hold references to the 5 sockets
    private bool[] socketStates;         // Array to track the usage state of sockets

    void Start()
    {
        // Initialize socket states
        socketStates = new bool[sockets.Length];

        // Ensure only the first socket is active initially
        for (int i = 0; i < sockets.Length; i++)
        {
            sockets[i].gameObject.SetActive(i == 0); // Only activate the first socket
            SetShowInteractableHover(sockets[i], true); // Ensure hover visuals are enabled initially
        }
    }

    public void OnSocketUse(int socketIndex)
    {
        if (socketIndex < 0 || socketIndex >= socketStates.Length) return;

        // Mark the current socket as in use
        socketStates[socketIndex] = true;

        // Turn off hover visuals for the current socket
        SetShowInteractableHover(sockets[socketIndex], false);

        // Activate the next socket, if it exists
        if (socketIndex + 1 < sockets.Length)
        {
            sockets[socketIndex + 1].gameObject.SetActive(true);
        }
    }

    public void OnSocketRelease(int socketIndex)
    {
        if (socketIndex < 0 || socketIndex >= socketStates.Length) return;

        // Mark the current socket as not in use
        socketStates[socketIndex] = false;

        // Turn on hover visuals for the current socket
        SetShowInteractableHover(sockets[socketIndex], true);

        // Deactivate the next socket, if it exists
        if (socketIndex + 1 < sockets.Length)
        {
            sockets[socketIndex + 1].gameObject.SetActive(false);
        }
    }

    private void SetShowInteractableHover(XRSocketInteractor socket, bool isEnabled)
    {
        if (socket != null)
        {
            socket.showInteractableHoverMeshes = isEnabled;
        }
    }
}
