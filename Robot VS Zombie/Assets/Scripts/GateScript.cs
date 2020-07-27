using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GateScript : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Robot"))
        {
            Invoke("ForInvoke",1);
            
           
        }
    }
    void ForInvoke()
    {
        SceneManager.LoadScene("Boss");
    }
}
