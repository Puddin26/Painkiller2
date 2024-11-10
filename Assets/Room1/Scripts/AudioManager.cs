using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    
    [SerializeField] AudioSource[] Audio;

    public bool isStopped;

    private void Awake()
    {
        if (instance == null) { instance = this; }
        else if (instance != this) { Destroy(this); }
    }

    // Play the sound of the Player Walking

    public void StopAllMusic()
    {
        foreach (var audio in Audio) { audio.Stop();}
    }

    public void FlipCard() { Audio[0].pitch = Random.Range(0.8f, 1.2f); Audio[0].Play(); }
    public void TarotDialogue() { Audio[1].pitch = Random.Range(0.8f, 1.2f); Audio[1].Play();}
    public void TarotReadingTheme() {Audio[2].pitch = Random.Range(1f, 1.6f); Audio[2].Play(); }

    public void TakingNotesTheme() { if (!Audio[3].isPlaying) { Audio[3].pitch = Random.Range(1f, 1.6f); Audio[3].Play(); } }
    public void ScribbleNotes() { if (!Audio[4].isPlaying) { Audio[4].pitch = Random.Range(1f, 1.6f); Audio[4].Play(); } }
    public void HallwayTheme() { if (!Audio[5].isPlaying) { Audio[5].pitch = Random.Range(1f, 1.6f); Audio[5].Play(); } }
    public void Walking() { if (!Audio[6].isPlaying) { Audio[6].pitch = Random.Range(1f, 1.6f); Audio[6].Play(); } }
    public void LookBack() { if (!Audio[7].isPlaying) { Audio[7].pitch = Random.Range(1f, 1.6f); Audio[7].Play(); } }
}
