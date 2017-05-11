using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannons : MonoBehaviour
{
    public bool isLeft = false;
    public bool isRight = false;
    public bool isFront = false;
    public bool isEnemy = false;
    public int sailors;
    public float shootTimer;
    public Renderer rangeColor;

    void Start()
    {
        rangeColor = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        shootTimer += Time.deltaTime;
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

        if (sailors > 0 && shootTimer > GameManager.instance.FireRateDic[gameObject.tag])
        {
            rangeColor.material.color = Color.green;
        }
        else
        {
            rangeColor.material.color = Color.red;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8  && sailors > 0 )
        {
            if (shootTimer > GameManager.instance.FireRateDic[gameObject.tag])
            {                
            other.GetComponent<Health>().health = GameManager.instance.calculateDamage(gameObject.tag, other.GetComponent<Health>().health);
            shootTimer = 0;
            }
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 8  && sailors > 0 && shootTimer > GameManager.instance.FireRateDic[gameObject.tag])
        {
            other.GetComponent<Health>().health = GameManager.instance.calculateDamage(gameObject.tag, other.GetComponent<Health>().health);
            shootTimer = 0;
        }
    }
}
