using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    public new string name;
    public Gun Gun;

    public virtual void OnObjectReuse()
    {
        
    }

    protected void Destroy()
    {
        gameObject.SetActive(false);
    }
}
