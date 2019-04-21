using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Subject : MonoBehaviour
{
    public int health = 100;

    public void GetDmg(int value)
    {
        Debug.Log("got " + value + " DMG");
        health = health - value;
        CheckDeath();
    }

    public void Death()
    {
        Destroy(this.gameObject);
    }
    

    private void CheckDeath()
    {
        if (health <= 0)
        { 
            Death();
        }
    }
}
