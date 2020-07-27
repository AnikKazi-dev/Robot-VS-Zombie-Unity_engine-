using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMeneger : MonoBehaviour
{
    // Start is called before the first frame update
   public void OnPlayButtonClick()
    {
        SceneManager.LoadScene("MainGame");

    }

    public void OnExitButtonClick()
    {
       // SceneManager.LoadScene("");

    }
}
