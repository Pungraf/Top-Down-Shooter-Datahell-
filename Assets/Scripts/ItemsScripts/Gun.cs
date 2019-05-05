using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Gun : WeaponObject
{
    public GameObject Barrel;
    private Transform _spawn;
    public float TimeToCooldown = 0.5f;
    public new AudioSource audio;
    public GameObject Bullet;

    private void Start()
    {
        Barrel = transform.Find("Barrel").gameObject;
        _spawn = Barrel.transform;
        audio = GetComponent<AudioSource>();
    }


    public override IEnumerator Action()
    {
        Fire();
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
        yield return new WaitForSeconds(TimeToCooldown);    
    }
    
    
}
