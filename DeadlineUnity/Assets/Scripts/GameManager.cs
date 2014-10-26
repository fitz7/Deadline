using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public Maze mazePrefab;

	public Player playerPrefab;

    public OfficeWorker officeWorkerPrefab;

	private Maze mazeInstance;

	private Player playerInstance;

	private void Start () {
		BeginGame();
	}
	
	private void Update () {
		if (Input.GetKeyDown(KeyCode.Space)) {
			RestartGame();
		}
	}

	private void BeginGame () {
		Camera.main.clearFlags = CameraClearFlags.Skybox;
		Camera.main.rect = new Rect(0f, 0f, 1f, 1f);
		mazeInstance = Instantiate(mazePrefab) as Maze;
		mazeInstance.Generate();
		playerInstance = Instantiate(playerPrefab) as Player;
	    MazeCell playerloc = mazeInstance.GetCell(mazeInstance.RandomCoordinates);
		playerInstance.SetLocation(playerloc);
	    MazeCell exitLoc;
	    do
	    {
            exitLoc = mazeInstance.GetCell(mazeInstance.RandomCoordinates); 
	    }
        while (!CanPlaceExit(playerloc.coordinates,exitLoc.coordinates)))
		Camera.main.clearFlags = CameraClearFlags.Depth;
		Camera.main.rect = new Rect(0f, 0f, 0.5f, 0.5f);
	}

    private bool CanPlaceExit(IntVector2 player, IntVector2 exit)
    {
        
    }
	private void RestartGame () {
		Destroy(mazeInstance.gameObject);
		if (playerInstance != null) {
			Destroy(playerInstance.gameObject);
		}
    BeginGame();
	}
}