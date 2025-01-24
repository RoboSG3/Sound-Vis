using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneInitiator : MonoBehaviour
{
    private CutsceneHandler cutsceneHandler;

    public void Start()
    {
        cutsceneHandler = GetComponent<CutsceneHandler>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            cutsceneHandler.PlayNextElement();
        }    
    }
}
