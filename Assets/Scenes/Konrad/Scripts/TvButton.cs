using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class TvButton : MonoBehaviour
{
    public GameObject childObject1; // First child object to toggle
    public GameObject childObject2; // Second child object to toggle
    public bool isToggled = false;  // Toggle state

    public Material toggledMaterial;   // Material for the toggled state
    public Material untoggledMaterial; // Material for the untoggled state

    private Renderer objectRenderer;   // Reference to the Renderer component
    private XRGrabInteractable grabInteractable;
    [SerializeField] NoteUpdater updater;


    void Awake()
    {
        // Get the XRGrabInteractable component
        grabInteractable = GetComponent<XRGrabInteractable>();

        // Get the Renderer component
        objectRenderer = GetComponent<Renderer>();

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
        Toggle();
    }

    public void Toggle()
    {
        // Toggle the state
        isToggled = !isToggled;

        // Toggle between the two child objects
        if (isToggled)
        {
            childObject1.SetActive(true);
            childObject2.SetActive(false);
            objectRenderer.material = toggledMaterial; // Change to the toggled material
        }
        else
        {
            childObject1.SetActive(false);
            childObject2.SetActive(true);
            objectRenderer.material = untoggledMaterial; // Change to the untoggled material
            updater.CompleteQuest("Schalte den Fernseher");
        }
    }
}