using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovingPlatform : MonoBehaviour
{

    [SerializeField] float speed;
    [SerializeField] float waitDuration;
    Vector3 TargetPos;

    MovementController movementController;
    Rigidbody2D rb;
    Vector3 movDir;

    Rigidbody2D playerRb;

    public GameObject ways;
    public Transform[] waysPoints;

    int pointIndex;

    private void Awake()
    {
        movementController = GameObject.FindGameObjectWithTag("Player").GetComponent<MovementController>();
        rb = GetComponent<Rigidbody2D>();
        playerRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();

        waysPoints = new Transform[ways.transform.childCount];
        for (int i = 0; i < ways.transform.childCount; i++)
        {
            waysPoints[i] = ways.transform.GetChild(i).gameObject.transform;
        }
    }
    private void Start()
    {
        pointIndex = 1;
        TargetPos = waysPoints[pointIndex].transform.position;
        DirCalculate();
    }
    void Update()
    {
        if (Vector2.Distance(transform.position, TargetPos) < 0.05f)
        {
            NextPoint();
        }

   
    }

    private void NextPoint()
    {
        transform.position = TargetPos;
        movDir = Vector3.zero;
        pointIndex++;
        pointIndex %= waysPoints.Length;
        TargetPos = waysPoints[pointIndex ].transform.position;
        StartCoroutine(WaitNextPoint());
    }

    IEnumerator WaitNextPoint()
    {
        yield return new WaitForSeconds(waitDuration);
        DirCalculate();
    }
    private void FixedUpdate()
    {
        rb.velocity = movDir * speed;
    }

    void DirCalculate()
    {
        movDir = (TargetPos - transform.position).normalized;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            movementController.isOnPlatform = true;
            movementController.platformRb = rb;
            playerRb.gravityScale *= 50;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            movementController.isOnPlatform = false;
            playerRb.gravityScale /= 50;
        }
    }
}
