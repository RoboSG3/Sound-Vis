using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class VRButton : MonoBehaviour
{
    public float deadTime = 1.0f;
    
    private bool _deadTimeActive = false;
    
    public string _compareTag = "Button";

    public UnityEvent onPressed, onReleased;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_compareTag) && !_deadTimeActive)
        {
            onPressed?.Invoke();
            Debug.Log("Pressed "+_compareTag);
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(_compareTag) && !_deadTimeActive)
        {
            onReleased?.Invoke();
            Debug.Log("Released");
            StartCoroutine(WaitForDeadTime());
        }
    }

    IEnumerator WaitForDeadTime()
    {   
        _deadTimeActive = true;
        yield return new WaitForSeconds(deadTime);
        _deadTimeActive = false;
    }
}
