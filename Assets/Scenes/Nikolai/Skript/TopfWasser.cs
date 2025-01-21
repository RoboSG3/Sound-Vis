using System;
using UnityEngine;

public class TopfWasser : MonoBehaviour
{
    private bool _meshActive = false;
    private MeshRenderer _mesh;
    public float offset = 0.1f;
    public float maxHeight = 1.0f;
    private void Start()
    {
        _mesh = GetComponent<MeshRenderer>();
        _mesh.enabled = false;
        
    }

    private void ActivateMesh()
    {
        
        if (!_meshActive)
        {
            _mesh = GetComponent<MeshRenderer>();
            if (_mesh != null)
            {
                _mesh.enabled = true; // Enable the MeshRenderer to make the mesh visible
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
            transform.position += new Vector3(0, offset, 0); // Move the object up by 0.1 units
        }
        else
        {
            Debug.Log("Maximum height reached."); // Log a message when the max height is reached
        }

    }
}
