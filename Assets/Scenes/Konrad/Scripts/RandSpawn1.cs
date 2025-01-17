using UnityEngine;
using System.Collections;

public class RandSpawn1 : MonoBehaviour
{   
    [SerializeField] private GameObject spawnObject;
    [SerializeField] private  int maxSpawnAmount;
    private Transform spawner;
    private int spawnCount = 0;
    [SerializeField] AudioManager audioManager;
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
        Transform childTransform = spawenedObject.transform.Find("CatAudio");
        childTransform.gameObject.name = "CatAudio " + spawnCount;
        spawnCount++;

    }

    IEnumerator WaitToSpawn()
    {
        spawner = transform;

        for (int i = maxSpawnAmount; i > 0; i--)
        {
            Spawn();
            audioManager.UpdateSources();

            yield return new WaitForSeconds(2);
        }
    }

    void Start()
    {
        StartCoroutine(WaitToSpawn());
    }
}
