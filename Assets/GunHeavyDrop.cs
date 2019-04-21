using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunHeavyDrop : MonoBehaviour
{
    public Gun GunPrefab;
    public bool PlayerInRange;
    private PlayerMovement player;


    // Update is called once per frame
    void Update()
    {
        if (PlayerInRange)
        {
            if (Input.GetKey(KeyCode.F))
            {
                player.gunList.Add(GunPrefab);
                Destroy(this.gameObject);
            }
            
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInRange = true;
            player = other.GetComponent<PlayerMovement>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInRange = false;
            player = null;
        }
    }

}
