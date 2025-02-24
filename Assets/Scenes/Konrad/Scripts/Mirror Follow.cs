using UnityEngine;

public class MirrorFollow : MonoBehaviour
{
    public Transform playerTarget;
    public Transform mirror;

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 localPlayer = mirror.InverseTransformPoint(playerTarget.position);
        transform.position = mirror.TransformPoint(new Vector3(localPlayer.x, localPlayer.y, -localPlayer.z));

        Vector3 lookAtMirror = mirror.TransformPoint(new Vector3(-localPlayer.x, localPlayer.y, localPlayer.z));
        transform.LookAt(lookAtMirror);
    }
}
