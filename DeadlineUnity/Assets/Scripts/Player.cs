using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public int health = 10;

    private int baseDamage = 3;

	private MazeCell currentCell;

	private MazeDirection currentDirection;

	public void SetLocation (MazeCell cell) {
		if (currentCell != null) {
            currentCell.cellIsOccupied = false;
			currentCell.OnPlayerExited();
		}
		currentCell = cell;
        currentCell.cellIsOccupied = true;
		transform.localPosition = cell.transform.localPosition;
		currentCell.OnPlayerEntered();
        if(currentCell.isExit)
            Subject.Notify(GameManager.NEXT_LEVEL);
        
	}

	private void Move (MazeDirection direction) {
		MazeCellEdge edge = currentCell.GetEdge(direction);
        if ( edge is MazePassage )
        {
            if ( edge.otherCell.cellIsOccupied )
            {
                AttackMonster( edge.otherCell );
                return;
            }
			SetLocation(edge.otherCell);
		}
	}

    private void AttackMonster( MazeCell occupiedCell )
    {

    }

	private void Look (MazeDirection direction) {
		transform.localRotation = direction.ToRotation();
		currentDirection = direction;
	}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            Move(currentDirection);
            Subject.NotifySendAll( currentCell, OfficeWorker.MOVE_ENEMY, " " );
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            Move(currentDirection.GetNextClockwise());
            Subject.NotifySendAll( currentCell, OfficeWorker.MOVE_ENEMY, " " );
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            Move(currentDirection.GetOpposite());
            Subject.NotifySendAll( currentCell, OfficeWorker.MOVE_ENEMY, " " );
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Move(currentDirection.GetNextCounterclockwise());
            Subject.NotifySendAll( currentCell, OfficeWorker.MOVE_ENEMY, " " );
        }

    }
}