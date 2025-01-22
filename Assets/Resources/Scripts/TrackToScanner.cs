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
    AudioSource[] scannables;
    [SerializeField]
    LayerMask ghostLayer;
    [SerializeField]
    LayerMask allObstacles;
    [SerializeField]
    Canvas targetCrosshair;
    bool manualTarget = false;
    Collider currentTarget;

    // Update is called once per frame
    void Update()
    {
        UpdateCrosshair();
    }

    public void UpdateCrosshair()
    {
        cameraFrustum = GeometryUtility.CalculateFrustumPlanes(scannerCam);
        List<Collider> scansInsideFrustum = new();
        foreach (AudioSource source in scannables)
        {
            //Debug.Log(LayerMask.GetMask(LayerMask.LayerToName(source.gameObject.layer)) + " " + ghostLayer.value);
            if (source.gameObject != null && Equals(LayerMask.GetMask(LayerMask.LayerToName(source.gameObject.layer)), ghostLayer.value))
            {
                Collider collider = source.gameObject.GetComponentInParent<Collider>();
                if (GeometryUtility.TestPlanesAABB(cameraFrustum, collider.bounds))
                {
                    scansInsideFrustum.Add(collider);
                }
            }
        }
        if (!manualTarget)
        {
            scansInsideFrustum.Sort((a, b) => scannerCam.WorldToScreenPoint(a.transform.position).z.CompareTo(scannerCam.WorldToScreenPoint(b.transform.position).z));
            
            foreach (Collider collider in scansInsideFrustum)
            {
                    if (Physics.Raycast(scannerCam.transform.position, collider.transform.position, Vector3.Distance(collider.transform.position, scannerCam.transform.position), allObstacles.value))
                    {
                        return;
                    }
                    targetCrosshair.enabled = true;
                    Vector3 screenPos = scannerCam.WorldToScreenPoint(collider.transform.position);
                    if (screenPos.x > 64 && screenPos.x < 192 && screenPos.y > 64 && screenPos.y < 192)
                    {
                        currentTarget = collider;
                        targetCrosshair.GetComponent<RectTransform>().anchoredPosition3D = screenPos;
                        targetCrosshair.GetComponent<RectTransform>().localScale = Vector3.one * Mathf.Max((1 / screenPos.z), 1);
                        return;
                    }
            }
            targetCrosshair.enabled = false;
            currentTarget = null;
        }
    }

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
