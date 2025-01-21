using System.Collections;
using UnityEngine;

public class CutsceneElementBase : MonoBehaviour
{
    public float duration;
    private CutsceneHandler cutsceneHandler;

    public void Start()
    {
        cutsceneHandler= GetComponent<CutsceneHandler>();
    }

    public virtual void Excecute()
    {

    }

    protected IEnumerator WaitAndAdvance()
    {
        yield return new WaitForSeconds(duration);
        cutsceneHandler.PlayNextElement();
    }
}
