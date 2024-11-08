using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    
    [SerializeField] AudioSource[] Audio;

    private void Awake()
    {
        if (instance == null) { instance = this; }
        else if (instance != this) { Destroy(this); }
    }

    // Play the sound of the Player Walking
    public void Croissant() { Audio[0].pitch = Random.Range(0.8f, 1.2f); Audio[0].Play(); }
    public void Blueberry() { Audio[1].pitch = Random.Range(0.8f, 1.2f); Audio[1].Play();}
    public void Hit() {Audio[2].pitch = Random.Range(1f, 1.6f); Audio[2].Play(); }
}
