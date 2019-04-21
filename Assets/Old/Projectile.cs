using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private GameObject _go;         // shortcut to gameObject
    private Vector3 _startPoint;    // start point for trail or wherever the projectile was born
    private Vector3 _endPoint;      // end point of trail or wherever the final hit took place
    private Vector3 _endNormal;    // victim that was hit (not good for Pierced type)
    private GameObject _victimGo; 
    public LineRenderer LineRenderer;

    public float shootDistance = 20f;
    // Start is called before the first frame update
    void Awake()
    {
        _go = gameObject;
        Fire();
    }

    // Update is called once per frame
    void Fire()
    {
        Ray ray = new Ray(_go.transform.position,_go.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, shootDistance))
        {
            shootDistance = hit.distance;
        }
        _startPoint = _go.transform.position;
        _endPoint = hit.point;
        LineRenderer.enabled = true;
        LineRenderer.SetPosition(0, _startPoint);
        LineRenderer.SetPosition(1, _endPoint);
        //Destroy(this);
    }
}
