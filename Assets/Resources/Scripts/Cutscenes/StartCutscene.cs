using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StartCutscene : CutsceneElementBase
{
    private Camera cam;
    [SerializeField] private Animation leftHandAnim;
    [SerializeField] private Vector3 degreesToRotate;
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
        leftHandAnim.Play();
        startTime = Time.time;
        elapsedTime = 0;
        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            cam.transform.rotation = Quaternion.Euler(Vector3.Lerp(targetCamRotation, originalCamRotation, t));
            elapsedTime = Time.time - startTime;
            yield return null;
        }
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
