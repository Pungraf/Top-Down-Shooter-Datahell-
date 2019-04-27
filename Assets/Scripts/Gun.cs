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
    public PlayerMovement Owner;
    public AudioSource audio;
    public GameObject Bullet;

    private void Start()
    {
        Barrel = transform.Find("Barrel").gameObject;
        Owner = GetComponentInParent<PlayerMovement>();
        _spawn = Barrel.transform;
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
    
    private void FireRay()
    {
            
        GameObject newBullet = Instantiate(Bullet, _spawn.position, Quaternion.identity);
        //TODO  Add Recoil 
        newBullet.transform.rotation = _spawn.rotation;
        audio.Play();
    }

    private IEnumerator FireCooldown()
    {
        WeaponIsOnCooldown = true;
        yield return new WaitForSeconds(TimeToCooldown);    
        WeaponIsOnCooldown = false;
    }
    
    
}
