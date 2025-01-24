using System.Threading;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class ZahnbürsteInsideTriggerChecker : MonoBehaviour
{
    public string targetTag = "Zahnbürste";    // Tag of the collider to detect (optional)
    private bool isInsideTrigger = false;  // The boolean to toggle
    private float timer = 0;
    public float time = 0;
    [SerializeField] NoteUpdater updater;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger matches the target tag (if specified)
        if (string.IsNullOrEmpty(targetTag) || other.CompareTag(targetTag))
        {
            isInsideTrigger = true; // Set the boolean to true
            //Debug.Log($"Object {other.name} entered the trigger. isInsideTrigger: {isInsideTrigger}");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the object exiting the trigger matches the target tag (if specified)
        if (string.IsNullOrEmpty(targetTag) || other.CompareTag(targetTag))
        {
            isInsideTrigger = false; // Set the boolean to false
            //Debug.Log($"Object {other.name} exited the trigger. isInsideTrigger: {isInsideTrigger}");
        }
    }

    void Update()
    {
        if(isInsideTrigger)
        {
            timer += Time.deltaTime;
            //Debug.Log(timer);

            if (timer > time)
            {
                updater.CompleteQuest("Putze Zähne (10 Sek)");
                timer = 0;
            }
        }
    }
}
