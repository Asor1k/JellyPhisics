using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JellyPhisics
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class JellyPoint : MonoBehaviour
    {
        private CircleCollider2D circleCollider;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, GetComponent<CircleCollider2D>().radius);
        }

        private void Awake()
        {
            circleCollider = GetComponent<CircleCollider2D>();
        }



        public CircleCollider2D GetCollider()
        {
            return circleCollider;
        }
    }
}