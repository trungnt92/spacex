using UnityEngine;
using System.Collections;

public class AutoMove : MonoBehaviour
{
	public Vector3 direction;
	public float speed;

	// Update is called once per frame
	void Update()
	{
		transform.position += direction * speed * Time.deltaTime;
	}
}

