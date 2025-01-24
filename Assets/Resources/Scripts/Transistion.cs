using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transistion : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(WarningCoroutine());
    }

    private IEnumerator WarningCoroutine()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(2);
    }
}
