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
    public void Confirmation1() { Audio[8].pitch = Random.Range(1f, 1.6f); Audio[8].Play();  }
    public void Confirmation2() { if (!Audio[9].isPlaying) { Audio[9].pitch = Random.Range(1f, 1.6f); Audio[9].Play(); } }
    public void Plane() { if (!Audio[10].isPlaying) { Audio[10].pitch = Random.Range(1f, 1.6f); Audio[10].Play(); } }
    public void Laughing() { if (!Audio[11].isPlaying) { Audio[11].Play(); } }
    public void Sticker() { Audio[12].pitch = Random.Range(1f, 1.6f); Audio[12].Play(); }
    public void Wrap() { Audio[13].pitch = Random.Range(1f, 1.6f); Audio[13].Play(); }
    public void Tie() { Audio[14].pitch = Random.Range(1f, 1.6f); Audio[14].Play(); }
    public void Gift() { Audio[15].pitch = Random.Range(1f, 1.6f); Audio[15].Play(); }
    public void Vibrate() { Audio[16].pitch = Random.Range(1f, 1.6f); Audio[16].Play(); }
    public void EndCall() { Audio[17].pitch = Random.Range(1f, 1.6f); Audio[17].Play(); }
    public void Clock() { Audio[18].pitch = Random.Range(1f, 1.6f); Audio[18].Play(); }
    public void Poping() { Audio[19].pitch = Random.Range(1f, 1.6f); Audio[19].Play(); }
    public void MuffledTalk() { Audio[20].pitch = Random.Range(1f, 1.6f); Audio[20].Play(); }
    public void LinkCon() { Audio[21].pitch = Random.Range(1f, 1.6f); Audio[21].Play(); }
    public void Apply() { Audio[22].pitch = Random.Range(1f, 1.6f); Audio[22].Play(); }
    public void Scroll() { Audio[23].pitch = Random.Range(1f, 1.6f); Audio[23].Play(); }
    public void Pack() { Audio[24].pitch = Random.Range(1f, 1.6f); Audio[24].Play(); }
    public void Shuffle() { Audio[25].pitch = Random.Range(1f, 1.6f); Audio[25].Play(); }
}
