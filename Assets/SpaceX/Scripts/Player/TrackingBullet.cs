using System;
using UnityEngine;

[RequireComponent(typeof(Bullet))]
public class TrackingBullet : MonoBehaviour
{
	public GameObject aimTarget;
	private Bullet mBullet;
    public float angleChangingSpeed = 20f;

    private void Awake()
    {
        mBullet = GetComponent<Bullet>();
    }

    void FixedUpdate()
    {
        //Vector3 direction = aimTarget.transform.position - transform.position;
        //direction.Normalize();
        //float rotateAmount = Vector3.Cross(direction, transform.up).z;
        //bullet.SetAngularVelocity(new Vector3(0, -angleChangingSpeed * rotateAmount, 0));
        if (aimTarget != null && aimTarget.activeInHierarchy)
        {
            transform.LookAt(aimTarget.transform);
            mBullet.SetVelocity(transform.forward * mBullet.speed);
        }

        //if lose target auto destroy
        if (aimTarget != null && !aimTarget.activeInHierarchy)
        {
            mBullet.lifeTime = 0;
        }
    }
}

