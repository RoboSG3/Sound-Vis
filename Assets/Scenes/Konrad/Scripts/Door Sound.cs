using UnityEngine;

public class PlaySoundOnAngle : MonoBehaviour
{
    public new HingeJoint hingeJoint; // Assign the HingeJoint in the Inspector
    public AudioSource audioSource; // Assign an AudioSource with your sound
    public float positiveTargetAngle = 30f; // Upper limit angle in degrees
    public float negativeTargetAngle = -30f; // Lower limit angle in degrees
    public float angleTolerance = 1f; // Allow some tolerance around the target angles

    private bool soundPlayed = false;

    void FixedUpdate()
    {
        // Get the current angle of the hinge joint
        float currentAngle = hingeJoint.angle;

        // Check if the angle exceeds either the positive or negative target
        if (!soundPlayed &&
            (currentAngle >= positiveTargetAngle - angleTolerance ||
             currentAngle <= negativeTargetAngle + angleTolerance))
        {
            // Play the sound
            audioSource.Play();

            // Set the flag to prevent the sound from playing again
            soundPlayed = true;
        }

        // Reset the flag if the angle moves back within the range (optional)
        if (soundPlayed &&
            (currentAngle < positiveTargetAngle - angleTolerance &&
             currentAngle > negativeTargetAngle + angleTolerance))
        {
            soundPlayed = false;
        }
    }
}
