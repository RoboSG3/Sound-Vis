using System.Collections;
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

        leftHand.transform.SetPositionAndRotation(handDistanceToMove[0], Quaternion.Euler(handDegreesToRotate[0]));
        Vector3 originalPosition = leftHand.transform.position;
        Vector3 targetPosition = originalPosition + handDistanceToMove[1];
        Vector3 originalRotation = leftHand.transform.rotation.eulerAngles;
        Vector3 targetRotation = originalRotation + handDegreesToRotate[1];

        startTime = Time.time;
        elapsedTime = 0;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            leftHand.transform.SetPositionAndRotation(Vector3.Lerp(originalPosition, targetPosition, t), Quaternion.Euler(Vector3.Lerp(originalRotation, targetRotation, t)));
            elapsedTime = Time.time - startTime;
            yield return null;
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
}
