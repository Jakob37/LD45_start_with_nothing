using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{

    public AudioSource birds;
    public AudioSource tractor;
    public AudioSource shout;
    public AudioSource dig;
    public AudioSource crash;

    void Start() {
        birds.Play();
        birds.loop = true;
    }

    void Update() {
        
        if (TractorExists() && !tractor.isPlaying) {
            tractor.Play();
            print("Tractor is playing");
        }
    }

    public void MakeShout() {
        shout.Play();
    }

    public void MakeDigSound() {
        dig.Play();
    }

    public void MakeCrashSound() {
        crash.Play();
    }

    private bool TractorExists() {
        return FindObjectOfType<BigTomatoLady>() != null;
    }
}
