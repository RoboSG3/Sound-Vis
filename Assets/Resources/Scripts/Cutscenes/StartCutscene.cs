using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCutscene : CutsceneElementBase
{
    private Camera cam;
    [SerializeField] private GameObject leftHand;
    [SerializeField] private Vector3 degreesToRotate;
    [SerializeField] private Vector3[] handDegreesToRotate;
    [SerializeField] private Vector3[] handDistanceToMove;
    public override void Execute()
    {
        Debug.Log("Exceute");
        //Debug.Log(cutsceneHandler.cam);
        cam = GetComponent<CutsceneHandler>().cam;
        //cam = cutsceneHandler.cam;
        StartCoroutine(PanCoroutine());
        //StartCoroutine(HandGrabCoroutine());
    }

    private IEnumerator PanCoroutine()
    {
        Vector3 originalCamRotation = cam.transform.rotation.eulerAngles;
        Vector3 targetCamRotation = originalCamRotation + degreesToRotate;

        float startTime = Time.time;
        float elapsedTime = 0;

        while (elapsedTime < duration) {
            float t = elapsedTime / duration;
            cam.transform.rotation = Quaternion.Euler(Vector3.Lerp(originalCamRotation, targetCamRotation, t));
            elapsedTime = Time.time - startTime;
            yield return null;
        }
        cam.transform.rotation = Quaternion.Euler(targetCamRotation);
        Debug.Log(leftHand.transform.position);
        leftHand.transform.SetLocalPositionAndRotation(handDistanceToMove[0], Quaternion.Euler(handDegreesToRotate[0]));
        Vector3 originalPosition = leftHand.transform.localPosition;
        Debug.Log(originalPosition);
        Vector3 targetPosition = originalPosition + handDistanceToMove[1];
        Vector3 originalRotation = leftHand.transform.localRotation.eulerAngles;
        Vector3 targetRotation = originalRotation + handDegreesToRotate[1];

        List<Transform[]> finger = getHandChildren();
        for (int i = 0; i < handDistanceToMove.Length; i++) {
            startTime = Time.time;
            elapsedTime = 0;
            if (i > 0) 
            {
                originalPosition = handDistanceToMove[i - 1];
                originalRotation = handDegreesToRotate[i - 1];
            }
            while (elapsedTime < duration)
            {
                float t = elapsedTime / duration;
                leftHand.transform.SetLocalPositionAndRotation(Vector3.Lerp(originalPosition, handDistanceToMove[i], t), Quaternion.Euler(Vector3.Lerp(originalRotation, handDegreesToRotate[i], t)));
                elapsedTime = Time.time - startTime;
                yield return null;
            }
        }


        //cam.transform.rotation = Quaternion.Euler(targetRotation);

        cutsceneHandler.PlayNextElement();
    }

    //private IEnumerator HandGrabCoroutine()
    //{

    //}

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    private List<Transform[]> getHandChildren()
    {
        List<Transform[]> children = new();
        Transform[] childer = leftHand.GetComponentsInChildren<Transform>();
        children.Add(childer);
        foreach(Transform[] child in children)
        {
            Debug.Log("Child: " + child[0].position);
            Debug.Log("Child: " + child[1].position);
        }
        return children;
    }
}
