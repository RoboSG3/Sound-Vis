using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    [SerializeField]
    LayerMask collectableLayer;
    [SerializeField]
    LineRenderer scanLine;
    float scanTimer = 2f;
    bool startTimer = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CastRay();
        ScanTimer();
    }

    void CastRay()
    {
        Vector3 center = this.gameObject.transform.position;
        Vector3 direction = this.gameObject.transform.forward;
        Ray scanRay = new Ray(center, direction);
        if (Physics.Raycast(scanRay, out RaycastHit scanHit, 100, collectableLayer))
        {
            if (startTimer == false)
            {
                startTimer = true;
            }
            scanLine.SetPosition(0, center);
            scanLine.SetPosition(1, scanHit.point);
            Debug.Log("Hit object: " + scanHit.collider.gameObject.name);
            Debug.DrawLine(center, scanHit.point, Color.green, 2);
        }
        Debug.Log(scanTimer);
        if (scanTimer <= 0f)
        {
            Debug.Log("Scanned!" + scanHit.collider.gameObject.name);
        }
    }

    void ScanTimer()
    {
        if (startTimer && scanTimer > 0f)
        {
            scanTimer -= Time.deltaTime;
        }else if (startTimer && scanTimer <= 0f)
        {
            startTimer = false;
            scanTimer = 2f;
        }
    }

}
