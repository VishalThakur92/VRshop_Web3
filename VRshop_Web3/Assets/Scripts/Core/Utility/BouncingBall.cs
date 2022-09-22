using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace VRshop_Web3
{
    public class BouncingBall : MonoBehaviour
    {
        #region Parameters
        [SerializeField]
        [Range(0f, 1f)]
        [Tooltip("0 = regular bounce ignoring player | 1 = direct to the player")]
        private float bias = 0.5f;

        [SerializeField]
        [Tooltip("Just for debugging, adds some velocity during OnEnable")]
        private Vector3 initialVelocity;


        //Table for this instance, Ball will return to this object after playing around
        [SerializeField]
        private Transform origin;

        [SerializeField]
        private float bounceVelocity = 10f;

        private Vector3 lastFrameVelocity;
        private Rigidbody rb;

        //ball will return after this many seconds
        [SerializeField]
        int returnsAfterSeconds = 5;

        //If TRUE - Signals ball to return to origin 
        bool finishBouncing = false;
        #endregion


        #region Core
        void Start()
        {
            //grab rigidbody ref
            rb = GetComponent<Rigidbody>();
        }


        public void StartBouncingRandomly()
        {
            //disable 
            origin.GetComponent<BoxCollider>().enabled = false;
            rb.isKinematic = false;
            float randomX = Random.Range(-3, 3);
            float randomY = Random.Range(3, 6);
            float randomZ = Random.Range(-3, 3);
            rb.AddForce(new Vector3(randomX, randomY, randomZ), ForceMode.Impulse);
            StartCoroutine(RecallToHome());
        }

        IEnumerator RecallToHome()
        {
            yield return new WaitForSeconds(returnsAfterSeconds);
            finishBouncing = true;
            origin.GetComponent<BoxCollider>().enabled = true;
        }

        private void OnEnable()
        {
            rb = GetComponent<Rigidbody>();
            rb.velocity = initialVelocity;
        }

        private void Update()
        {
            if (finishBouncing)
                lastFrameVelocity = rb.velocity;
        }


        //Vector3 lastCollisionNormal;
        private void OnCollisionEnter(Collision collision)
        {
            if (finishBouncing)
                Bounce(collision.contacts[0].normal);

            //lastCollisionNormal = collision.contacts[0].normal;

            if (collision.gameObject.name != "mesh")
                return;

            OnRecallSuccessfull();

        }


        void OnRecallSuccessfull()
        {
            rb.isKinematic = true;
            transform.localPosition = new Vector3(0, 0.4f, 0);
            finishBouncing = false;
            Data.Events.OnProductPlaySpecialEnd.Invoke();
        }

        private void Bounce(Vector3 collisionNormal)
        {
            var speed = lastFrameVelocity.magnitude;
            var bounceDirection = Vector3.Reflect(lastFrameVelocity.normalized, collisionNormal);
            var directionToPlayer = origin.position - transform.position;

            var direction = Vector3.Lerp(bounceDirection, directionToPlayer, bias);

            Debug.Log("Out Direction: " + direction);
            rb.velocity = direction * bounceVelocity;
        }
        #endregion
    }
}
