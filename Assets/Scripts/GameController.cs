using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    Vector2 checkPoint;
    Rigidbody2D rb;
    public ParticlesController dieParticle;
    public bool isDead = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        checkPoint = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            isDead = true;
            Die();
        }
    }

    public void updateCheckPoint(Vector2 cp)
    {
        checkPoint = cp;
    }
    void Die()
    {
        dieParticle.playDieParticle(transform.position);
        StartCoroutine(Respawn(0.5f));
    }

    IEnumerator Respawn(float duration)
    {
        rb.simulated = false;
        rb.velocity = new Vector2(0, 0);
        transform.localScale = new Vector3(0, 0, 0);
        yield return new WaitForSeconds(duration);
        isDead = false;
        transform.position = checkPoint;
        transform.localScale = new Vector3(1, 1, 1);
        rb.simulated = true;
    }
}
