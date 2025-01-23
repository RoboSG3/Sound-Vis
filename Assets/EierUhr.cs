using System;
using TMPro;
using UnityEngine;

public class EierUhr : MonoBehaviour
{
   [SerializeField] GameObject numbers;
   public XRToggleButton xRToggleButton;
   public SocketChecker socketChecker;
   public AudioSource audioSource;
   private int timer;
   private float temp = 0.0f;
   private bool started = false;
   private TextMeshProUGUI _textMeshProUGUI;

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

      if (timer == 0)
      {
         started = false; 
         Debug.Log("Started");
         audioSource.Play();
      }
      UpdateDisplay();
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
      Debug.Log(minutes + ":" + seconds);
      
      _textMeshProUGUI.text = string.Format("{0:00}:{1:00}", minutes, seconds);
      
   }

   public void startEierUhr()
   {
      if (socketChecker.isObjectInside && xRToggleButton.isToggled && timer > 0)
      {
         started = true;
         Debug.Log("startEierUhr");
      }
      else
      {
         _textMeshProUGUI.text = string.Format("Not ready yet");
      }
     
      
   }
}
