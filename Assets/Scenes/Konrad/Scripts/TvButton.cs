using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class TvButton : MonoBehaviour
{
    public GameObject childObject1; // First child object to toggle
    public GameObject childObject2; // Second child object to toggle
    public bool isToggled = false;  // Toggle state

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

        // Toggle between the two child objects
        if (isToggled)
        {
            childObject1.SetActive(true);
            childObject2.SetActive(false);
        }
        else
        {
            childObject1.SetActive(false);
            childObject2.SetActive(true);
        }
    }

    public void Toggle()
    {
        isToggled = !isToggled;

        // Toggle between the two child objects
        if (isToggled)
        {
            childObject1.SetActive(true);
            childObject2.SetActive(false);
        }
        else
        {
            childObject1.SetActive(false);
            childObject2.SetActive(true);
        }
    }
}