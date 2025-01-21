using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class WasserButton : MonoBehaviour
{
    [Header("Button Settings")]
    public float deadTime = 1.0f; // Cooldown time between button presses
    public string compareTag = "Button"; // Tag to compare against

    [Header("Button Events")]
    public UnityEvent onPressed; // Event triggered when the button is pressed
    public UnityEvent onReleased; // Event triggered when the button is released

    private bool _deadTimeActive = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(compareTag) && !_deadTimeActive)
        {
            onPressed?.Invoke();
            Debug.Log("Pressed " + compareTag);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(compareTag) && !_deadTimeActive)
        {
            onReleased?.Invoke();
            Debug.Log("Released");
            StartCoroutine(WaitForDeadTime());
        }
    }

    private IEnumerator WaitForDeadTime()
    {
        _deadTimeActive = true;
        yield return new WaitForSeconds(deadTime);
        _deadTimeActive = false;
    }
}