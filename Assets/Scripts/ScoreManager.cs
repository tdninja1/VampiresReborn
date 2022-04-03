using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public Text scrollText;
    public GameObject collectCube;
    private InputManager inputManager;

    int score = 0;
    // Start is called before the first frame update

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        
        collectCube =  GetComponent<GameObject>();
    }

    public void UpdateScrollText()
    {
        score += 1;
        scrollText.text = score.ToString();
    }

}
