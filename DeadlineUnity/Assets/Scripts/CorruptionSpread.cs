using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CorruptionSpread : MonoBehaviour
{
    private List<MazeRoom> currentRooms = new List<MazeRoom>( );

    public void StartCorruption( List< MazeRoom > roomList ){
        currentRooms = roomList;
        //for ( int i = 0; i < currentRooms.Count; i++ )
        //{
        //    currentRooms[ i ].CorruptRoom( );
        //}
        StartCoroutine( currentRooms[ 1 ].CorruptRoom( ) );
    }
}