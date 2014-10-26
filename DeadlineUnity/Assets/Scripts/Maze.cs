using System;

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Maze : MonoBehaviour
{

    public OfficeWorker officeWorkerPrefab;

    public Item itemPrefab;
    public Weapon weaponPrefab;

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

    private List<MazeRoom> rooms = new List<MazeRoom>();

    private MazeCell[,] cells;

    private List<OfficeWorker> officeWorkers = new List<OfficeWorker>();
    private List<Item> items = new List<Item>();
    private List<Weapon> weapons = new List<Weapon>();
    public IntVector2 RandomCoordinates
    {
        get
        {
            return new IntVector2(UnityEngine.Random.Range(0, size.x), UnityEngine.Random.Range(0, size.z));
        }
    }

    public bool ContainsCoordinates(IntVector2 coordinate)
    {
        return coordinate.x >= 0 && coordinate.x < size.x && coordinate.z >= 0 && coordinate.z < size.z;
    }

    public MazeCell GetCell(IntVector2 coordinates)
    {
        return cells[coordinates.x, coordinates.z];
    }

    public void Generate()
    {
        corruption = (GameObject)GameObject.Instantiate(corruption);
        cells = new MazeCell[size.x, size.z];
        List<MazeCell> activeCells = new List<MazeCell>();
        DoFirstGenerationStep(activeCells);
        while (activeCells.Count > 0)
        {
            DoNextGenerationStep(activeCells);
        }
        UnityEngine.Debug.Log(rooms.Count.ToString());
        for (int i = 0; i < rooms.Count; i++)
        {
            SpawnEnemies(rooms[i]);
        }
        for ( int i = 0; i < cells.GetLength( 0 ); i++ )
        {
            for ( int j = 0; j < cells.GetLength( 1 ); j++ )
            {
                PickObject( cells[ i, j ] );
            }
        }
        for ( int i = 0; i < rooms.Count; i++ )
        {
            //SpawnItems(rooms[i].CountCells());
            rooms[ i ].Hide( );
        }
    }

    public void SpawnEnemies(MazeRoom room)
    {
        int enemycount = 0;
        int numCells = room.CountCells();
        if (numCells <= 5)
            if ((int)Mathf.Floor(UnityEngine.Random.Range(1, 4)) == 1)
                enemycount = 1;
            else
            {
                enemycount = 0;
            }
        else if (numCells > 5 && numCells <= 10)
            enemycount = 1;
        else if (numCells > 10 && numCells <= 20)
            enemycount = (int)Mathf.Floor(UnityEngine.Random.Range(1, 2));
        else if (numCells > 20 && numCells <= 30)
            enemycount = (int)Mathf.Floor(UnityEngine.Random.Range(2, 4));
        else
            enemycount = 5;

        if (enemycount > 0)
            for (int i = 0; i < enemycount; i++)
            {
                SpawnOfficeWorker(room.RandomCell());
                //officeWorkers.Add(newOfficeWorker);
            }
    }

    public void SpawnOfficeWorker(MazeCell cell)
    {
        OfficeWorker tempOfficeWorker = Instantiate(officeWorkerPrefab) as OfficeWorker;
        tempOfficeWorker.SetInitialLocation(cell);
        tempOfficeWorker.transform.parent = cell.transform;
        officeWorkers.Add( tempOfficeWorker );
    }


    public void PickObject(MazeCell cell)
    {
        int picker = UnityEngine.Random.Range(0, 99);
        if (picker < 6)
            SpawnItem(cell);
        else if (picker < 9)
            SpawnWeapon(cell);
        else
            return;
    }

    public void SpawnItem(MazeCell cell)
    {
        Item tempItem = Instantiate(itemPrefab) as Item;
        tempItem.SetInitialLocation(cell);
        tempItem.transform.parent = cell.transform;
        items.Add(tempItem);
    }

    public void SpawnWeapon(MazeCell cell)
    {
        Weapon tempWeapon = Instantiate(weaponPrefab) as Weapon;
        tempWeapon.SetInitialLocation(cell);
        tempWeapon.transform.parent = cell.transform;
        weapons.Add(tempWeapon);
    }

        
    private void DoFirstGenerationStep(List<MazeCell> activeCells)
    {
        MazeCell newCell = CreateCell(RandomCoordinates);
        newCell.Initialize(CreateRoom(-1));
        activeCells.Add(newCell);
    }

    private void DoNextGenerationStep(List<MazeCell> activeCells)
    {
        int currentIndex = activeCells.Count - 1;
        MazeCell currentCell = activeCells[currentIndex];
        if (currentCell.IsFullyInitialized)
        {
            activeCells.RemoveAt(currentIndex);
            return;
        }
        MazeDirection direction = currentCell.RandomUninitializedDirection;
        IntVector2 coordinates = currentCell.coordinates + direction.ToIntVector2();
        if (ContainsCoordinates(coordinates))
        {
            MazeCell neighbor = GetCell(coordinates);
            if (neighbor == null)
            {
                neighbor = CreateCell(coordinates);
                CreatePassage(currentCell, neighbor, direction);
                activeCells.Add(neighbor);
            }
            else if (currentCell.room.settingsIndex == neighbor.room.settingsIndex)
            {
                CreatePassageInSameRoom(currentCell, neighbor, direction);
            }
            else
            {
                CreateWall(currentCell, neighbor, direction);
            }
        }
        else
        {
            CreateWall(currentCell, null, direction);
        }
    }

    private MazeCell CreateCell(IntVector2 coordinates)
    {
        MazeCell newCell = Instantiate(cellPrefab) as MazeCell;
        cells[coordinates.x, coordinates.z] = newCell;
        newCell.coordinates = coordinates;
        newCell.name = "Maze Cell " + coordinates.x + ", " + coordinates.z;
        newCell.transform.parent = transform;
        newCell.transform.localPosition = new Vector3(coordinates.x - size.x * 0.5f + 0.5f, 0f, coordinates.z - size.z * 0.5f + 0.5f);
        return newCell;
    }

    private void CreatePassage(MazeCell cell, MazeCell otherCell, MazeDirection direction)
    {
        MazePassage prefab = UnityEngine.Random.value < doorProbability ? doorPrefab : passagePrefab;
        MazePassage passage = Instantiate(prefab) as MazePassage;
        passage.Initialize(cell, otherCell, direction);
        passage = Instantiate(prefab) as MazePassage;
        if (passage is MazeDoor)
        {
            otherCell.Initialize(CreateRoom(cell.room.settingsIndex));
        }
        else
        {
            otherCell.Initialize(cell.room);
        }
        passage.Initialize(otherCell, cell, direction.GetOpposite());
    }

    private void CreatePassageInSameRoom(MazeCell cell, MazeCell otherCell, MazeDirection direction)
    {
        MazePassage passage = Instantiate(passagePrefab) as MazePassage;
        passage.Initialize(cell, otherCell, direction);
        passage = Instantiate(passagePrefab) as MazePassage;
        passage.Initialize(otherCell, cell, direction.GetOpposite());
        if (cell.room != otherCell.room)
        {
            MazeRoom roomToAssimilate = otherCell.room;
            cell.room.Assimilate(roomToAssimilate);
            rooms.Remove(roomToAssimilate);
            Destroy(roomToAssimilate);
        }
    }

    private void CreateWall(MazeCell cell, MazeCell otherCell, MazeDirection direction)
    {
        MazeWall wall = Instantiate(wallPrefabs[UnityEngine.Random.Range(0, wallPrefabs.Length)]) as MazeWall;
        wall.Initialize(cell, otherCell, direction);
        if (otherCell != null)
        {
            wall = Instantiate(wallPrefabs[UnityEngine.Random.Range(0, wallPrefabs.Length)]) as MazeWall;
            wall.Initialize(otherCell, cell, direction.GetOpposite());
        }
    }

    private MazeRoom CreateRoom(int indexToExclude)
    {
        MazeRoom newRoom = ScriptableObject.CreateInstance<MazeRoom>();
        newRoom.settingsIndex = UnityEngine.Random.Range(0, roomSettings.Length);
        if (newRoom.settingsIndex == indexToExclude)
        {
            newRoom.settingsIndex = (newRoom.settingsIndex + 1) % roomSettings.Length;
        }
        newRoom.settings = roomSettings[newRoom.settingsIndex];
        rooms.Add(newRoom);
        return newRoom;
    }
}