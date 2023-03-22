using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class ShootSystem : MonoBehaviour
{
	public ObjectPool misslePool;
	public ShipSettings shipSettings;

    public ObjectPool bulletPool;

	public static ShootSystem Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        misslePool.SetUp();
        bulletPool.SetUp();
    }

    public void OnFire(InputAction.CallbackContext button)
    {
        if (button.performed)
        {
            if (AimingSystem.Instance.active)
            {
                AimFire(GameManager.Instance.player);
            }
            else
            {
                Fire();
            }
        }
    }

    public void Fire()
    {
        Fire(this.shipSettings, GameManager.Instance.player);        
    }

    public void Fire(ShipSettings shipSettings, GameObject owner)
    {
        if (shipSettings != null)
        {
            for (int i = 0; i < shipSettings.firePlaces.Count; i++)
            {
                var bulletObj = bulletPool.GetPooledObject();
                bulletObj.transform.position = shipSettings.firePlaces[i].position;
                bulletObj.transform.rotation = shipSettings.firePlaces[i].rotation;

                var bullet = bulletObj.GetComponent<Bullet>();
                bullet.lifeTime = 5;
                bullet.SetVelocity(shipSettings.firePlaces[i].transform.forward * bullet.speed);
                bullet.owner = owner;

                //var trackingBuller = bulletObj.GetComponent<TrackingBullet>();
                //trackingBuller.enabled = false;

                bulletObj.SetActive(true);
            }
        }
    }

    public void AimFire(GameObject owner)
    {
        if (shipSettings != null && AimingSystem.Instance.lockingTarget != null)
        {
            for (int i = 0; i < shipSettings.firePlaces.Count; i++)
            {
                var bulletObj = misslePool.GetPooledObject();
                bulletObj.transform.position = shipSettings.firePlaces[i].position;
                bulletObj.transform.rotation = shipSettings.firePlaces[i].rotation;

                var bullet = bulletObj.GetComponent<Bullet>();
                bullet.lifeTime = 20;
                bullet.SetVelocity(shipSettings.firePlaces[i].transform.forward * bullet.speed);
                bullet.owner = owner;

                var trackingBuller = bulletObj.GetComponent<TrackingBullet>();
                trackingBuller.aimTarget = AimingSystem.Instance.lockingTarget;
                trackingBuller.enabled = true;

                bulletObj.SetActive(true);
            }
        }
    }
}

