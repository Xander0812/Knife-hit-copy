using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{

    public void StartGame()
    {
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }
    public void ChangeSkin()
    {
        SceneManager.LoadScene("Skin Change", LoadSceneMode.Single);
    }
}
