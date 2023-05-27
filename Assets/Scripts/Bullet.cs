using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JellyPhisics
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField]
        private float speed;
        [SerializeField]
        private Rigidbody2D rb;


        private IEnumerator Dying()
        {
            yield return new WaitForEndOfFrame();
            Destroy(gameObject);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.collider.TryGetComponent(out JellyPoint point))
            {
                StartCoroutine(Dying());
            }
        }

        public void Init()
        {
            rb.AddForce(Vector2.up * speed);
        }

    }
}