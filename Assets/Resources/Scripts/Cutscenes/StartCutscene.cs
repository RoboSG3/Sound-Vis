using UnityEngine;

public class StartCutscene : CutsceneElementBase
{
    public override void Excecute()
    {
        Debug.Log("Exceuting " + name);
    }
}
