using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections; 

public class Win : MonoBehaviour
{
    public string winScreen = "Scenes/winScreen"; 
    public string loseScreen = "Scenes/loseScreen";
    public float winDelay = 20f;

    private bool hasWon = false;


    private void OnTriggerEnter(Collider other)
    {
        if (!hasWon)
        {
            hasWon = true; 
            SceneManager.LoadScene(winScreen);
        }
    }

    private void Start()
    {
        StartCoroutine(WinCheckCoroutine());
    }

    private IEnumerator WinCheckCoroutine()
    {
        yield return new WaitForSeconds(winDelay); 

        if (!hasWon) 
        {
            SceneManager.LoadScene(loseScreen); 
        }
    }
}
