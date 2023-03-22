using UnityEngine;
using System.Collections;

public class ParticlelManager : MonoBehaviour
{
	public ObjectPool objectPool;

	public static ParticlelManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        objectPool.SetUp();
    }

    public void PlayExplosive(Vector3 pos, float scale)
    {
        var particle = objectPool.GetPooledObject();
        var particelSys = particle.GetComponent<ParticleSystem>();
        particle.transform.position = pos;
        Debug.Log("Scale explosive " + scale);
        particle.transform.localScale = Vector3.one * scale;
        particle.SetActive(true);
        particelSys.Play(true);

        StartCoroutine(Deactive(particle));
    }

    IEnumerator Deactive(GameObject particle)
    {
        yield return new WaitForSeconds(2);
        particle.SetActive(false);
    }
}

