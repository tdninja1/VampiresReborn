using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioClip menuClickSnd;
    
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GoToControlsMenu()
    {
        SceneManager.LoadScene("ControlsMenu");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ClickSound()
    {
        AudioSource ac = GetComponent<AudioSource>();

        if (ac.isPlaying)
            {
                return;
            } else {
                ac.PlayOneShot(menuClickSnd);
            }
    }
}
