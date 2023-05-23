using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JellyPhisics
{
    public class BetterJelly : MonoBehaviour
    {
        [SerializeField]
        private List<JellyPoint> points;
        [SerializeField]
        private Vector3 upVector;

        [SerializeField]
        private Transform facingTransform;
        [SerializeField]
        private Transform backTransform;

        [SerializeField]
        private SkinnedMeshRenderer skinnedMeshRenderer;
        [SerializeField]
        private Mesh bakedMesh;

        private List<Vector3> initialRotations = new List<Vector3>();
        private List<float> initialAngles = new List<float>();

        [ContextMenu("MakeDistanceJoints")] 
        public void MakeDistanceJoints()
        {
            for (int i = 0; i < points.Count; i++)
            {
                DistanceJoint2D distanceJoint2D = gameObject.AddComponent<DistanceJoint2D>();
                distanceJoint2D.connectedBody = points[i].GetComponent<Rigidbody2D>();
            }
        }

        [ContextMenu("AttachSpringJointsToAll")]
        public void AttachSpringJointsToAll()
        {
            for (int i = 0; i < points.Count; i++)
            {
                for (int j = 0; j < points.Count; j++)
                {
                    if (points[i] == points[j]) continue;
                    SpringJoint2D springJoint = points[i].gameObject.AddComponent<SpringJoint2D>();
                    springJoint.connectedBody = points[j].GetComponent<Rigidbody2D>();
                    springJoint.frequency = 3;
                }
            }
        }


        private void FixedUpdate()
        {
            Vector3 center = new Vector3();
            for (int i = 0; i < points.Count; i++)
            {
                center += points[i].transform.position;
            }
            center /= points.Count;

            for (int i = 0; i < points.Count; i++)
            {
                points[i].FaceCenter(center);
            }
            facingTransform.position = new Vector3(center.x, center.y, facingTransform.position.z);
            backTransform.position = new Vector3(center.x, center.y, backTransform.position.z);
            //for (int i = 0; i < points.Count; i++)
            //{
            //    Vector3 directionToCenter = center - points[i].transform.position;
            //    /*Vector2 leftTangent = Vector2.Perpendicular(directionToCenter);
            //    float angle = Mathf.Atan2(leftTangent.y, leftTangent.x);*/
            //    directionToCenter.z = 0;

            //    float toCenterRotation =  Vector3.Angle(points[i].position, center);

            //    //points[i].transform.eulerAngles = new Vector3( toCenterRotation;
            //    //print(points[i].name + " " + points[i].transform.localEulerAngles.z);
            //    //Quaternion temp = points[i].transform.rotation;
            //    float angle = Mathf.Atan2(directionToCenter.y, directionToCenter.x) * Mathf.Rad2Deg;
            //    print(points[i].name + " to center " + directionToCenter);
            //    print(points[i].name + " tangent " + Mathf.Atan2(directionToCenter.y, directionToCenter.x) * Mathf.Rad2Deg);
            //    print(points[i].name + " rotation " + toCenterRotation * Mathf.Rad2Deg);

            //    //print(points[i].name + " " + points[i].rotation);
            //   // points[i].LookAt(center, upVector);//
               
            //    points[i].rotation = Quaternion.Euler(initialRotations[i].x, angle + 90, initialRotations[i].z);
            //    // points[i].transform.rotation.SetLookRotation(center);//, points[i].transform.up);//(new Vector3(0, 1 * Time.deltaTime, 0)); // = Quaternion.Euler(points[i].transform.localEulerAngles.x, toCenterRotation.eulerAngles.y, points[i].transform.localEulerAngles.z);
                
            //}


            //skinnedMeshRenderer.BakeMesh(bakedMesh);
        }

    }
}