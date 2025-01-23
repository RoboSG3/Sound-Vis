using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using System;
using System.Runtime.InteropServices;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    AudioSource[] sources;
    [SerializeField]
    private Camera cam;
    TrackToScanner tracking;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        tracking = this.GetComponent<TrackToScanner>();
        sources = FindObjectsByType<AudioSource>(FindObjectsSortMode.None);
        tracking.UpdateSources(sources);
    }

    // Update is called once per frame
    void Update()
    {
        List<AudioData> audioList = GetAudiosInRange();

        //foreach (AudioData item in audioList)
        //{
        //    Debug.Log("Name: " + item.name);
        //    Debug.Log("Relative angle: " + item.relativeAngle);
        //    Debug.Log("Relative distance: " + item.relativeDistance);
        //    Debug.Log("Relative height: " + item.relativeHeight);
        //}
    }

    public List<AudioData> GetAudiosInRange()
    {
        List<AudioData> audioSources = new();
        foreach (AudioSource source in sources)
        {
            if (source != null && source.isPlaying)
            {
                float range = source.maxDistance;
                Vector3 relativePosition = source.transform.position - cam.transform.position;
                float relativeDistance = relativePosition.magnitude;
                if (relativeDistance < range)
                {
                    Vector3 perpendicularCam = Vector3.Cross(Vector3.up, new Vector3(cam.transform.forward.x, 0, cam.transform.forward.z));
                    Vector3 perpendicularAudio = Vector3.Cross(Vector3.up, new Vector3(relativePosition.x, 0, relativePosition.z));
                    float relativeAngle = Vector3.SignedAngle(perpendicularCam, perpendicularAudio, Vector3.up);
                    float relativeHeight = source.transform.position.y - cam.transform.position.y;
                    audioSources.Add(new AudioData(source.name, relativeAngle, relativeDistance, relativeHeight, source));
                    //Debug.Log(source.name);
                }
            }
        }
        return audioSources;
    }

    public void UpdateSources()
    {
        Debug.Log("sources deleted");
        sources = FindObjectsByType<AudioSource>(FindObjectsSortMode.None);
        tracking.UpdateSources(sources);
        //Debug.Log(sources[0]);
        //Debug.Log(sources.Length);
    }
}

public struct AudioData
{
    public AudioData(string name, float relativeAngle, float relativeDistance, float relativeHeight, AudioSource source)
    {
        this.name = name;
        this.relativeAngle = relativeAngle;
        this.relativeDistance = relativeDistance;
        this.relativeHeight = relativeHeight;
        this.source = source;
    }
    public string name;
    public float relativeAngle;
    public float relativeDistance;
    public float relativeHeight;
    public AudioSource source;
}