using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private PongEnvController envController;
    [SerializeField] private GameObject leftGoal;
    [SerializeField] private GameObject rightGoal;
    
    private Rigidbody2D rb;

    private void Awake() 
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Launch();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("RightGoal"))
        {
            envController.ResolveEvent(Event.LeftPaddleGoal);
        }
        if (other.gameObject.CompareTag("LeftGoal"))
        {
            envController.ResolveEvent(Event.RightPaddleGoal);
        }
    }

    public void Launch()
    {
        transform.localPosition = Vector3.zero;
        float x = Random.Range(0, 2) == 0 ? -1 : 1;
        float y = Random.Range(0, 2) == 0 ? -1 : 1;
        rb.velocity = new Vector2(speed * x, speed * y);
    }
}
