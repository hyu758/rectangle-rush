using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesController : MonoBehaviour
{
    [Header("Movement Particle")]
    [SerializeField] ParticleSystem movementParticle;
    [SerializeField] ParticleSystem fallParticle;
    [SerializeField] ParticleSystem touchParticle;
    [SerializeField] ParticleSystem dieParticle;

    [Range(0, 10)]
    [SerializeField] int occurAfterVelocity;

    [Range(0, 0.2f)]
    [SerializeField] float dustFormationPeriod;

    [SerializeField] Rigidbody2D playerRb;

    bool isGrounded;
    float counter;

    private void Start()
    {
        touchParticle.transform.parent = null;
    }
    private void Update()
    {
        counter += Time.deltaTime;
        if (isGrounded && Mathf.Abs(playerRb.velocity.x) > occurAfterVelocity)
        {
            if (counter > dustFormationPeriod)
            {
                movementParticle.Play();
                counter = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            fallParticle.Play();
            isGrounded = true;
        }
    }

    public void playParticle(Vector2 pos)
    {
        touchParticle.transform.position = pos;
        touchParticle.Play();
    }

    public void playDieParticle(Vector2 pos)
    {
        dieParticle.transform.position = pos;
        dieParticle.Play();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground")){
            isGrounded= false;
        }
    }
}
