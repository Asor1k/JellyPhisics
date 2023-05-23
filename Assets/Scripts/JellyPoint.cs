using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JellyPhisics
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class JellyPoint : MonoBehaviour
    {
        [ContextMenu("Face center")]
        public void FaceCenter(Vector3 center)
        {
            //transform.LookAt(center);
        }

    }
}