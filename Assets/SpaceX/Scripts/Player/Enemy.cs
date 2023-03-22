using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    [ColorUsage(true, true)]
    public Color trackingColor;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // Update game manager
            GameManager.Instance.CollideEnemy(this);
            gameObject.SetActive(false);
        }
    }
}

