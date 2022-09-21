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
    [SerializeField]
    int returnsAfterSeconds = 5;
    bool finishBouncing = false;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    public void StartBouncingRandomly()
    {
        playerTransform.GetComponent<BoxCollider>().enabled = false;
        rb.isKinematic = false;
        float randomX = Random.Range(-3, 3);
        float randomY = Random.Range(3, 6);
        float randomZ = Random.Range(-3, 3);
        rb.AddForce(new Vector3(randomX, randomY, randomZ), ForceMode.Impulse);
        StartCoroutine(RecallToHome());
    }

    IEnumerator RecallToHome() {
        yield return new WaitForSeconds(returnsAfterSeconds);
        finishBouncing = true;
        playerTransform.GetComponent<BoxCollider>().enabled = true;
    }

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = initialVelocity;
    }

    private void Update()
    {
        if(finishBouncing)
            lastFrameVelocity = rb.velocity;
    }


    //Vector3 lastCollisionNormal;
    private void OnCollisionEnter(Collision collision)
    {
        if(finishBouncing)
            Bounce(collision.contacts[0].normal);

        //lastCollisionNormal = collision.contacts[0].normal;

        if (collision.gameObject.name != "mesh")
            return;

        OnRecallSuccessfull();

    }


    void OnRecallSuccessfull() {
        rb.isKinematic = true;
        transform.localPosition = new Vector3(0, 0.4f, 0);
        finishBouncing = false;
        Data.DataEvents.OnProductPlaySpecialEnd.Invoke();
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
