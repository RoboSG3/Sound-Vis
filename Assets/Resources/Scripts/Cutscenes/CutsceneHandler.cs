using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CutsceneHandler :MonoBehaviour
{
    public Camera cam;
    private CutsceneElementBase[] cutsceneElements;
    private int index = -1;

    public void Start()
    {
        cutsceneElements= GetComponents<CutsceneElementBase>();
    }

    private void ExecuteCurrentElement()
    {
        if (index >= 0 && index < cutsceneElements.Length)
        {
            Debug.Log(cam);
            cutsceneElements[index].Execute();
        }
    }

    public void PlayNextElement()
    {
        index++;
        ExecuteCurrentElement();
    }
}
