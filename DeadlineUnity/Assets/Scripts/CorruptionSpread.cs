using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CorruptionSpread : UnityObserver
{
    private MazeCell playersCurrentCell;
    private bool coroutineRunning;

    public override void OnNotify( Object sender, EventArguments e )
    {
        if ( e.eventMessage == OfficeWorker.MOVE_ENEMY )
        {
            if ( coroutineRunning )
            {
                return;
            }
            playersCurrentCell = ( MazeCell )sender;
            StartCoroutine( CorruptRoom( ) );
        }
    }

    private IEnumerator CorruptRoom( )
    {
        if ( !playersCurrentCell.room.roomIsCorrupted )
        {
            coroutineRunning = true;
            yield return StartCoroutine( playersCurrentCell.room.CorruptRoom( ) );
            coroutineRunning = false;
        }
    }
}