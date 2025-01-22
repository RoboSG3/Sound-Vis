using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ParticleCollisionHandler : MonoBehaviour
{
    
    public float deadTime = 1.0f; // Cooldown time between button presses
    public string compareTag = "Button"; // Tag to compare against
    
    // This method is called when particles collide
    public UnityEvent onPressed; // Event triggered when the button is pressed
    private bool _deadTimeActive = false;
    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag(compareTag) && !_deadTimeActive)
        {
            onPressed?.Invoke();
            Debug.Log("Pressed " + compareTag);
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