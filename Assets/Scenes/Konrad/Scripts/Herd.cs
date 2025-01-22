using System;
using UnityEngine;

public class Herd : MonoBehaviour
{
   private bool isCooking = false;
   public String m_CompareTag = "Topf";
   public float m_CookTime = 1.0f;

   private void OnCollisionEnter(Collision collision)
   {
      // Check if the object colliding matches the specified tag and cooking has not started
      if (collision.gameObject.CompareTag(m_CompareTag) && !isCooking)
      {
         // Check if the object has a Renderer and it is enabled
         var renderer = GetComponent<Renderer>();
         if (renderer != null && renderer.enabled)
         {
            StartCooking();
         }
      }
   }

   private void StartCooking()
   {
      isCooking = true;
      Debug.Log("Cooking started!");

      // Start the cooking process (e.g., coroutine for timer)
      StartCoroutine(CookingTimer());
   }

   private System.Collections.IEnumerator CookingTimer()
   {
      // Simulate cooking time
      yield return new WaitForSeconds(m_CookTime);

      // Finish cooking
      Debug.Log("Cooking completed!");
      isCooking = false;
      }
   
   }