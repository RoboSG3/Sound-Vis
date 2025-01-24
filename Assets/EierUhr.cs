using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class EierUhr : MonoBehaviour
{
   [SerializeField] GameObject numbers;
   [SerializeField] ParticleSystem particel;
   public XRToggleButton xRToggleButton;
   public SocketChecker socketChecker;
   public AudioSource audioSource;
   public int playCount = 3;
   public float delayBetweenPlays = 0f;
   private int timer;
   private float temp = 0.0f;
   private bool started = false;
   private TextMeshProUGUI _textMeshProUGUI;
   [SerializeField] NoteUpdater updater;

    private void Start()
   {
      _textMeshProUGUI = numbers.GetComponent<TMPro.TextMeshProUGUI>();
   }

   void Update()
   {
      temp += Time.deltaTime;
      if (temp >= 0.5f && started)
      {
         temp = 0.0f;
         timer--;
      }

      if (timer == 0 && started)
      {
         started = false; 
         Debug.Log("Started");
         updater.CompleteQuest("Koche ein Ei");
         StartCoroutine(PlaySoundRepeatedly(playCount, delayBetweenPlays));
         particel.Stop();
        }
      UpdateDisplay();
   }

    private IEnumerator PlaySoundRepeatedly(int times, float delay)
    {
        for (int i = 0; i < times; i++)
        {
            audioSource.Play();
            yield return new WaitForSeconds(audioSource.clip.length + delay);
        }
    }

    public void IncreaseByMin()
   {
      Debug.Log("IncreaseByMin");
      timer += 60;
      if (timer >= 600)
      {
         timer = 0;
      }
   }
   
   public void IncreaseBySec()
   {
      Debug.Log("IncreaseBySec");
      timer += 10;
      if (timer >= 600)
      {
         timer = 0;
      }
   }

   // ReSharper disable Unity.PerformanceAnalysis
   private void UpdateDisplay()
   {
      int minutes = Mathf.FloorToInt(timer / 60);
      int seconds = Mathf.FloorToInt(timer % 60);
      //Debug.Log(minutes + ":" + seconds);
      
      _textMeshProUGUI.text = string.Format("{0:00}:{1:00}", minutes, seconds);
      
   }

   public void startEierUhr()
   {
      if (socketChecker.isObjectInside && xRToggleButton.isToggled && timer >= 180)
      {
         started = true;
         Debug.Log("startEierUhr");
         particel.Play();
      }
      else
      {
         _textMeshProUGUI.text = string.Format("Not ready yet");
      }
     
      
   }
}
