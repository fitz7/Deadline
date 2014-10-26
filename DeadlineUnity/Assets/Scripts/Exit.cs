using UnityEngine;
using System.Collections;


public class Exit : UnityObserver {

    
    private MazeCell currentCell;
    private MazeRoom currentRoom;



    public void SetInitialLocation( MazeCell cell )
    {
        currentRoom = cell.room;
        SetLocation( cell );
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