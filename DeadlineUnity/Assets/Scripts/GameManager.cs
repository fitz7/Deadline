using System;
using UnityEngine;
using System.Collections;
using Object = UnityEngine.Object;

public class GameManager : UnityObserver {

	public Maze mazePrefab;

	public Player playerPrefab;

    public const string NEXT_LEVEL ="NEXT_LEVEL";

    public Exit exitPrefab;

	private Maze mazeInstance;

	private Player playerInstance;

    private Exit exitInstance;

    public int Level = 1;
	private void Start () {
		BeginGame();
	}
	
	private void Update () {
#if UNITY_EDITOR
		if (Input.GetKeyDown(KeyCode.KeypadEnter)) {
			NextLevel();
		}
#endif
	}

    public override void OnNotify(Object sender, EventArguments e)
    {
        if (e.eventMessage == NEXT_LEVEL)
        {
            NextLevel();
            
        }
    }

    private void BeginGame () {
		Camera.main.clearFlags = CameraClearFlags.Skybox;
		Camera.main.rect = new Rect(0f, 0f, 1f, 1f);
		mazeInstance = Instantiate(mazePrefab) as Maze;
		mazeInstance.Generate();
        if(Level ==1 )
		playerInstance = Instantiate(playerPrefab) as Player;
	    MazeCell playerloc = mazeInstance.GetCell(mazeInstance.RandomCoordinates);
		
	    MazeCell exitLoc;
	    do
	    {
	        exitLoc = mazeInstance.GetCell(mazeInstance.RandomCoordinates);
	    } while (!CanPlaceExit(playerloc, exitLoc));
	    exitInstance = Instantiate(exitPrefab) as Exit;
	    exitInstance.SetInitialLocation(exitLoc);
	    exitInstance.transform.parent = exitLoc.transform;
	    exitLoc.isExit = true;
        mazeInstance.HideRooms();
        playerInstance.SetLocation(playerloc);
		Camera.main.clearFlags = CameraClearFlags.Depth;
		Camera.main.rect = new Rect(0f, 0f, 0.5f, 0.5f);
	}

    private bool CanPlaceExit(MazeCell player, MazeCell exit)
    {
        int xd = player.coordinates.x - exit.coordinates.x;
        int yd = player.coordinates.z - exit.coordinates.z;
        float distance = Mathf.Sqrt(xd*xd + yd*yd);
        if (distance>9 && player.room != exit.room)
            return true;
        else return false;
    }
	private void RestartGame () {
		Destroy(mazeInstance.gameObject);
		if (playerInstance != null) {
			Destroy(playerInstance.gameObject);
		}
    BeginGame();
	}

    private void NextLevel()
    {
        Destroy(mazeInstance.gameObject);
        StopAllCoroutines();
        Destroy(mazeInstance.corruption.gameObject);
        Level++;
        BeginGame();
    }
}