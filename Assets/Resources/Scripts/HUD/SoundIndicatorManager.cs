using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SoundIndicator : MonoBehaviour
{
    public Transform playerLocation;
    public GameObject prefab;
    public GameObject audioManager;

    public CanvasGroup soundIndicatorCanvas;
    public float maxFadeTime;
    Dictionary<string, IndicatorData> indicatorDataset;
    
    void Start()
    {
    }


    void Update()
    {
        List<AudioData> audioList = audioManager.GetComponent<AudioManager>().GetAudiosInRange();

        foreach (AudioData item in audioList)
        {
            if (indicatorDataset.ContainsKey(item.name))
            {
                indicatorDataset[item.name] = new IndicatorData(indicatorDataset[item.name].indicatorGameObject, item, maxFadeTime);
            }
            else
            {
                GameObject newIndicator = (Instantiate(prefab) as GameObject);
                newIndicator.transform.parent = this.gameObject.transform;
                indicatorDataset.Add(item.name, new IndicatorData(newIndicator, item, maxFadeTime));
            }
        }

        Dictionary<string, IndicatorData> tempData = indicatorDataset;
        foreach (KeyValuePair<string, IndicatorData> item in tempData) 
        {
            if (item.Value.lifeTime > 0)
            {
                IndicatorData data = indicatorDataset[item.Key];
                data.lifeTime -= Time.deltaTime;
                indicatorDataset[item.Key] = data;
                indicatorDataset[item.Key].indicatorGameObject.transform.localEulerAngles = new Vector3(0, 0, -indicatorDataset[item.Key].audioData.relativeAngle);
            }
            else
            {
                Destroy(indicatorDataset[item.Key].indicatorGameObject);
                indicatorDataset.Remove(item.Key);
            }
        }
    }
}

public struct IndicatorData
{
    public IndicatorData(GameObject indicatorGameObject, AudioData audioData, float lifeTime)
    {
        this.indicatorGameObject = indicatorGameObject;
        this.audioData = audioData;
        this.lifeTime = lifeTime;
    }
    public GameObject indicatorGameObject;
    public AudioData audioData;
    public float lifeTime;

}
