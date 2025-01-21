using UnityEngine;

public class SocketOrder : MonoBehaviour
{

    [SerializeField] GameObject[] sockets; // Array to hold references to the 5 sockets
    private bool[] socketStates; // Array to track the usage state of sockets

    void Start()
    {
        // Initialize socket states
        socketStates = new bool[sockets.Length];

        // Ensure only the first socket is active initially
        for (int i = 0; i < sockets.Length; i++)
        {
            sockets[i].SetActive(i == 0); // Only activate the first socket
            SetMeshRendererVisibility(sockets[i], true); // Ensure the Mesh Renderer is visible initially
        }
    }

    public void OnSocketUse(int socketIndex)
    {
        if (socketIndex < 0 || socketIndex >= socketStates.Length) return;

        // Mark the current socket as in use
        socketStates[socketIndex] = true;

        // Turn off the Mesh Renderer for the current socket
        SetMeshRendererVisibility(sockets[socketIndex], false);

        // Activate the next socket, if it exists
        if (socketIndex + 1 < sockets.Length)
        {
            sockets[socketIndex + 1].SetActive(true);
        }
    }

    public void OnSocketRelease(int socketIndex)
    {
        if (socketIndex < 0 || socketIndex >= socketStates.Length) return;

        // Mark the current socket as not in use
        socketStates[socketIndex] = false;

        // Turn on the Mesh Renderer for the current socket
        SetMeshRendererVisibility(sockets[socketIndex], true);

        // Deactivate the next socket, if it exists
        if (socketIndex + 1 < sockets.Length)
        {
            sockets[socketIndex + 1].SetActive(false);
        }
    }

    private void SetMeshRendererVisibility(GameObject socket, bool isVisible)
    {
        // Check if the socket has a MeshRenderer component
        MeshRenderer renderer = socket.GetComponent<MeshRenderer>();
        if (renderer != null)
        {
            renderer.enabled = isVisible; // Enable or disable the Mesh Renderer
        }
    }

}
