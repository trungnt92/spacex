using UnityEngine;
using System.Collections;
using UnityEngine.UIElements;

public class Bullet : MonoBehaviour
{
	public float speed;
    public float lifeTime;

    private Rigidbody mRb;
    [HideInInspector]
    public GameObject owner;

    private void Awake()
    {
        mRb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //transform.position += (direction * speed * Time.deltaTime);
        //transform.position += (transform.forward * speed * Time.deltaTime);

        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void SetVelocity(Vector3 vel)
    {
        mRb.velocity = vel;
    }

    public void SetAngularVelocity(Vector3 vel)
    {
        mRb.angularVelocity = vel;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Obstacles")
        {
            // deactive both instead of destroy
            other.gameObject.SetActive(false);
            gameObject.SetActive(false);
            mRb.velocity = Vector3.zero;
            mRb.angularVelocity = Vector3.zero;
            gameObject.transform.rotation = Quaternion.identity;

            var trackingbuller = GetComponent<TrackingBullet>();
            if (trackingbuller != null)
            {
                trackingbuller.aimTarget = null;
                trackingbuller.enabled = false;
            }

            var trailRenderer = GetComponentInChildren<TrailRenderer>();
            if (trailRenderer != null)
            {
                trailRenderer.Clear();
            }

            // particle playing
            var scale = other.transform.localScale.x;
            ParticlelManager.Instance.PlayExplosive(transform.position, scale);

            if (owner != null && owner.tag == "Player")
            {
                // size smaller more point
                var score = Mathf.RoundToInt(90f / scale);
                GameManager.Instance.AddScore(score);
            }
        }
        else if (other.gameObject.tag == "Player" && owner != null && owner.tag == "Enemy")
        {
            // reduce health or teleport ?
        }
        else if (other.gameObject.tag == "Enemy" && owner != null && owner.tag == "Player")
        {
            var controller = other.gameObject.GetComponent<AIShipController>();
            controller.trackingTarget = null;

            other.gameObject.SetActive(false);
            gameObject.SetActive(false);

            ParticlelManager.Instance.PlayExplosive(transform.position, 20f);

            GameManager.Instance.ScoreEnemy();
        }
    }    
}

