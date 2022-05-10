
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseScene : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {   
        if (other.tag == "Player")
        {
            Debug.Log("LoseScene: OnTriggerEnter: "  + other.name);
            SceneManager.LoadScene(4); //lose scene
        }
        
    }
}
