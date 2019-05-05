using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslateBullet : Bullet
{
    private Vector3 lastPosition;
    private Vector3 thisPosition;
    private Vector3 heading;
    private float distance;
    private bool firstShot;
    private GameObject victimGO;
    private Subject victim;
    public int Dmg = 10;

    public float Speed = 1;
    public LayerMask Mask;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveByTranslate();
    }

    void MoveByTranslate()
    {
        lastPosition = gameObject.transform.position;
        transform.Translate(Vector3.forward * Speed);
        thisPosition = gameObject.transform.position;
        heading = (thisPosition - lastPosition);

        if (firstShot)
        {
            distance = heading.magnitude; // only need distance once, assuming projectile speed is constant.
            firstShot = false;
        }

    }

    private void OnCollisionEnter(Collision other)
    {
        victimGO = other.gameObject;
        victim = victimGO.GetComponent<Subject>();
        if (victim != null)
        {
            victim.GetDmg(Dmg);
        }
        Destroy();
    }

    public override void OnObjectReuse()
    {
        base.OnObjectReuse();
    }
}
