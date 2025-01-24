using UnityEngine;

public class HandControllerSwitch : MonoBehaviour
{
    [SerializeField]
    GameObject leftHand;
    [SerializeField]
    GameObject rightHand;
    [SerializeField]
    GameObject leftController;
    [SerializeField]
    GameObject rightController;
    SkinnedMeshRenderer leftHandMesh;
    SkinnedMeshRenderer rightHandMesh;
    void Start()
    {
        leftHandMesh = leftHand.GetComponent<SkinnedMeshRenderer>();
        rightHandMesh = rightHand.GetComponent<SkinnedMeshRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        toggleInputMode();
    }

    void toggleInputMode()
    {
        if (leftHandMesh.enabled || rightHandMesh.enabled)
        {
            leftController.SetActive(false);
            rightController.SetActive(false);
        } else
        {
            leftController.SetActive(true);
            rightController.SetActive(true);
        }
    }
}
