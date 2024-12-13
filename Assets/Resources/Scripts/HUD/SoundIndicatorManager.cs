using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SoundIndicator : MonoBehaviour
{
    public GameObject prefab;
    public GameObject audioManager;
    public float indicatorScale;

    public float maxFadeTime;
    Dictionary<string, IndicatorData> indicatorDataset = new Dictionary<string, IndicatorData>();
    
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
                float screenWidth = gameObject.GetComponent<RectTransform>().rect.width;
                float screenHeight = gameObject.GetComponent<RectTransform>().rect.height;
                GameObject newIndicator = Instantiate(prefab, new Vector3(screenWidth/2, screenHeight/2, 0), Quaternion.identity);
                newIndicator.transform.SetParent(gameObject.transform);
                newIndicator.transform.localScale = new Vector3(indicatorScale, indicatorScale, indicatorScale);
                indicatorDataset.Add(item.name, new IndicatorData(newIndicator, item, maxFadeTime));
            }
        }

        Dictionary<string, IndicatorData> tempData = new Dictionary<string, IndicatorData>(indicatorDataset);
        foreach (var key in tempData.Keys) 
        {
            if (indicatorDataset[key].lifeTime > 0)
            {
                IndicatorData data = indicatorDataset[key];
                data.lifeTime -= Time.deltaTime;
                indicatorDataset[key] = data;
                indicatorDataset[key].indicatorGameObject.transform.localEulerAngles = new Vector3(0, 0, -indicatorDataset[key].audioData.relativeAngle);
            }
            else
            {
                Destroy(indicatorDataset[key].indicatorGameObject);
                indicatorDataset.Remove(key);
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
