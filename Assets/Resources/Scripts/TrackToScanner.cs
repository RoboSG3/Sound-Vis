using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class TrackToScanner : MonoBehaviour
{
    [SerializeField]
    Camera scannerCam;
    Plane[] cameraFrustum;
    AudioManager audioManager;
    AudioSource[] scannables;
    [SerializeField]
    Canvas targetCrosshair;
    Vector3 crosshairPos;
    bool manualTarget = false;
    Collider currentTarget;

    void Start()
    {
        audioManager= GetComponent<AudioManager>();
        crosshairPos = targetCrosshair.GetComponent<RectTransform>().localPosition;
    }
    // Update is called once per frame
    void Update()
    {
        UpdateCrosshair();
    }

    public void UpdateCrosshair()
    {
        cameraFrustum = GeometryUtility.CalculateFrustumPlanes(scannerCam);
        List<Collider> scansInsideFrustum = new List<Collider>();
        foreach (AudioSource source in scannables)
        {
            if (source.gameObject != null)
            {
                Collider collider = source.gameObject.GetComponentInParent<Collider>();
                if (GeometryUtility.TestPlanesAABB(cameraFrustum, collider.bounds))
                {
                    scansInsideFrustum.Add(collider);
                }
            }
        }
        //Vector3 scannerCenter = new(128, 128, 0);
        //scansInsideFrustum.Sort((a, b) => {
        //    return (scannerCam.WorldToScreenPoint(a.transform.position) - scannerCenter).magnitude.CompareTo((scannerCam.WorldToScreenPoint(b.transform.position) - scannerCenter).magnitude);
        //});

        // for eventual manual targeting with controls
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    manualTarget = !manualTarget;
        //}

        if (!manualTarget)
        {
            scansInsideFrustum.Sort((a, b) => scannerCam.WorldToScreenPoint(a.transform.position).z.CompareTo(scannerCam.WorldToScreenPoint(b.transform.position).z));

            foreach (Collider collider in scansInsideFrustum)
            {
                targetCrosshair.enabled = true;
                Vector3 screenPos = scannerCam.WorldToScreenPoint(collider.transform.position);
                if (screenPos.x > 64 && screenPos.x < 192 && screenPos.y > 64 && screenPos.y < 192)
                {
                    currentTarget = collider;
                    targetCrosshair.GetComponent<RectTransform>().anchoredPosition3D = screenPos;
                    Debug.Log(targetCrosshair.GetComponent<RectTransform>().localScale);
                    Debug.Log(1 / screenPos.z);
                    targetCrosshair.GetComponent<RectTransform>().localScale = Vector3.one *  Mathf.Max((1 / screenPos.z), 1);
                    return;
                }
            }
            targetCrosshair.enabled = false;
            currentTarget = null;
        } else
        {
            //if (Input.GetKeyDown(KeyCode.E))
            //{
            //    ManuallySwitchTarget(scansInsideFrustum);
            //}
        }
    }

    //public void ManuallySwitchTarget(List<Collider> collidersInFrustum)
    //{
    //    manualTarget = true;
    //    if (currentTarget < collidersInFrustum.Count && currentTarget + 1 < collidersInFrustum.Count)
    //    {
    //        currentTarget += 1;
    //    }
    //}
    public Collider GetCurrentTarget()
    {
        return currentTarget;
    }
    public void UpdateTargetCroshairColor(float scanTimer, float initialTimerValue)
    {
        float currentProgress = scanTimer / initialTimerValue;
        Color currentColor = Color.Lerp(Color.green, Color.red, 1 - currentProgress);
        foreach (Image crosshairComponent in targetCrosshair.GetComponentsInChildren<Image>()) 
        { 
            crosshairComponent.color = currentColor;
        }
    }
    public void UpdateSources(AudioSource[] sources)
    {
        scannables = sources;
    }
}
