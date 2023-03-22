using UnityEngine;
using System.Collections;

public class Miner : MonoBehaviour
{
    [ColorUsage(true, true)]
    public Color trackingColor;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // Update game manager
            GameManager.Instance.CollectMiner(this);
            gameObject.SetActive(false);
        }
    }
}

