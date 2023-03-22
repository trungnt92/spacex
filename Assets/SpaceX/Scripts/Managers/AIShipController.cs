using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIShipController : MonoBehaviour
{
    [HideInInspector]
    public GameObject trackingTarget;

    [SerializeField]
    private ShipSettings mShipSettings;

    private Rigidbody mRb;

    public float avoidDistance = 10f;
    public float firingDistance = 20f;
    public float firingInterval = 2f;
    private float mFiringWaitTime = 0;

    private void Awake()
    {
        mRb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        CalculateMovement();
        CalculateFiring();
    }

    private void CalculateMovement()
    {
        if (trackingTarget != null)
        {
            var direction = trackingTarget.transform.position - transform.position;

            RaycastHit hit;
            if (Physics.Raycast(transform.position, direction, out hit, 50f, LayerMask.NameToLayer("Obstacles")))
            {
                var avoidDirection = hit.point - hit.collider.bounds.center;
                var newLookAt = hit.point + (avoidDirection.normalized * avoidDistance);
                direction = newLookAt - transform.position;
            }

            transform.forward = direction.normalized;
            mRb.velocity = transform.forward * mShipSettings.baseSpeed * 5;            
        }
    }

    private void CalculateFiring()
    {
        if (trackingTarget != null)
        {
            var distance = (trackingTarget.transform.position - transform.position).magnitude;
            mFiringWaitTime -= Time.fixedDeltaTime;
            if (distance <= firingDistance && mFiringWaitTime <= 0)
            {
                ShootSystem.Instance.Fire(mShipSettings, gameObject);
                mFiringWaitTime = firingInterval;
            }            
        }
    }
}
