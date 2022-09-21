using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingBall : MonoBehaviour
{
    public Vector3 startForce;


    [SerializeField]
    [Range(0f, 1f)]
    [Tooltip("0 = regular bounce ignoring player | 1 = direct to the player")]
    private float bias = 0.5f;

    [SerializeField]
    [Tooltip("Just for debugging, adds some velocity during OnEnable")]
    private Vector3 initialVelocity;

    [SerializeField]
    private Transform playerTransform;

    [SerializeField]
    private float bounceVelocity = 10f;

    private Vector3 lastFrameVelocity;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(startForce, ForceMode.Impulse);
    }

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = initialVelocity;
    }

    private void Update()
    {
        lastFrameVelocity = rb.velocity;

        if (Input.GetKeyUp(KeyCode.Space))
        {
            finishBouncing = true;
        }
    }


    bool finishBouncing = false;

    //Vector3 lastCollisionNormal;
    private void OnCollisionEnter(Collision collision)
    {
        if(finishBouncing)
            Bounce(collision.contacts[0].normal);

        //lastCollisionNormal = collision.contacts[0].normal;

        if (collision.gameObject.name != "BallHome")
            return;

        rb.isKinematic = true;
        transform.position = playerTransform.position;
    }


    private void Bounce(Vector3 collisionNormal)
    {
        var speed = lastFrameVelocity.magnitude;
        var bounceDirection = Vector3.Reflect(lastFrameVelocity.normalized, collisionNormal);
        var directionToPlayer = playerTransform.position - transform.position;

        var direction = Vector3.Lerp(bounceDirection, directionToPlayer, bias);

        Debug.Log("Out Direction: " + direction);
        rb.velocity = direction * bounceVelocity;
    }

}
