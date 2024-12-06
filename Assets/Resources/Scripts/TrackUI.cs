using UnityEngine;

public class TrackUI : MonoBehaviour
{
    [SerializeField]
    private Transform Subject;
    [SerializeField]
    private Camera ScannerCam;
    // Update is called once per frame
    void Update()
    {
        if (Subject)
        {
            transform.position = ScannerCam.WorldToScreenPoint(Subject.position);
        }
    }
}
