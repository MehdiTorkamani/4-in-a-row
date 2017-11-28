using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

	public static UIManager Instance;

	[SerializeField] GameObject startPanel;
	[SerializeField] GameObject endPanel;
	[SerializeField] GameObject rIcon;
	[SerializeField] GameObject yIcon;

	void Awake()
	{
		if (Instance == null)
			Instance = this;
		else
			Destroy (this);
	}

	public void BeginGame(int a)
	{
		startPanel.SetActive (false);
		GameController.Instance.turn = a;
		GameController.Instance.currentBall = Instantiate (GameController.Instance.turn == 1 ? GameController.Instance.rBall : GameController.Instance.yBall);
		GameController.Instance.currentBall.GetComponent<Rigidbody2D> ().isKinematic = true;
		GameController.Instance.gameStarted = true;
	}

	public IEnumerator GameEnded (int a)
	{
		yield return new WaitForSeconds (2f);

		endPanel.SetActive (true);
		if (a == 1)
			rIcon.SetActive (true);
		else
			yIcon.SetActive (true);
	}

	public void RestartGame()
	{
		SceneManager.LoadScene (0);
	}
}
