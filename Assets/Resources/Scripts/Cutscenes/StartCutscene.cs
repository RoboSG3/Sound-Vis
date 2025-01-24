using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StartCutscene : CutsceneElementBase
{
    private Camera cam;
    [SerializeField] private Animator leftHandAnim;
    [SerializeField] private Animator brilleAnim;
    [SerializeField] private GameObject brille;
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
        //Vector3 originalCamRotation = cam.transform.rotation.eulerAngles;
        //Vector3 targetCamRotation = originalCamRotation + degreesToRotate;

        //float startTime = Time.time;
        //float elapsedTime = 0;

        //while (elapsedTime < duration) {
        //    float t = elapsedTime / duration;
        //    cam.transform.rotation = Quaternion.Euler(Vector3.Lerp(originalCamRotation, targetCamRotation, t));
        //    elapsedTime = Time.time - startTime;
        //    yield return null;
        //}
        //cam.transform.rotation = Quaternion.Euler(targetCamRotation);
        yield return new WaitForSeconds(4.5f);
        leftHandAnim.enabled = true;
        yield return new WaitForSeconds(0.7f);
        brilleAnim.enabled = true;
        brille.transform.parent = leftHandAnim.transform;
        brille.transform.localPosition = new Vector3(0.0934f, -0.1668f, 0.1728f);
        yield return new WaitForSeconds(0.5f);
        //startTime = Time.time;
        //elapsedTime = 0;
 
        //while (elapsedTime < (duration * 2))
        //{
        //    float t = elapsedTime / (duration * 2);
        //    cam.transform.rotation = Quaternion.Euler(Vector3.Lerp(targetCamRotation, originalCamRotation, t));
        //    elapsedTime = Time.time - startTime;
        //    yield return null;
        //}
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
