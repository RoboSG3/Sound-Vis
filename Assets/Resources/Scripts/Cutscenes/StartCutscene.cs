using System.Collections;
using UnityEngine;

public class StartCutscene : CutsceneElementBase
{
    private Camera cam;
    [SerializeField] private Vector3 degreesToRotate;
    public override void Execute()
    {
        Debug.Log("Exceute");
        cam = cutsceneHandler.cam;
        StartCoroutine(PanCoroutine());
    }

    private IEnumerator PanCoroutine()
    {
        Vector3 originalRotation = cam.transform.rotation.eulerAngles;
        Vector3 targetRotation = originalRotation + degreesToRotate;

        float startTime = Time.time;
        float elapsedTime = 0;

        while (elapsedTime < duration) {
            float t = elapsedTime / duration;
            cam.transform.rotation = Quaternion.Euler(Vector3.Lerp(originalRotation, targetRotation, t));
            elapsedTime = Time.time - startTime;
            yield return null;
        }

        cam.transform.rotation = Quaternion.Euler(targetRotation);

        cutsceneHandler.PlayNextElement();
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
