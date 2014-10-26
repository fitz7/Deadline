using UnityEngine;
using System.Collections;

public enum WORKER_MOVEMENT
{
    CLOCKWISE,
    COUNTER_CLOCKWISE,
    OPPOSITE
}

public enum EnemyType
{
    Female,
    Male,
    Janitor
}

public class OfficeWorker : UnityObserver
{

    public const string MOVE_ENEMY = "MOVE_ENEMY";
    private MazeDirection currentDirection;
    private WORKER_MOVEMENT workerMovement = WORKER_MOVEMENT.CLOCKWISE;
    private MazeCell currentCell;
    private MazeCell playersCurrentCell;
    private MazeCell cachedPlayerCell;
    private MazeRoom currentRoom;
    private bool invertClockwiseRotation;
    private EnemyType enemyType;
    private int health;
    private int damage;

    public override void OnNotify(Object sender, EventArguments e)
    {
        if (e.eventMessage == MOVE_ENEMY)
        {
            playersCurrentCell = (MazeCell)sender;
            SearchForPlayer();
        }
    }

    void Start()
    {
        SelectRandomEnemy();
    }

    private void SelectRandomEnemy()
    {
        int enumCount = EnemyType.GetNames(typeof(EnemyType)).Length;
        System.Random rnd = new System.Random();
        int selection = rnd.Next(0, 10);

        if (selection <= 5)
        {
            enemyType = EnemyType.Female;
            Material[] pencil = new Material[1];
            pencil[0] = mats[0];
            transform.GetChild(0).renderer.materials = pencil;
        }
        else if (selection > 5 && selection < 9)
        {
            enemyType = EnemyType.Male;
            Material[] keyboard = new Material[1];
            keyboard[0] = mats[1];
            transform.GetChild(0).renderer.materials = keyboard;
        }
        else if (selection == 9)
        {
            enemyType = EnemyType.Janitor;
            Material[] stapler = new Material[1];
            stapler[0] = mats[2];
            transform.GetChild(0).renderer.materials = stapler;
        }
    }

    public void SetInitialLocation(MazeCell cell)
    {
        currentRoom = cell.room;
        SetLocation(cell);
    }

    private void SetLocation(MazeCell cell)
    {
        currentCell = cell;
        this.transform.position = currentCell.transform.position;
    }

    private void Move(MazeDirection direction)
    {
        MazeCellEdge edge = currentCell.GetEdge(direction);
        if (edge is MazePassage
             && edge.otherCell.room == currentRoom)
        {
            SetLocation(edge.otherCell);
            return;
        }
        workerMovement = (WORKER_MOVEMENT)Random.Range(0, 3);
        if (workerMovement == WORKER_MOVEMENT.CLOCKWISE)
        {
            currentDirection = currentDirection.GetNextClockwise();
            return;
        }

        if (workerMovement == WORKER_MOVEMENT.OPPOSITE)
        {
            currentDirection = currentDirection.GetOpposite();
            return;
        }
        if (workerMovement == WORKER_MOVEMENT.COUNTER_CLOCKWISE)
        {
            currentDirection = currentDirection.GetNextCounterclockwise();
        }
    }

    private void SearchForPlayer()
    {
        if (playersCurrentCell.room != currentRoom)
        {
            Move(currentDirection);
            return;
        }
        if (cachedPlayerCell == playersCurrentCell)
        {
            return;
        }
        float currentCellDistance = 1000.0f;
        int closestCellVector = 0;
        for (int i = 0; i < currentRoom.cells.Count; i++)
        {
            float distanceWeight = Vector3.Distance(currentRoom.cells[i].transform.position,
                                                       playersCurrentCell.transform.position);

            if (distanceWeight < currentCellDistance
                && (currentRoom.cells[i].coordinates.x == (currentCell.coordinates.x + 1)
                     || currentRoom.cells[i].coordinates.x == (currentCell.coordinates.x - 1))
                && (currentRoom.cells[i].coordinates.z == (currentCell.coordinates.z + 1)
                     || currentRoom.cells[i].coordinates.z == (currentCell.coordinates.z - 1)))
            {
                currentCellDistance = distanceWeight;
                closestCellVector = i;
            }
        }
        cachedPlayerCell = playersCurrentCell;
        SetLocation(currentRoom.cells[closestCellVector]);
    }

}