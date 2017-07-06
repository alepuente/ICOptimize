using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapObstacles : MonoBehaviour {

    public float collisionDamage;
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player" && other.gameObject.layer == 8)
        {
                PlayerController.instance.gameObject.GetComponent<Health>().health = GameManager.instance.calculateDamage("Limit", PlayerController.instance.gameObject.GetComponent<Health>().health);
        }
    }
}
