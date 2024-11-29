using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using System;
using System.Runtime.InteropServices;


public class AudioManager : MonoBehaviour
{
    AudioSource[] sources;
    [SerializeField]
    private Camera cam;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sources = FindObjectsByType<AudioSource>(FindObjectsSortMode.None);
    }

    // Update is called once per frame
    void Update()
    {
        List<AudioData> audioList = GetAudiosInRange();

        Debug.Log(audioList[0].name);
        Debug.Log(audioList[0].relativeAngle);
        Debug.Log(audioList[0].distance);
        Debug.Log(audioList[0].relativeHeight);


    }

    List<AudioData> GetAudiosInRange()
    {
        List<AudioData> audioSources = new();
        foreach (AudioSource source in sources)
        {
            float range = source.maxDistance;
            Vector3 axis = Vector3.up;
            float distance = Vector3.Distance(source.transform.position, cam.transform.position);
            if (distance< range)
            {
                Vector3 perpendicularCam = Vector3.Cross(axis, cam.transform.TransformDirection(Vector3.forward));
                Vector3 perpendicularAudio = Vector3.Cross(axis, source.transform.position);
                float relativeAngle = Vector3.SignedAngle(perpendicularCam, perpendicularAudio, axis);
                float relativeHeight = source.transform.position.y - cam.transform.position.y;
                audioSources.Add(new AudioData(source.name, relativeAngle, distance, relativeHeight));
                Debug.Log(source.name);
            }
        }
        return audioSources;
    }

}

struct AudioData
{
    public AudioData(string name, float relativeAngle, float distance, float relativeHeight)
    {
        this.name = name;
        this.relativeAngle = relativeAngle;
        this.distance = distance;
        this.relativeHeight = relativeHeight;
    }
    public string name;
    public float relativeAngle;
    public float distance;
    public float relativeHeight;
}