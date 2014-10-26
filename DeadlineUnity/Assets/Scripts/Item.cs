using UnityEngine;
using System.Collections;

enum ItemType
{
    Health,
}
public class Item : UnityObserver {

    public const string PICK_UP_ITEM = "PICK_UP_ITEM";
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