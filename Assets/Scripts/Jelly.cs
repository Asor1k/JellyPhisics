using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.U2D;

namespace JellyPhisics
{
    public class Jelly : MonoBehaviour
    {
        [SerializeField]
        private SpriteShapeController shapeRendererToModify;
        [SerializeField]
        private float splineOffset;
        [SerializeField]
        private Rigidbody2D myRb;
        [SerializeField]
        private int dencity;
        [SerializeField]
        private float radius;


        private List<JellyPoint> points;


        private Vector2 GetCenter()
        {
            Vector3 center = new Vector3();

            for (int i = 0; i < points.Count; i++)
            {
                center += points[i].transform.localPosition;
            }
            return center / points.Count;
        }


        private void Start()
        {
            points = GetComponentsInChildren<JellyPoint>().ToList();
            StartCoroutine(AnimatingJelly());
        }
        [ContextMenu("CreateCircle")]
        public void CreateCircle()
        {
            float delta = 2 * 3.14f / dencity;
            JellyPoint prefab = GetComponentInChildren<JellyPoint>();
            shapeRendererToModify.spline.Clear();
            for (int i = 0; i < dencity; i++)
            {
                JellyPoint newPoint = Instantiate(prefab, transform);

                newPoint.transform.localPosition = new Vector3(Mathf.Cos(((delta-1) - i) * delta), Mathf.Sin(((delta - 1) - i) * delta)) * radius;
                //Vector2 toCenterDirection = (Vector2.zero - vertex).normalized;

                shapeRendererToModify.spline.InsertPointAt(i, newPoint.transform.localPosition);
                shapeRendererToModify.spline.SetTangentMode(i, ShapeTangentMode.Continuous);
            }
            DestroyImmediate(prefab.gameObject);
            SetAllSpringJoints();
        }

        [ContextMenu("SetAllSpringJoints")]
        public void SetAllSpringJoints()
        {
            points = GetComponentsInChildren<JellyPoint>().ToList();
            for (int i = 0; i < points.Count; i++)
            {
                SpringJoint2D[] joints = points[i].GetComponents<SpringJoint2D>();
                if (i > 0 && i < points.Count - 1)
                {
                    joints[0].connectedBody = points[i - 1].GetComponent<Rigidbody2D>();
                    joints[1].connectedBody = points[i + 1].GetComponent<Rigidbody2D>();
                    points[i].GetComponent<DistanceJoint2D>().connectedBody = points[i - 1].GetComponent<Rigidbody2D>();
                }
                if (i == 0)
                {
                    joints[0].connectedBody = points[points.Count - 1].GetComponent<Rigidbody2D>();
                    joints[1].connectedBody = points[i + 1].GetComponent<Rigidbody2D>();
                    points[i].GetComponent<DistanceJoint2D>().connectedBody = points[points.Count - 1].GetComponent<Rigidbody2D>();
                }
                if(i == points.Count - 1)
                {
                    joints[0].connectedBody = points[0].GetComponent<Rigidbody2D>();
                    joints[1].connectedBody = points[i - 1].GetComponent<Rigidbody2D>();
                    points[i].GetComponent<DistanceJoint2D>().connectedBody = points[i - 1].GetComponent<Rigidbody2D>();
                }
                joints[2].connectedBody = GetComponent<Rigidbody2D>();
            }
        }
        private void UpdateVerticies()
        {
            for(int i = 0; i < points.Count; i++)
            {
                Vector2 vertex = points[i].transform.localPosition;
                print(GetCenter());
                Vector2 toCenterDirection = (GetCenter() - vertex).normalized;

                float colliderRadius = points[i].GetCollider().radius;

                try
                {
                    shapeRendererToModify.spline.SetPosition(i, vertex);
                }catch(Exception e)
                {
                    Debug.Log(e);
                    Debug.Log(shapeRendererToModify.spline.GetPointCount());
                    shapeRendererToModify.spline.SetPosition(i, (vertex - toCenterDirection * (colliderRadius + splineOffset)));
                }

                Vector2 lt = shapeRendererToModify.spline.GetLeftTangent(i);

                Vector2 newRt = Vector2.Perpendicular(toCenterDirection) * lt.magnitude;
                Vector2 newLt = -(newRt);
                shapeRendererToModify.spline.SetLeftTangent(i,newLt);
                shapeRendererToModify.spline.SetRightTangent(i, newRt);
            }
        }

        private IEnumerator AnimatingJelly()
        {
            while (true)
            {
                UpdateVerticies();
                yield return null;
            }
        }

    }
}