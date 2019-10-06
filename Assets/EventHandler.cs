using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventHandler : MonoBehaviour
{
    public float timeUntilFirstMessage;
    public bool firstMessageShown = false;
    public float timeUntilThiefSpawnStart;

    private float gameTime;
    private DisplayText textController;

    
    void Start()
    {
        gameTime = 0;
        textController = FindObjectOfType<DisplayText>();
    }

    void Update()
    {
        gameTime += Time.deltaTime;

        if (!firstMessageShown && timeUntilFirstMessage <= gameTime) {
            print("Display Message");
            firstMessageShown = true;
            textController.ShowText("Hej din apa", 3f);
        }
    }
}
