using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShipSelectionManager : MonoBehaviour
{
	public PoolData shipData;
	public Transform shipParent;
	public Button btnPrev;
	public Button btnNext;

	private int mSelectedIdx;
	private GameObject mCurrentShip;
	private readonly float sTimeMove = 1.5f;

	// Use this for initialization
	void Start()
	{
		mSelectedIdx = GameHelper.LoadShipSelection();
		mCurrentShip = Instantiate(shipData.prefabs[mSelectedIdx], shipParent);
		var rotateY = mCurrentShip.AddComponent<AutoRotateY>();
		rotateY.speed = 20f;
		//CheckShipAvailable();
	}

	private void CheckShipAvailable()
    {
		btnPrev.interactable = mSelectedIdx == 0 ? false : true;
		btnNext.interactable = mSelectedIdx == (shipData.prefabs.Count - 1) ? false : true;
    }

	private void ActivePrevNext()
    {
		btnPrev.interactable = true;
		btnNext.interactable = true;
	}

	private void DisablePrevNext()
	{
		btnPrev.interactable = false;
		btnNext.interactable = false;
	}

	public void PrevShip()
    {
		mSelectedIdx--;
		if (mSelectedIdx < 0)
		{
			mSelectedIdx = shipData.prefabs.Count - 1;
		}
		DisablePrevNext();
		var move = mCurrentShip.GetComponent<AutoMove>();
		if (move == null)
        {
			move = mCurrentShip.AddComponent<AutoMove>();
        }
		move.direction = Vector3.right;
		move.speed = 16;
		move.enabled = true;
		var destroy = mCurrentShip.AddComponent<AutoDestroy>();
		destroy.lifeTime = sTimeMove;

		var newShip = Instantiate(shipData.prefabs[mSelectedIdx], shipParent);
		newShip.transform.position = new Vector3(-24, 0, 0);
		var moveN = newShip.AddComponent<AutoMove>();
		moveN.direction = Vector3.right;
		moveN.speed = 16;
		mCurrentShip = newShip;

		StartCoroutine(StopShip());
	}

	public void NextShip()
    {
		mSelectedIdx++;
		if (mSelectedIdx == shipData.prefabs.Count)
        {
			mSelectedIdx = 0;
        }
		DisablePrevNext();
		var move = mCurrentShip.GetComponent<AutoMove>();
		if (move == null)
		{
			move = mCurrentShip.AddComponent<AutoMove>();
		}
		move.direction = Vector3.left;
		move.speed = 16;
		move.enabled = true;
		var destroy = mCurrentShip.AddComponent<AutoDestroy>();
		destroy.lifeTime = sTimeMove;

		var newShip = Instantiate(shipData.prefabs[mSelectedIdx], shipParent);
		newShip.transform.position = new Vector3(24, 0, 0);
		var moveN = newShip.AddComponent<AutoMove>();
		moveN.direction = Vector3.left;
		moveN.speed = 16;
		mCurrentShip = newShip;

		StartCoroutine(StopShip());
	}

	IEnumerator StopShip()
    {
		yield return new WaitForSeconds(sTimeMove);
		var move = mCurrentShip.GetComponent<AutoMove>();
		move.enabled = false;
		var rotateY = mCurrentShip.AddComponent<AutoRotateY>();
		rotateY.speed = 20f;
		mCurrentShip.transform.position = Vector3.zero;
		//CheckShipAvailable();
		ActivePrevNext();
	}

	public void BackToMenu()
    {
		GameHelper.SaveShipSelection(mSelectedIdx);
		SceneManager.LoadScene("Menu");
    }
}

