using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{

    public GameObject gamePassedMenu;
    public GameObject gameFailedMenu;
    // Start is called before the first frame update
    public void OnPlayAgainButtonPressed()
    {
        SceneManager.LoadScene("MainGame");
    }
    public void OnPlayExitButtonPressed()
    {
        SceneManager.LoadScene("Menu");
    }
    public void BossDied()
    {
        gamePassedMenu.SetActive(true) ; 
    }
    public void PlayerDied()
    {
        gameFailedMenu.SetActive(true);
    }
}
