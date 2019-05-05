using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WeaponObject : MonoBehaviour
{
    
    public int animationType;

    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
    }

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
