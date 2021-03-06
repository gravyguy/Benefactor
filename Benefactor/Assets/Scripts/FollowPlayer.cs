﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    public float moveSpeed;
    protected GameObject toFollow;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (toFollow != null)
        {
            Vector3 target = toFollow.transform.position;
            target.z = -10;
            float distance = Math.Abs(transform.position.x - target.x) + Math.Abs(transform.position.y - target.y);
            if (distance < 0.03)
            {
                transform.position = new Vector3(target.x, target.y, -10);
            }
            else
            {
                moveSpeed = Math.Max(distance * 2, 0.03f);
                transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
            }
        }
    }

    public void Target(GameObject newTarget)
    {
        toFollow = newTarget;
    }

}
