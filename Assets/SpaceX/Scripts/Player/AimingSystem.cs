using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class AimingSystem : MonoBehaviour
{
	public GameObject aimingObj;

	private Material mMaterial;
	public GameObject lockingTarget { get; private set; }

	public static AimingSystem Instance { get; private set; }

	private bool mActive;
	public bool active
    {
		get
        {
			return mActive;
        }
		set
        {
			mActive = value;
			aimingObj.SetActive(mActive);
			gameObject.SetActive(mActive);
        }
    }

    private void Awake()
    {
		Instance = this;
    }

    void Start()
	{
		mMaterial = aimingObj.GetComponent<MeshRenderer>().material;
	}

	void Update()
	{
		var ray = new Ray(aimingObj.transform.position, aimingObj.transform.forward);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, 200, LayerMask.GetMask("Obstacles")))
        {
			mMaterial.SetColor("_Color", Color.red);
			lockingTarget = hit.collider.gameObject;
        }
        else
        {
			mMaterial.SetColor("_Color", Color.green);
			lockingTarget = null;
		}
	}

	public void OnAim(InputAction.CallbackContext button)
	{
		Debug.Log("On Aim");
		active = button.started || button.performed;
	}
}

