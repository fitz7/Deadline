using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	private MazeCell currentCell;

	private MazeDirection currentDirection;

    private bool coroutineRunning;

	public void SetLocation (MazeCell cell) {
		if (currentCell != null) {
            currentCell.cellIsOccupied = false;
			currentCell.OnPlayerExited();
		}
		currentCell = cell;
        currentCell.cellIsOccupied = true;
		transform.localPosition = cell.transform.localPosition;
		currentCell.OnPlayerEntered();
	}

	private void Move (MazeDirection direction) {
		MazeCellEdge edge = currentCell.GetEdge(direction);
        if ( edge is MazePassage && !edge.otherCell.cellIsOccupied )
        {
			SetLocation(edge.otherCell);
		}
	}

	private void Look (MazeDirection direction) {
		transform.localRotation = direction.ToRotation();
		currentDirection = direction;
	}

    private void Update()
    {
        if ( coroutineRunning )
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            Move(currentDirection);
            StartCoroutine( WaitForAITurn( ) );
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            Move(currentDirection.GetNextClockwise());
            StartCoroutine( WaitForAITurn( ) );
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            Move(currentDirection.GetOpposite());
            StartCoroutine( WaitForAITurn( ) );
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Move(currentDirection.GetNextCounterclockwise());
            StartCoroutine( WaitForAITurn( ) );
        }

    }

    private IEnumerator WaitForAITurn( )
    {
        coroutineRunning = true;
        yield return new WaitForSeconds( 0.3f );
        coroutineRunning = false;
        Subject.NotifySendAll( currentCell, OfficeWorker.MOVE_ENEMY, " " );
    }
}