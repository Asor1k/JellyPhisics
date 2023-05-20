using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JellyPhisics
{
    public class BetterJelly : MonoBehaviour
    {
        [SerializeField]
        private List<Transform> points;

        [SerializeField]
        private SkinnedMeshRenderer skinnedMeshRenderer;
        [SerializeField]
        private Mesh bakedMesh;


        private void FixedUpdate()
        {
            Vector3 center = new Vector3();
            for (int i = 0; i < points.Count; i++)
            {
                center += points[i].position;
            }
            center /= points.Count;
            for (int i = 0; i < points.Count; i++)
            {
                Vector3 directionToCenter = center - points[i].transform.position;
                /*Vector2 leftTangent = Vector2.Perpendicular(directionToCenter);
                float angle = Mathf.Atan2(leftTangent.y, leftTangent.x);*/
                directionToCenter.z = 0;

                float toCenterRotation =  Vector3.Angle(points[i].position, center);
                
                //points[i].transform.eulerAngles = new Vector3( toCenterRotation;
                //print(points[i].name + " " + points[i].transform.localEulerAngles.z);
                //Quaternion temp = points[i].transform.rotation;
                print(points[i].name + " to center " + directionToCenter);
                print(points[i].name + " tangent " + Mathf.Atan2(directionToCenter.y, directionToCenter.x) * Mathf.Rad2Deg);
                print(points[i].name + " rotation " + toCenterRotation * Mathf.Rad2Deg);
                
                //print(points[i].name + " " + points[i].rotation);
                 points[i].localRotation = Quaternion.Euler(points[i].localEulerAngles.x, Mathf.Atan2(directionToCenter.y, directionToCenter.x) * Mathf.Rad2Deg, points[i].localEulerAngles.z);
                // points[i].transform.rotation.SetLookRotation(center);//, points[i].transform.up);//(new Vector3(0, 1 * Time.deltaTime, 0)); // = Quaternion.Euler(points[i].transform.localEulerAngles.x, toCenterRotation.eulerAngles.y, points[i].transform.localEulerAngles.z);
                
            }


            //skinnedMeshRenderer.BakeMesh(bakedMesh);
        }

    }
}