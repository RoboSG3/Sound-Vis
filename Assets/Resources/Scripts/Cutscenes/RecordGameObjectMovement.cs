using System.Collections.Generic;
using UnityEngine;

public class RecordGameObjectMovement : MonoBehaviour
{
    [SerializeField] GameObject[] recordObjects;
    private Dictionary<string, List<Transform>> transforms = new();

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //    while (Input.GetKeyUp(KeyCode.Space))
                Record();             
    }

    void Record()
    {
        foreach (GameObject gameObject in recordObjects)
        {
            if (!transforms.ContainsKey(gameObject.name))
            {
                List<Transform> list = new()
                {
                    gameObject.transform
                };
                transforms.Add(gameObject.name, list);
            }
            else
            {
                List<Transform> objectTransforms = transforms[gameObject.name];
                objectTransforms.Add(gameObject.transform);
                transforms.Remove(gameObject.name);
                transforms.Add(gameObject.name, objectTransforms);
                string currentTransforms = "";
                foreach (Transform transform in objectTransforms)
                {
                    currentTransforms+= transform.name + ": Position: " + transform.position + ", Rotation: " + transform.rotation.eulerAngles;
                }
                Debug.Log("Transforms of " + gameObject.name + ": " + currentTransforms);
            }
        }
    }
}
