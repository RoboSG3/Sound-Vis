using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;
using UnityEngine.Audio;
using UnityEngine.UI;


public class IndicatorIcon : MonoBehaviour
{
    [SerializeField] GameObject arrowPivot;
    [SerializeField] GameObject icon;
    [SerializeField] float maxScale;
    [SerializeField] float maxScaleDistance;
    private float defaultScale = 1;
    [SerializeField] Sprite image;

    [SerializeField] Sprite ghost;
    [SerializeField] Sprite alarm;
    [SerializeField] Sprite eggAlarm;
    [SerializeField] Sprite phone;
    [SerializeField] Sprite closet;
    [SerializeField] Sprite door;
    [SerializeField] Sprite drawer;
    [SerializeField] Sprite smartphone;



    public void UpdateArrowDirection(float relativHeight, float rotation)
    {
        if (relativHeight < 0)
        {
            arrowPivot.transform.localEulerAngles = new Vector3(0, 0, 180 + rotation);
        }
        else
        {
            arrowPivot.transform.localEulerAngles = new Vector3(0, 0, 0 + rotation);
        }
    }
    public void ChangeIconSize(AudioSource source, float distance) 
    {
        // Berechnung basierend auf der Roll-Off-Kurve
        float volumeFactor = 1f; // Standardwert (falls keine D‰mpfung)
        if (source.rolloffMode == AudioRolloffMode.Linear)
        {
            if (distance <= source.minDistance)
                volumeFactor = 1f; // Maximale Lautst‰rke
            else if (distance > source.maxDistance)
                volumeFactor = 0f; // Auﬂerhalb der Reichweite
            else
                volumeFactor = 1f - (distance - source.minDistance) / (source.maxDistance - source.minDistance);
        }
        else if (source.rolloffMode == AudioRolloffMode.Logarithmic)
        {
            if (distance <= source.minDistance)
                volumeFactor = 1f;
            else if (distance > source.maxDistance)
                volumeFactor = 0f;
            else
                volumeFactor = source.minDistance / distance;
        }
        float perceivedVolume = source.volume * volumeFactor;
        //Debug.Log(perceivedVolume);
        float scaleFactor = perceivedVolume * 15;
        scaleFactor = Mathf.Clamp(scaleFactor, 0.7f, 1.7f);

        icon.transform.localScale = new Vector3(defaultScale * scaleFactor, defaultScale * scaleFactor, defaultScale);
    }

    public void RotateImage(float angle)
    {
        icon.transform.localEulerAngles = new Vector3 (0f, 0f, angle);
    }

    public void SetImage(AudioSource source)
    {
        switch (source.tag)
        {
            case "Ghost":
                image = ghost;
                break;

            case "Door":
                image = door;
                break;

            default:
                image = ghost;
                break;
        }
        icon.GetComponent<Image>().overrideSprite = image;
    }
}
