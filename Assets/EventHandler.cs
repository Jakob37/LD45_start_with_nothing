using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventHandler : MonoBehaviour
{
    public float timeUntilFirstMessage;
    public float timeUntilThiefSpawnStart;

    private float gameTime;
    
    private DisplayText textController;
    
    void Start()
    {
        gameTime = 0;
        story_controller = FindObjectOfType<DisplayText>();
    }

    void Update()
    {
        gameTime += Time.deltaTime;

        if (timeUntilFirstMessage <= gameTime) {
            print("Display Message");
            textController.ShowText("Hej din apa");
        }
    }
}
