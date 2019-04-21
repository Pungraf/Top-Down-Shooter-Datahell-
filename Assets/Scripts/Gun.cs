using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Gun : Weapon
{
    public GameObject Barrel;
    private Transform _spawn;
    private bool WeaponIsOnCooldown;
    public float TimeToCooldown = 0.5f;
    public float maxshotDistance = 10f;
    private LineRenderer trace;
    public PlayerMovement Owner;
    public AudioSource audio;
    private GameObject victimGO;
    private Subject victim;
    public int Dmg = 10;
    public GameObject Bullet;

    private void Start()
    {
        Barrel = transform.Find("Barrel").gameObject;
        Owner = GetComponentInParent<PlayerMovement>();
        _spawn = Barrel.transform;
        trace = GetComponent<LineRenderer>();
        trace.enabled = false;
        audio = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        // Start the correct control loop for this Weapon Type.
        StartCoroutine(WeaponLoop());
    }

    private IEnumerator  WeaponLoop()
    {
        while (true)
        {
            if (Input.GetKey(KeyCode.Mouse0)) yield return StartCoroutine(FireRanged());
            yield return null;
        }
    }

    private IEnumerator FireRanged()
    {
        switch (weaponType)
        {
            case Gun.WeaponType.Raycast:
                FireRay();
                break;  
            case Gun.WeaponType.Regular:
                Fire();
                break; 
        } 
        yield return StartCoroutine(FireCooldown());
        yield break;
    }

    private void Fire()
        {
            
            GameObject newBullet = Instantiate(Bullet, _spawn.position, Quaternion.identity);
            newBullet.transform.rotation = _spawn.rotation;
            audio.Play();
        }

    private IEnumerator FireCooldown()
    {
        WeaponIsOnCooldown = true;
        yield return new WaitForSeconds(TimeToCooldown);    
        WeaponIsOnCooldown = false;
    }
    
    private IEnumerator RenderTrace(Vector3 hitPoint)
    {
        trace.enabled = true;
        trace.SetPosition(0, _spawn.position);
        trace.SetPosition(1, _spawn.position + hitPoint);
        yield return null;
        trace.enabled = false;
    }

    private void FireRay()
    {
        Ray ray = new Ray(_spawn.position,_spawn.forward);
        RaycastHit hit;

        float shotDistance = maxshotDistance;
        
        if (Physics.Raycast(ray, out hit, shotDistance))
        {
            victimGO = hit.collider.gameObject;
            victim = victimGO.GetComponent<Subject>();
            if (victim != null)
            {
                victim.GetDmg(Dmg);
            }
            shotDistance = hit.distance;
        }

        if (trace)
        {
            StartCoroutine("RenderTrace", ray.direction * shotDistance);
        }
        audio.Play();
        
    }
}
