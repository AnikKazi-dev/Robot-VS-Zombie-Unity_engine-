using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG : MonoBehaviour
{
    public Transform robot;
    Transform bg;

    private void Start()
    {
        bg = GetComponent<Transform>();
    }
    // Update is called once per frame
    void Update()
    {
        transform.position =new Vector2  (robot.transform.position.x, robot.transform.position.y);
       // bg.position.y
    }
}
