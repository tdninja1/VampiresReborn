using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseSceneSnd : MonoBehaviour
{
    //SOUND
    public AudioClip loseSound;
    
    // Start is called before the first frame update
    void Start()
    {
        LoseSnd();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoseSnd()
    {
        AudioSource ac = GetComponent<AudioSource>();

        if (ac.isPlaying)
            {
                return;
            } else {
                ac.PlayOneShot(loseSound);
            }
    }

    
}
