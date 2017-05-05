﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannons : MonoBehaviour
{
    public bool isLeft = false;
    public bool isRight = false;
    public bool isFront = false;
    public bool isEnemy = false;

    private int sailors;

    private float shootTimer;
    private float shootFireRate;

    // Update is called once per frame
    void Update()
    {
        if (isLeft)
        {
            sailors = PlayerController.instance.cannonLeft;
        }
        else if (isRight)
        {
            sailors = PlayerController.instance.cannonRight;
        }
        else if (isFront)
        {
            sailors = PlayerController.instance.cannonFront;
        }
        else if (isEnemy)
        {
            sailors = 1;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8 && sailors > 0 && shootTimer > GameManager.instance.FireRateDicc[gameObject.tag])
        {
            other.GetComponent<Health>().health = GameManager.instance.calculateDamage(gameObject.tag, other.GetComponent<Health>().health);
            shootTimer = 0;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 8 && sailors > 0 && shootTimer > GameManager.instance.FireRateDicc[gameObject.tag])
        {
            other.GetComponent<Health>().health = GameManager.instance.calculateDamage(gameObject.tag, other.GetComponent<Health>().health);
            shootTimer = 0;
        }
    }
}