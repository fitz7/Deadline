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
		playerInstance.SetLocation(mazeInstance.GetCell(mazeInstance.RandomCoordinates));
        CreateStubEnemy( );
		Camera.main.clearFlags = CameraClearFlags.Depth;
		Camera.main.rect = new Rect(0f, 0f, 0.5f, 0.5f);
	}

    private void CreateStubEnemy( )
    {
        officeWorkerPrefab = Instantiate( officeWorkerPrefab ) as OfficeWorker;
        officeWorkerPrefab.SetInitialLocation( mazeInstance.GetCell( mazeInstance.RandomCoordinates ) );
    }

	private void RestartGame () {
		Destroy(mazeInstance.gameObject);
		if (playerInstance != null) {
			Destroy(playerInstance.gameObject);
		}
    BeginGame();
	}
}