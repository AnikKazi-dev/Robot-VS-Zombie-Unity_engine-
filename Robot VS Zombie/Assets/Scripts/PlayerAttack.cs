using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // Start is called before the first frame update

    public Animator attack;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey("d"))
        {
            Attack();
        }
        
    }

    void Attack()
    {
        
        attack.SetTrigger("Attack");

    }
}
