using System.Collections;
using UnityEngine;

public class TopfWasser : MonoBehaviour
{
    private bool _meshActive = false;
    private MeshRenderer _mesh;
    public bool drain = true; // Enables or disables the auto-drain feature
    public float drainAfter = 1.0f; // Time in seconds to wait before moving down
    public float offset = 0.1f; // Movement step
    public float maxHeight = 1.0f; // Maximum height the object can reach
    private float startHeight;
    private int steps = 0;
    private void Start()
    {
        _mesh = GetComponent<MeshRenderer>();
        if (_mesh != null)
        {
            _mesh.enabled = false;
        }
        startHeight = transform.position.y;
    }

    private void ActivateMesh()
    {
        if (!_meshActive)
        {
            if (_mesh != null)
            {
                _mesh.enabled = true; 
                Debug.Log("MESH ACTIVATED");
            }
            _meshActive = true;
        }
    }

    public void MoveUp()
    {
        ActivateMesh();
        if (transform.position.y < maxHeight)
        {
            transform.position += new Vector3(0, offset, 0); 
            steps++;
        }
        else
        {
            Debug.Log("Maximum height reached.");
            if (drain)
            {
                StartCoroutine(StartDrain());
            }
        }
    }
    
    // ReSharper disable Unity.PerformanceAnalysis
    private IEnumerator StartDrain()
    {
        Debug.Log("Waiting to drain...");
        yield return new WaitForSeconds(drainAfter); 
        StartCoroutine(MoveDown()); 
    }
 
    // ReSharper disable Unity.PerformanceAnalysis
    private IEnumerator MoveDown()
    {
       for (int i = 0; i <= steps - 1; i++)
        {
            if (transform.position.y > startHeight ) transform.position -= new Vector3(0, offset, 0); // Move the object down by offset units

            yield return new WaitForSeconds(0.1f); // Wait for 0.1 seconds to show each step
        }

        steps = 0;

        Debug.Log("Minimum height reached.");
        if (_mesh != null)
        {
            _mesh.enabled = false;
        }
        _meshActive = false;
    }
}