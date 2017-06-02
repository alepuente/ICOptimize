using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLimits : MonoBehaviour {
    public float timer;
     public float damageSpeed;
  
    private void OnTriggerStay(Collider other)
    {
        timer += Time.deltaTime;
        if (other.tag == "Player" && other.gameObject.layer == 8)
        {
            if (timer > damageSpeed)
            {
                //Change this cancer
                PlayerController.instance.gameObject.GetComponent<Health>().health = GameManager.instance.calculateDamage("Limit", PlayerController.instance.gameObject.GetComponent<Health>().health);
                timer = 0;
            }
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            timer = 0;
        }
    }
}
