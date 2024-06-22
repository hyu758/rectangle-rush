using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rb;
    [SerializeField] int speed;
    [Range(0f, 10f)]
    [SerializeField] float acceleration;
    float speedMultipler;

    bool pressed;
    [SerializeField]
    bool isWallTouched;
    public LayerMask wallLayer;
    public Transform wallCheckPoint;
    int dir = 1;

    [SerializeField] ParticlesController touchParticle;
    public bool isOnPlatform;
    public Rigidbody2D platformRb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    private void FixedUpdate()
    {
        updateMultipler();
        float actualSpeed = speed * speedMultipler * dir;
        if (isOnPlatform)
        {
            rb.velocity = new Vector2(actualSpeed + platformRb.velocity.x, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(actualSpeed, rb.velocity.y);
        }
        isWallTouched = Physics2D.OverlapBox(wallCheckPoint.position, new Vector2(0.06f, 0.7f), 0, wallLayer);
        
        if (isWallTouched)
        {
            Debug.Log("Ngu");
            Flip();

        }
    }

    private void Flip()
    {
        touchParticle.playParticle(wallCheckPoint.position);
        transform.localScale *= new Vector2(-1, 1);
        dir *= -1;
    }

    public void Move(InputAction.CallbackContext val)
    {
        if (val.started)
        {
            pressed = true;
        }
        else if (val.canceled)
        {
            pressed = false;
        }
    }

    void updateMultipler()
    {
        if (pressed && speedMultipler <  1) {
            speedMultipler += Time.deltaTime*acceleration;
        }
        else if (!pressed && speedMultipler > 0) {
            speedMultipler = Math.Max(0, speedMultipler - Time.deltaTime*acceleration);
        }
    }
}
