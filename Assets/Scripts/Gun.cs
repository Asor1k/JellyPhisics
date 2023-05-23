using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JellyPhisics
{
    public class Gun : MonoBehaviour
    {
        [SerializeField]
        private float movementSpeed;
        [SerializeField]
        private float recoilSpeed;
        [SerializeField]
        private Bullet bulletPrefab;
        [SerializeField]
        private Transform bulletPoint;


        private float lastShotTime;

        private void MoveToMouse()
        {
            Vector2 mousePos = Input.mousePosition;
            Vector2 gunPos = Camera.main.ScreenToWorldPoint(mousePos);
            gunPos.y = transform.position.y;

            transform.position = Vector3.Lerp(transform.position, gunPos, Time.deltaTime * movementSpeed);
        }

        private void Shoot()
        {
            lastShotTime = Time.time;
            Bullet bullet = Instantiate(bulletPrefab, bulletPoint.position, Quaternion.identity);
        }

        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                MoveToMouse();
                if(Time.time - lastShotTime > 1 / recoilSpeed)
                {
                    Shoot();
                }
            }
        }
    }
}