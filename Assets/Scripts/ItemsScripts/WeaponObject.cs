using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponObject : MonoBehaviour
{
    
    public int animationType;
    
    void OnEnable()
    {
        StartCoroutine(WeaponLoop());
    }
    
    private IEnumerator  WeaponLoop()
    {
        while (true)
        {
            if (Input.GetKey(KeyCode.Mouse0)) yield return StartCoroutine(Action());
            yield return null;
        }
    }

    public virtual IEnumerator Action()
    {
        yield break;
    }
}
