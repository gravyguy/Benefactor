﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : InteractableObject
{
    public bool locked;
    public bool takesKey;
    public float speed = 90; // door speed in degrees per second
    public float openAngle = 90; // opening angle in degrees

    protected bool open;
    protected InteractableObject trigger;

    private Vector2 initialPos;
    private Vector3 curAngle;
    private float startAngle;
    private float targetAngle;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        curAngle = transform.eulerAngles;
        startAngle = curAngle.z; // save the startAngle
        targetAngle = startAngle;
        initialPos = transform.position;
    }

    void Update()
    { // rotate gradually door to targetAngle, if different of curAngle:
        curAngle.z = Mathf.MoveTowards(curAngle.z, targetAngle, speed * Time.deltaTime);
        transform.eulerAngles = curAngle;
        float xDisplacement = (float)Math.Cos(curAngle.z * Math.PI / 180)/2;
        float yDisplacement = (float)Math.Sin(curAngle.z * Math.PI / 180)/2;
        Vector2 newPosition = new Vector2(initialPos.x + xDisplacement - 0.5f, initialPos.y + yDisplacement);
        rb2D.MovePosition(newPosition);
    }

    void Open(Vector3 playerPos) //thanks to https://answers.unity.com/questions/239912/rotate-door-object-away-from-player.html
    {
        if (open == false)
        { // only open a closed door!
            Vector3 dirDoor = transform.position - playerPos;
            float dot = Vector3.Dot(dirDoor, transform.right);
            if (dot > 0){ // if door opens to the wrong side, use dot < 0
                targetAngle = startAngle + openAngle;
            }
            else {
                targetAngle = startAngle - openAngle;
            }
            open = true;
            boxCollider.enabled = false;
        }
    }
    
    void Close()
    {
        targetAngle = startAngle;
        open = false;
        boxCollider.enabled = true;
    }

    public void Toggle(Vector3 playerPos)
    {
        if (open)
            Close();
        else
            Open(playerPos);
    }

    public override SortedSet<String> GetActions()
    {
        receiveActions = base.GetActions();
        if (!locked)
            receiveActions.Add("Door");

        return receiveActions;
    }
}
