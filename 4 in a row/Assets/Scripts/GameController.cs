using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public static GameController Instance;

	public GameObject rBall;
	public GameObject yBall;
	public GameObject currentBall;

	public int [][] gaps = new int[7][];
	public GameObject [][] coins = new GameObject[7][];

	public int turn = 1;
	public bool readyForNextBall = true;
	public bool gameFinished;
	public bool gameStarted;

	float x;

	void Awake()
	{
		if (Instance == null)
			Instance = this;
		else
			Destroy (this);

		for (int i = 0; i < gaps.Length; i++)
		{
			gaps [i] = new int[6];
			coins [i] = new GameObject[6];
		}
	}


	void Update()
	{
		if (gameFinished || !gameStarted)
			return;

		bool valid = false;

		Vector3 choosePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		x = RestrictX (choosePos.x, out valid);

		if (currentBall != null)
			currentBall.transform.position = new Vector2 (x, 3.2f);

		if (Input.GetMouseButtonDown (0) && valid && gaps [(int)x+3][5] == 0 && readyForNextBall)
			StartCoroutine (DropBall ());
	}
		

	float RestrictX(float x, out bool valid)
	{
		if (x > -3.5f && x < 3.5f)
		{	
			valid = true;
			return Mathf.Round (x);
		}

		valid = false;
		return x;
	}

	IEnumerator DropBall()
	{
		readyForNextBall = false;
		int i = (int)x + 3;
		for (int j = 0; j < 6; j++)
		{
			if (gaps [i] [j] == 0)
			{
				gaps [i] [j] = turn;
				coins[i] [j] = currentBall; 
				CheckAllDirections (i, j);
				break;
			}
		}

		GameObject nextBall = (turn == 1) ? yBall : rBall;
		turn = (turn == 1) ? -1 : 1;
		currentBall.GetComponent<Rigidbody2D> ().isKinematic = false;
		currentBall = null;

		yield return new WaitForSeconds (1f);
		currentBall = Instantiate (nextBall);
		currentBall.GetComponent<Rigidbody2D> ().isKinematic = true;
		readyForNextBall = true;
	}

	public void CheckAllDirections(int i, int j)
	{
		CheckVertical (i, j);
		CheckHorizontal (j);
		CheckDiagonal ();
	}

	public void CheckVertical (int i, int j)
	{
		if (j > 2) {
			int[] check = new int[]{i, j, i, j - 1, i, j - 2, i, j - 3 };
			CheckWin (check);
		}
	}

	public void CheckHorizontal (int j)
	{
		for (int i = 0; i < gaps.Length - 3; i++) {
			int[] check = new int[]{i, j, i+1, j, i+2, j, i+3, j };
			CheckWin (check);
		}
	}

	public void CheckDiagonal()
	{
		for(int j = 0; j < 3; j++)
		{
			for (int i = 0; i < gaps.Length - 3; i++) {
				int[] check = new int[]{i, j, i+1, j+1, i+2, j+2, i+3, j+3 };
				CheckWin (check);

				check = new int[]{gaps.Length -i -1, j, gaps.Length - i - 2, j+1, gaps.Length -i - 3, j+2, gaps.Length -i -4, j+3 };
				CheckWin (check);
			}
		}
	}

	public void CheckWin(int [] array)
	{
		int w = 0;
		for (int i = 0; i < 8; i+=2)
			w += gaps [array[i]] [array[i + 1]];

		if (w == 4 || w == -4)
			gameFinished = true;

		if (w == 4)
		{
			WinColor (array);
			UIManager.Instance.StartCoroutine(UIManager.Instance.GameEnded (1));
		}

		if (w == -4)
		{
			WinColor (array);
			UIManager.Instance.StartCoroutine(UIManager.Instance.GameEnded (-1));
		}
	}

	public void WinColor(int [] array)
	{
		for (int i = 0; i < 8; i+=2)
			coins[array[i]][array[i+1]].GetComponent<ColorChange>().enabled = true;
	}
}
