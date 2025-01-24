using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void buttonClicked()
    {
        Debug.Log("Clicked!");
        SceneManager.LoadScene(1);
    }

    public void button2Clicked()
    {
        Debug.Log("Clicked23222!");
    }


}
