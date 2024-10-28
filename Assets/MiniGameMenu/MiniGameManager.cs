using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGameManager : MonoBehaviour
{
    public void MG1() { SceneManager.LoadScene("PlayTarot"); }
    public void MG2() { SceneManager.LoadScene("MiniGame2"); }
    public void MG3() { SceneManager.LoadScene("MiniGame3"); }
    public void MG4() { SceneManager.LoadScene("MiniGame4"); }
    public void MG5() { SceneManager.LoadScene("MiniGame5"); }
    public void MG6() { SceneManager.LoadScene("Game6"); }
}
