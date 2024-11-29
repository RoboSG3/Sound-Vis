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
        Dictionary<String, float> audioAngles = getAudiosInRange();

        Debug.Log(audioAngles.GetValueOrDefault("Cat"));
    }

    Dictionary<String, float> getAudiosInRange()
    {
        Dictionary<String, float> audioAngles = new Dictionary<string, float>();
        foreach (AudioSource source in sources)
        {
            float range = source.maxDistance;
            Vector3 axis = Vector3.up;
            if (Vector3.Distance(source.transform.position, cam.transform.position) < range) {
                Vector3 perpendicularCam = Vector3.Cross(axis, cam.transform.TransformDirection(Vector3.forward));
                Vector3 perpendicularAudio = Vector3.Cross(axis, source.transform.position);
                audioAngles.Add(source.name, Vector3.SignedAngle(perpendicularCam, perpendicularAudio, axis));
                Debug.Log(source.name);
            }
        }
        return audioAngles;
    }
}
