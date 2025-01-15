using UnityEngine;

public class PlaySoundOnAngle : MonoBehaviour
{
    public new HingeJoint hingeJoint; // Assign the HingeJoint in the Inspector
    public AudioSource audioSource; // Assign an AudioSource with your sound
    public float positiveTargetAngle = 20f; // Upper limit angle in degrees
    public float negativeTargetAngle = -20f; // Lower limit angle in degrees
    public float angleTolerance = 1f; // Allow some tolerance around the target angles
    public float angleTolerance2 = 1f;

    private bool soundPlayed = false;

    void FixedUpdate()
    {
        // Get the current angle of the hinge joint
        float currentAngle = hingeJoint.angle;
        // Debug.Log(currentAngle);

        // Check if the angle exceeds either the positive or negative target
        if (!soundPlayed &&
            ((currentAngle >= positiveTargetAngle - angleTolerance &&
             currentAngle < positiveTargetAngle + angleTolerance2) ||
            (currentAngle <= negativeTargetAngle + angleTolerance &&
             currentAngle > negativeTargetAngle - angleTolerance2)))
        {
            Debug.Log("KATZE");
            // Play the sound
            audioSource.Play();

            // Set the flag to prevent the sound from playing again
            soundPlayed = true;
        }

        // Reset the flag if the angle moves back within the range (optional)
        if (soundPlayed &&
            (currentAngle < positiveTargetAngle - angleTolerance &&
             currentAngle > negativeTargetAngle + angleTolerance) ||
             soundPlayed && 
            (currentAngle > positiveTargetAngle + angleTolerance2 ||
             currentAngle < negativeTargetAngle - angleTolerance2))
        {
            soundPlayed = false;
        }
    }
}
