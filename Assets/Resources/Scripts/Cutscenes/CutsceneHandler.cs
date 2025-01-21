using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CutsceneHandler :MonoBehaviour
{
    private CutsceneElementBase[] cutsceneElements;
    private int index = -1;

    public void Start()
    {
        cutsceneElements= GetComponents<CutsceneElementBase>();
    }

    private void ExceuteCurrentElement()
    {
        if (index >= 0 && index < cutsceneElements.Length)
        {
            cutsceneElements[index].Excecute();
        }
    }

    public void PlayNextElement()
    {
        index++;
        ExceuteCurrentElement();
    }
}
