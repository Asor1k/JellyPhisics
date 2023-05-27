using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace JellyPhisics
{
    public class Jelly : MonoBehaviour
    {
        [SerializeField]
        private Transform centerBone;
     
     
        private List<JellyPoint> points;


        private void Start()
        {
            points = GetComponentsInChildren<JellyPoint>().ToList();       
        }

        private void SetCenterBonePosition()
        {
            Vector3 center = new Vector3();
            for (int i = 0; i < points.Count; i++)
            {
                center += points[i].transform.position;
            }
            center /= points.Count;
            centerBone.position = center;
        }

        private void FixedUpdate()
        {
            SetCenterBonePosition();
        }

    }
}