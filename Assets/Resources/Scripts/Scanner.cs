using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    [SerializeField]
    LayerMask collectableLayer;
    [SerializeField]
    float scanTimer = 2f;
    [SerializeField]
    AudioManager audioManager;
    bool startTimer = false;
    //bool focusSource = false;
    [SerializeField]
    LineRenderer scanLine;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CastRay();
    }

    void CastRay()
    {
        Vector3 center = this.gameObject.transform.position;
        Vector3 direction = this.gameObject.transform.forward;
        Ray scanRay = new Ray(center, direction);
        if (Physics.Raycast(scanRay, out RaycastHit scanHit, 100, collectableLayer))
        {
            scanLine.SetPosition(0, center);
            scanLine.SetPosition(1, scanHit.point);
            ScanTimer();
            if (scanTimer <= 0f)
            {
 
                Debug.Log("Scanned!" + scanHit.collider.gameObject.name);
                StartCoroutine(UpdateSourcesAfterDelete(0.01f, scanHit.collider.gameObject));
                Destroy(scanLine);
            }
        } else
        {
            scanTimer = 2f;
            startTimer = false;
        }
        //Debug.Log(scanTimer);

    }

    void ScanTimer()
    {
        if (startTimer == false)
        {
            startTimer = true;
        }
        if (startTimer && scanTimer > 0f)
        {
            scanTimer -= Time.deltaTime;
        }
    }
    IEnumerator UpdateSourcesAfterDelete(float delayTime, GameObject delete)
    {
        //Wait for the specified delay time before continuing.
        Destroy(delete);
        yield return new WaitForSeconds(delayTime);
        audioManager.UpdateSources();
        
        //Do the action after the delay time has finished.
    }
}
