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

    public Material[] good;
    public Material[] bad;
    public const string MOVE_ENEMY = "MOVE_ENEMY";
    private MazeDirection currentDirection;
    private WORKER_MOVEMENT workerMovement = WORKER_MOVEMENT.CLOCKWISE;
    private MazeCell currentCell;
    private MazeCell playersCurrentCell;
    private MazeCell cachedPlayerCell;
    private MazeRoom currentRoom;
    private bool workerIsCorrupted;
    private EnemyType enemyType;
    private int health;
    private int damage;

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
            enemyType = EnemyType.Male;
            Material[] male = new Material[1];
            male[0] = good[0];
            transform.GetChild(0).renderer.materials = male;
        }
        else if (selection > 5 && selection < 9)
        {
            enemyType = EnemyType.Female;
            Material[] female = new Material[1];
            female[0] = good[1];
            transform.GetChild(0).renderer.materials = female;
        }
        else if (selection == 9)
        {
            enemyType = EnemyType.Janitor;
            Material[] janitor = new Material[1];
            janitor[0] = good[2];
            transform.GetChild(0).renderer.materials = janitor;
        }
    }

    private void ChangeEnemySprite()
    {
        if (enemyType == EnemyType.Male)
        {
            Material[] male = new Material[1];
            male[0] = bad[0];
            transform.GetChild(0).renderer.materials = male;
        }
        else if (enemyType == EnemyType.Female)
        {
            Material[] Female = new Material[1];
            Female[0] = bad[1];
            transform.GetChild(0).renderer.materials = Female;
        }
        else if (enemyType == EnemyType.Janitor)
        {
            Material[] Janitor = new Material[1];
            Janitor[0] = bad[2];
            transform.GetChild(0).renderer.materials = Janitor;
        }
    }

    public override void OnNotify(Object sender, EventArguments e)
    {
        if (e.eventMessage == MOVE_ENEMY)
        {
            playersCurrentCell = (MazeCell)sender;
            SearchForPlayer();
        }
    }

    public void SetInitialLocation(MazeCell cell)
    {
        currentRoom = cell.room;
        SetLocation(cell);
    }

    private void SetLocation(MazeCell cell)
    {
        if (currentCell != null)
        {
            currentCell.cellIsOccupied = false;
            currentCell.currentMonsterOnCell = null;
        }
        currentCell = cell;
        currentCell.currentMonsterOnCell = this.gameObject;
        IsCellCorrupted();
        this.transform.position = currentCell.transform.position;
    }

    private void IsCellCorrupted()
    {
        if (currentCell.cellIsCorrupted && !workerIsCorrupted)
        {
            Debug.Log( "MONSTER CORRUPTED" );
            ChangeEnemySprite();
            workerIsCorrupted = true;
        }
        if (workerIsCorrupted)
        {
            currentCell.cellIsOccupied = true;
        }
    }

    private void SearchForPlayer()
    {
        if (playersCurrentCell.room != currentRoom || !workerIsCorrupted)
        {
            Move(currentDirection);
            return;
        }
        //if ( cachedPlayerCell == playersCurrentCell )
        //{
        //    return;
        //}
        float currentCellDistance = 1000.0f;
        int closestCellVector = 0;
        for (int i = 0; i < currentRoom.cells.Count; i++)
        {
            float distanceWeight = Vector3.Distance(currentRoom.cells[i].transform.position,
                                                       playersCurrentCell.transform.position);
            if (distanceWeight < currentCellDistance
                && !currentRoom.cells[i].cellIsOccupied
                //Can the AI Move Vertically
                && ((currentRoom.cells[i].coordinates.x == (currentCell.coordinates.x)
                       && (currentRoom.cells[i].coordinates.z == (currentCell.coordinates.z + 1)
                       || currentRoom.cells[i].coordinates.z == (currentCell.coordinates.z - 1)))
                //Can the AI Move Horizontally
                   || (currentRoom.cells[i].coordinates.z == (currentCell.coordinates.z)
                        && (currentRoom.cells[i].coordinates.x == (currentCell.coordinates.x + 1)
                        || currentRoom.cells[i].coordinates.x == (currentCell.coordinates.x - 1))))
                )
            {
                currentCellDistance = distanceWeight;
                closestCellVector = i;
            }
        }
        cachedPlayerCell = playersCurrentCell;
        SetLocation(currentRoom.cells[closestCellVector]);
    }
    private void Move(MazeDirection direction)
    {
        MazeCellEdge edge = currentCell.GetEdge(direction);
        if (edge is MazePassage
             && edge.otherCell.room == currentRoom
             && !edge.otherCell.cellIsOccupied)
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

    public void AttackEnemy(int damage)
    {
        health = health - damage;
        if (health < 0)
        {
            Destroy(this.gameObject);
        }
    }
}