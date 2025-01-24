using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    [SerializeField]
    LayerMask ghostLayer;
    [SerializeField]
    float initialTimerValue = 2f;
    float scanTimer;
    bool startTimer = false;
    AudioManager audioManager;
    TrackToScanner tracker;
    Collider focusedTarget;
    [SerializeField] NoteUpdater updater;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject manager = GameObject.Find("AudioManager");
        tracker = manager.GetComponent<TrackToScanner>();
        audioManager= manager.GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        ScanObject();
    }

    void ScanObject()
    {
        Collider currentTarget = tracker.GetCurrentTarget();
        // reset timer if current target is null or target has switched
        if (currentTarget != null && focusedTarget == currentTarget && LayerMask.Equals(LayerMask.GetMask(LayerMask.LayerToName(currentTarget.gameObject.layer)), ghostLayer.value))
        {
            ScanTimer();
            if (scanTimer <= 0f)
            {
                Debug.Log("Scanned!" + currentTarget.gameObject.name);
                StartCoroutine(UpdateSourcesAfterDelete(0.01f, currentTarget.gameObject));
            } 
        }
        else
        {
            scanTimer = initialTimerValue;
            startTimer = false;
        }
        focusedTarget = currentTarget;
    }

    void ScanTimer()
    {
        if (startTimer == false)
        {
            scanTimer = initialTimerValue;
            startTimer = true;
        }
        if (startTimer && scanTimer > 0f)
        {
            tracker.UpdateTargetCroshairColor(scanTimer, initialTimerValue);
            scanTimer -= Time.deltaTime;
        }
    }
    IEnumerator UpdateSourcesAfterDelete(float delayTime, GameObject delete)
    {
        //Wait for the specified delay time before continuing.
        Destroy(delete);
        yield return new WaitForSeconds(delayTime);
        audioManager.UpdateSources();
        updater.UpdateGhostCounter();
        //Do the action after the delay time has finished.
    }
}
