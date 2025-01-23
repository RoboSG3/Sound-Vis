using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit;

public class XRPressButton : MonoBehaviour
{
    [SerializeField] Vector3 newPosition = new Vector3(0, 0, 0);  // The object that will rotate
    [SerializeField] XRGrabInteractable grabInteractable;
    private Vector3 position;

    void Awake()
    {
        // Register event listener
        grabInteractable.selectEntered.AddListener(OnButtonPressed);
        grabInteractable.selectExited.AddListener(OnReleased);
        position = transform.localPosition;
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
        transform.localPosition = newPosition;
    }

    private void OnReleased(SelectExitEventArgs args)
    {
        Debug.Log("OFF");
        transform.localPosition = position;
    }
}
