using UnityEngine;
using System.Collections;

public class RandSpawn1 : MonoBehaviour
{   
    [SerializeField] private GameObject spawnObject;
    [SerializeField] private  int minSpawnAmount;
    [SerializeField] private  int maxSpawnAmount;
    [SerializeField] private  float delayTime;
    [SerializeField] AudioManager audioManager;
    
    private Transform spawner;
    private int spawnAmount;
    private int spawnedCount = 0;
    private Collider[] cachedColliders;
    
    void Start()
    {
        spawner = transform;
        cachedColliders = spawner.GetComponentsInChildren<Collider>();
        spawnAmount = Random.Range(minSpawnAmount, maxSpawnAmount);
        
        if (cachedColliders.Length == 0)
        {
            Debug.LogWarning("No colliders found under the specified parent object!");
            return;
        }

        if (audioManager == null || spawnObject == null)
        {
            Debug.LogError("AudioManager or SpawnObject is not assigned!");
            return;
        }
        StartCoroutine(WaitToSpawn());
    }
    
    // ReSharper disable Unity.PerformanceAnalysis
    public void Spawn()
    {
        
        Collider selectedCollider = cachedColliders[Random.Range(0, cachedColliders.Length)];
        Bounds bounds = selectedCollider.bounds;

        // Generate random position within the bounds of the selected collider
        float spawnX = Random.Range(bounds.min.x, bounds.max.x);
        float spawnY = Random.Range(bounds.min.y, bounds.max.y);
        float spawnZ = Random.Range(bounds.min.z, bounds.max.z);

        Vector3 spawnPos = new Vector3(spawnX, spawnY, spawnZ);
        var spawenedObject = Instantiate(spawnObject, spawnPos, Quaternion.identity);
        spawenedObject.transform.SetParent(spawner);
        
        Transform childTransform = spawenedObject.transform.Find("CatAudio");
        if (childTransform != null)
        {
            childTransform.gameObject.name = "CatAudio " + spawnedCount;
        }
        spawnedCount++;

    }
    
    // ReSharper disable Unity.PerformanceAnalysis
    IEnumerator WaitToSpawn()
    {
        for (int i = spawnAmount; i > 0; i--)
        {
            Spawn();
            audioManager.UpdateSources();
            float delay = Mathf.Max(delayTime, 0.01f); 
            yield return new WaitForSeconds(delay);
        }
    }
}
