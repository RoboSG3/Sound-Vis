using UnityEngine;

public class ControlParticleSystemByRotation : MonoBehaviour
{
    public ParticleSystem myParticleSystem; // Reference to the particle system
    public Transform targetObject;       // Object whose rotation will be checked

    void Update()
    {
        if (myParticleSystem == null || targetObject == null) return;

        // Get the X rotation of the target object
        float xRotation = targetObject.eulerAngles.x;

        // Normalize the rotation between 0 and 360
        if (xRotation > 180) xRotation -= 360;

        // Check if the rotation is between 0 and 180
        bool shouldEmit = xRotation >= 0 && xRotation <= 180;

        // Control the particle system emission
        var emission = myParticleSystem.emission;
        emission.enabled = shouldEmit;
    }
}