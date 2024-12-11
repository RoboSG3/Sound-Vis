using UnityEngine;

public class LeverController : MonoBehaviour
{
    [SerializeField] private GameObject spawner;
    [SerializeField] private HingeJoint lever;

    private float activationAngle = 70f;
    private bool active = false;

    void FixedUpdate()
    {
        float angle = lever.angle;

        if (angle >= activationAngle && active == false)
        {
            ActivateSpawner();
            Debug.Log("Spawner Activated");
        }
        else if (angle < activationAngle && active == true)
        {
            DeactivateSpawner();
            Debug.Log("Spawner Deactivated");
        }

    }

    private void ActivateSpawner()
    {
        active = true;
        spawner.SetActive(true);
    }

    private void DeactivateSpawner()
    {
        active = false;
        spawner.SetActive(false);
    }

}
