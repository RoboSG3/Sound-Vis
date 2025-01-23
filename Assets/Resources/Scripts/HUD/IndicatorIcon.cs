using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;
using UnityEngine.Audio;
using UnityEngine.UI;


public class IndicatorIcon : MonoBehaviour
{
    [SerializeField] GameObject arrowPivot;
    [SerializeField] GameObject icon;
    [SerializeField] float maxScale = 1.7f;
    [SerializeField] float minScale = 0.7f;
    [SerializeField] float maxScaleFactor = 15f;
    private float defaultScale = 1;
    private Sprite image;

    [SerializeField] Sprite ghost;
    [SerializeField] Sprite alarm;
    [SerializeField] Sprite eggAlarm;
    [SerializeField] Sprite phone;
    [SerializeField] Sprite closet;
    [SerializeField] Sprite door;
    [SerializeField] Sprite drawer;
    [SerializeField] Sprite smartphone;
    [SerializeField] Sprite doorbell;
    [SerializeField] Sprite faucet;
    [SerializeField] Sprite boiling;
    [SerializeField] Sprite collision;
    [SerializeField] Sprite music;


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
        Debug.Log(perceivedVolume);
        float scaleFactor = perceivedVolume * maxScaleFactor;
        scaleFactor = Mathf.Clamp(scaleFactor, minScale, maxScale);

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

            case "Alarm":
                image = alarm;
                break;

            case "Closet":
                image = closet;
                break;

            case "Drawer":
                image = drawer;
                break;

            case "Smartphone":
                image = smartphone;
                break;

            case "Phone":
                image = phone;
                break;

            case "EggAlarm":
                image = eggAlarm;
                break;

            case "Doorbell":
                image = doorbell;
                break;

            case "Boiling":
                image = boiling;
                break;

            case "Collision":
                image = collision;
                break;

            case "Faucet":
                image = faucet;
                break;

            case "Music":
                image = music;
                break;

            default:
                image = ghost;
                break;
        }
        icon.GetComponent<Image>().overrideSprite = image;
    }
}
