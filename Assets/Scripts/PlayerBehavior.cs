﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public event Action OnPlayerDeath;

    const float speed = 10f;

    Vector3 input;
    float screenHalfWidthInWorldlUnits;

    // Start is called before the first frame update
    void Start()
    {
        float playerWidth = transform.localScale.x / 2.0f;
        screenHalfWidthInWorldlUnits = Camera.main.aspect * Camera.main.orthographicSize + playerWidth;
    }

    void Update()
    {
        UpdatePosition();
    }

    void OnTriggerEnter2D(Collider2D triggerCollider)
    {
        if (triggerCollider.tag == "Obstacle")
        {
            if (OnPlayerDeath != null)
            {
                OnPlayerDeath();
            }
            Destroy(gameObject);
        }
    }

    void UpdatePosition()
    {
        input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0);
        transform.Translate(input.normalized * speed * Time.deltaTime);

        // Wrap around screen
        if (transform.position.x > screenHalfWidthInWorldlUnits)
        {
            transform.position = new Vector3(-screenHalfWidthInWorldlUnits, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < -screenHalfWidthInWorldlUnits)
        {
            transform.position = new Vector3(screenHalfWidthInWorldlUnits, transform.position.y, transform.position.z);
        }
    }
}
