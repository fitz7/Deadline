using System;

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Maze : MonoBehaviour {
    public OfficeWorker officeWorkerPrefab;
	public IntVector2 size;

	public MazeCell cellPrefab;

	public float generationStepDelay;

	public MazePassage passagePrefab;

	public MazeDoor doorPrefab;

    public GameObject corruption;

	[Range(0f, 1f)]
	public float doorProbability;

	public MazeWall[] wallPrefabs;

	public MazeRoomSettings[] roomSettings;

    private List<MazeRoom> rooms = new List<MazeRoom>( );

	private MazeCell[,] cells;

    private List<OfficeWorker> officeWorkers;

   
	public IntVector2 RandomCoordinates {
		get {
            return new IntVector2(UnityEngine.Random.Range(0, size.x), UnityEngine.Random.Range(0, size.z));
		}
	}

	public bool ContainsCoordinates (IntVector2 coordinate) {
		return coordinate.x >= 0 && coordinate.x < size.x && coordinate.z >= 0 && coordinate.z < size.z;
	}

	public MazeCell GetCell (IntVector2 coordinates) {
		return cells[coordinates.x, coordinates.z];
	}

	public void Generate () {
        corruption = ( GameObject )GameObject.Instantiate( corruption );
		cells = new MazeCell[size.x, size.z];
		List<MazeCell> activeCells = new List<MazeCell>();
		DoFirstGenerationStep(activeCells);
		while (activeCells.Count > 0) {
			DoNextGenerationStep(activeCells);
		}
        UnityEngine.Debug.Log(rooms.Count.ToString());
		for (int i = 0; i < rooms.Count; i++) {
            SpawnEnemies(rooms[i]);
            //SpawnItems(rooms[i].CountCells());
			rooms[i].Hide();
		}
	}

    public void SpawnEnemies(MazeRoom room)
    {
        int enemycount;
        int numCells = room.CountCells();
        if (numCells <= 5)
            enemycount = 0;
        else if (numCells > 5 && numCells <= 10)
            enemycount = 1;
        else if (numCells > 10 && numCells <= 15)
            enemycount = (int)Mathf.Floor(UnityEngine.Random.Range(1, 2));
        else
            enemycount = 2;

        if(enemycount>0)
        for (int i = 0; i < enemycount; i++)
        {

            OfficeWorker  newOfficeWorker = SpawnOfficeWorker(room.RandomCell());
            officeWorkers.Add(newOfficeWorker);
            
        }
    }

    public OfficeWorker SpawnOfficeWorker(MazeCell cell)
    {
        OfficeWorker tempOfficeWorker = Instantiate(officeWorkerPrefab) as OfficeWorker;
         tempOfficeWorker.SetInitialLocation(cell);
        tempOfficeWorker.transform.parent = cell.transform;
        return tempOfficeWorker;
    }
    public void SpawnItems(int cells)
    {
        int items;
        if (cells <= 5)
            items = 0;
        else if (cells > 5 && cells <= 10)
            items = 1;
        else if (cells > 10 && cells <= 15)
            items = (int)Mathf.Floor(UnityEngine.Random.Range(1, 2));
        else
            items = 2;
        for (int i = 0; i <= items; i++)
        {

        }
    }
	private void DoFirstGenerationStep (List<MazeCell> activeCells) {
		MazeCell newCell = CreateCell(RandomCoordinates);
		newCell.Initialize(CreateRoom(-1));
		activeCells.Add(newCell);
	}

	private void DoNextGenerationStep (List<MazeCell> activeCells) {
		int currentIndex = activeCells.Count - 1;
		MazeCell currentCell = activeCells[currentIndex];
		if (currentCell.IsFullyInitialized) {
			activeCells.RemoveAt(currentIndex);
			return;
		}
		MazeDirection direction = currentCell.RandomUninitializedDirection;
		IntVector2 coordinates = currentCell.coordinates + direction.ToIntVector2();
		if (ContainsCoordinates(coordinates)) {
			MazeCell neighbor = GetCell(coordinates);
			if (neighbor == null) {
				neighbor = CreateCell(coordinates);
				CreatePassage(currentCell, neighbor, direction);
				activeCells.Add(neighbor);
			}
			else if (currentCell.room.settingsIndex == neighbor.room.settingsIndex) {
				CreatePassageInSameRoom(currentCell, neighbor, direction);
			}
			else {
				CreateWall(currentCell, neighbor, direction);
			}
		}
		else {
			CreateWall(currentCell, null, direction);
		}
	}

	private MazeCell CreateCell (IntVector2 coordinates) {
		MazeCell newCell = Instantiate(cellPrefab) as MazeCell;
		cells[coordinates.x, coordinates.z] = newCell;
		newCell.coordinates = coordinates;
		newCell.name = "Maze Cell " + coordinates.x + ", " + coordinates.z;
		newCell.transform.parent = transform;
		newCell.transform.localPosition = new Vector3(coordinates.x - size.x * 0.5f + 0.5f, 0f, coordinates.z - size.z * 0.5f + 0.5f);
		return newCell;
	}

	private void CreatePassage (MazeCell cell, MazeCell otherCell, MazeDirection direction) {
        MazePassage prefab = UnityEngine.Random.value < doorProbability ? doorPrefab : passagePrefab;
		MazePassage passage = Instantiate(prefab) as MazePassage;
		passage.Initialize(cell, otherCell, direction);
		passage = Instantiate(prefab) as MazePassage;
		if (passage is MazeDoor) {
			otherCell.Initialize(CreateRoom(cell.room.settingsIndex));
		}
		else {
			otherCell.Initialize(cell.room);
		}
		passage.Initialize(otherCell, cell, direction.GetOpposite());
	}

	private void CreatePassageInSameRoom (MazeCell cell, MazeCell otherCell, MazeDirection direction) {
		MazePassage passage = Instantiate(passagePrefab) as MazePassage;
		passage.Initialize(cell, otherCell, direction);
		passage = Instantiate(passagePrefab) as MazePassage;
		passage.Initialize(otherCell, cell, direction.GetOpposite());
		if (cell.room != otherCell.room) {
			MazeRoom roomToAssimilate = otherCell.room;
			cell.room.Assimilate(roomToAssimilate);
			rooms.Remove(roomToAssimilate);
			Destroy(roomToAssimilate);
		}
	}

	private void CreateWall (MazeCell cell, MazeCell otherCell, MazeDirection direction) {
        MazeWall wall = Instantiate(wallPrefabs[UnityEngine.Random.Range(0, wallPrefabs.Length)]) as MazeWall;
		wall.Initialize(cell, otherCell, direction);
		if (otherCell != null) {
            wall = Instantiate(wallPrefabs[UnityEngine.Random.Range(0, wallPrefabs.Length)]) as MazeWall;
			wall.Initialize(otherCell, cell, direction.GetOpposite());
		}
	}

	private MazeRoom CreateRoom (int indexToExclude) {
		MazeRoom newRoom = ScriptableObject.CreateInstance<MazeRoom>();
        newRoom.settingsIndex = UnityEngine.Random.Range(0, roomSettings.Length);
		if (newRoom.settingsIndex == indexToExclude) {
			newRoom.settingsIndex = (newRoom.settingsIndex + 1) % roomSettings.Length;
		}
		newRoom.settings = roomSettings[newRoom.settingsIndex];
		rooms.Add(newRoom);
		return newRoom;
	}
}