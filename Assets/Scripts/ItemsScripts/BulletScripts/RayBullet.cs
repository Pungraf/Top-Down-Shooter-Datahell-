using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayBullet : Bullet
{
    private float distance = 10f;
    private GameObject victimGO;
    private Subject victim;
    public int Dmg = 10;
    public LineRenderer trace;
    private bool rayActive;
    public LayerMask FriendLayer;
    
    // Start is called before the first frame update
    void Start()
    {
        trace = GetComponent<LineRenderer>();
        trace.enabled = false;
        rayActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (rayActive)
        {
            FireRay();
        }
    }

    private void FireRay()
    {
        Ray ray = new Ray(transform.position,transform.forward);
        RaycastHit hit;

        float shotDistance = distance;
        
        if (Physics.Raycast(ray, out hit, shotDistance))
        {
            victimGO = hit.collider.gameObject;
            victim = victimGO.GetComponent<Subject>();
            if (victim != null && victimGO.layer != FriendLayer.value)
            {
                victim.GetDmg(Dmg);
            }
            shotDistance = hit.distance;
        }

        if (trace)
        {
            StartCoroutine("RenderTrace", ray.direction * shotDistance);
        }

        rayActive = false;
    }
    
    private IEnumerator RenderTrace(Vector3 hitPoint)
    {
        trace.enabled = true;
        trace.SetPosition(0, transform.position);
        trace.SetPosition(1, transform.position + hitPoint);
        yield return null;
        trace.enabled = false;
        Destroy(gameObject);
        
    }
}
