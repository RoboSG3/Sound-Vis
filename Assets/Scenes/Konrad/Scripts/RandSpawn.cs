using UnityEngine;

public class RandSpawn : MonoBehaviour
{   
    [SerializeField] private GameObject spawnObject;
    [SerializeField] private  int maxSpawnAmount;
    private Transform spawner;

    public void Spawn()
    {
        var spawnX = Random.Range(
            spawner.position.x - spawner.localScale.x / 2,
            spawner.position.x + spawner.localScale.x / 2);
        var spawnY = Random.Range(
            spawner.position.y,
            spawner.position.y + spawner.localScale.y);
        var spawnZ = Random.Range(
            spawner.position.z - spawner.localScale.z / 2,
            spawner.position.z + spawner.localScale.z / 2);
        
        Vector3 spawnPos = new Vector3(spawnX, spawnY, spawnZ);
        
        var spawenedObject = Instantiate(spawnObject, spawnPos, Quaternion.identity);
        spawenedObject.transform.SetParent(spawner);
    }

    void Start()
    {
        spawner = transform;
        //for (int i = Random.Range(0,maxSpawnAmount); i >0; i--)
        for (int i = maxSpawnAmount; i > 0; i--)
        { 
            Spawn();
        }
       
    }
}
