using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
	public void StartGame()
    {
        SceneManager.LoadScene("Main");
    }

    public void ShipSelection()
    {
        SceneManager.LoadScene("ShipSelection");
    }
}

