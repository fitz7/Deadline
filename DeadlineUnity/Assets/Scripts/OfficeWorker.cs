using UnityEngine;
using System.Collections;

public class OfficeWorker : UnityObserver {

    public const string MOVE_ENEMY = "MOVE_ENEMY";
    private MazeCell currentCell;
    private MazeRoom currentRoom;

    public override void OnNotify( Object sender, EventArguments e )
    {
        if ( e.eventMessage == MOVE_ENEMY )
        {
            
        }
    }

    public void SetInitialLocation( MazeCell cell )
    {
        currentRoom = cell.room;
        SetLocation( cell );
    }

    private void Move( MazeDirection direction )
    {
        MazeCellEdge edge = currentCell.GetEdge( direction );
        if ( edge is MazePassage )
        {
            SetLocation( edge.otherCell );
        }
    }

    private void SetLocation( MazeCell cell )
    {
        if ( currentCell != null )
        {
            currentCell.OnPlayerExited( );
        }
        currentCell = cell;
        transform.localPosition = cell.transform.localPosition;
    }
}