using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class TrackingSystem : MonoBehaviour
{
	public bool isTracking = false;    
    public ObjectPool objectPool;

    public GameObject player;
    public GameObject camParent;
    public Camera cam;

    public RenderTexture renderTexture;
    public RawImage rawImage;

    public float trackingRange = 500;
    private float mScale = 0.025f;

    private List<GameObject> mTrackingObjs = new List<GameObject>();
    private Vector3 mCenterPos = Vector3.zero;

    private void Awake()
    {
        objectPool.SetUp();
        cam.targetTexture = renderTexture;
        rawImage.texture = renderTexture;
        mScale = 5.0f / trackingRange;
        //cam.clearFlags = CameraClearFlags.Depth | CameraClearFlags.SolidColor;
    }

    private void FixedUpdate()
    {
        if (isTracking)
        {            
            var allMiner = FindObjectsOfType<Miner>();
            var allEnemies = FindObjectsOfType<Enemy>();

            int numOfTracking = 0;
            for (int i = 0; i < allMiner.Length; i++)
            {
                var direction = allMiner[i].transform.position - player.transform.position;
                var length = direction.magnitude;

                if (length <= trackingRange)
                {
                    var trackObj = PrepareTrackObj(numOfTracking);
                    trackObj.transform.position = mCenterPos + direction * mScale;
                    UpdateTrackEmissColor(trackObj, allMiner[i].trackingColor);
                    numOfTracking++;
                }
            }

            for (int i = 0; i < allEnemies.Length; i++)
            {
                var direction = allEnemies[i].transform.position - player.transform.position;
                var length = direction.magnitude;

                if (length <= trackingRange)
                {
                    var trackObj = PrepareTrackObj(numOfTracking);
                    trackObj.transform.position = mCenterPos + direction * mScale;
                    UpdateTrackEmissColor(trackObj, allEnemies[i].trackingColor);
                    numOfTracking++;
                }
            }

            CleanUpTrackObjs(numOfTracking);
        }
        //transform.forward = new Vector3(player.transform.forward.x, 0, player.transform.forward.z);
        //transform.rotation = player.transform.rotation;
        //camParent.transform.rotation = player.transform.rotation;
        camParent.transform.eulerAngles = new Vector3(0, player.transform.eulerAngles.y, 0);
    }

    void AllMinerMode()
    {
        // scale down all miner with the max distance
        var allMiner = FindObjectsOfType<Miner>();
        int idx = 0;
        float maxDistance = 0f;
        PrepareTrackObjs(allMiner.Length);

        // find the scale
        for (idx = 0; idx < allMiner.Length; idx++)
        {
            var direction = allMiner[idx].transform.position - player.transform.position;
            var length = direction.magnitude;
            maxDistance = length > maxDistance ? length : maxDistance;
        }

        float scale = 5.0f / maxDistance;
        if (maxDistance < 100)
        {
            scale = 5.0f / 100f;
        }

        // update mini tracking with scale, color
        for (idx = 0; idx < allMiner.Length; idx++)
        {
            var direction = allMiner[idx].transform.position - player.transform.position;
            //var length = direction.magnitude;
            mTrackingObjs[idx].transform.localPosition = mCenterPos + direction * scale;
            UpdateTrackEmissColor(mTrackingObjs[idx], allMiner[idx].trackingColor);
        }

        CleanUpTrackObjs(allMiner.Length);
    }

    private void PrepareTrackObjs(int maxNum)
    {
        while (mTrackingObjs.Count < maxNum)
        {
            var pooledObj = objectPool.GetPooledObject();
            pooledObj.SetActive(true);
            mTrackingObjs.Add(pooledObj);
        }
    }

    private GameObject PrepareTrackObj(int idx)
    {
        if (idx < mTrackingObjs.Count)
        {
            return mTrackingObjs[idx];
        }
        else
        {
            var pooledObj = objectPool.GetPooledObject();
            pooledObj.SetActive(true);
            mTrackingObjs.Add(pooledObj);
            return pooledObj;
        }
    }

    private void CleanUpTrackObjs(int maxTrackingNum)
    {
        for (int i = mTrackingObjs.Count - 1; i >= maxTrackingNum; i--)
        {
            // remove non-tracking obj
            mTrackingObjs[i].SetActive(false);
            mTrackingObjs.RemoveAt(i);
        }
    }

    private void UpdateTrackEmissColor(GameObject trackObject, Color color)
    {
        var mesh = trackObject.GetComponent<MeshRenderer>();
        mesh.material.SetColor("_EmissionColor", color);
    }

    public void ShowHideTracking(InputAction.CallbackContext button)
    {
        if (button.performed)
        {
            isTracking = !isTracking;
            rawImage.gameObject.SetActive(isTracking);
        }
    }
}

