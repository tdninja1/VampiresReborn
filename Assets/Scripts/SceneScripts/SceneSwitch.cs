using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    //SOUND
    public AudioClip winSound;
    
    void OnTriggerEnter(Collider other)
    {   //load the next level, then load the win scene if no playable scenes are available
        if (other.tag == "Player")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        

        //if win scene is loaded, play sound
        // if (SceneManager.LoadScene(2)) {
            // AudioSource ac = GetComponent<AudioSource>();
            // ac.PlayOneShot(winSound);

        // }
    }
}
