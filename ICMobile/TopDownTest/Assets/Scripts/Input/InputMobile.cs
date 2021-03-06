﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMobile : IInput{

    Ray ray;
    RaycastHit hit;

    public Vector3 getSelection()
    {
        if (Input.touchCount > 0)
        {
            ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
            if (Physics.Raycast(ray, out hit))
            {
                switch (hit.collider.tag)
                {
                    case "Water": return hit.point;
                    case "Limit": return hit.point;
                    case "Rudder": PlayerController.instance.sailorEvent.Invoke(1); break;
                    case "Nest": PlayerController.instance.sailorEvent.Invoke(2); break;
                    case "LeftCannons": PlayerController.instance.sailorEvent.Invoke(3); break;
                    case "RightCannons": PlayerController.instance.sailorEvent.Invoke(4); break;
                    case "FrontCannon": PlayerController.instance.sailorEvent.Invoke(5); break;
                }
            }
        }
        return Vector3.zero;
    }
}
