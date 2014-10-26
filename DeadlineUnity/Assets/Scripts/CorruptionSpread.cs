using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CorruptionSpread : MonoBehaviour
{
    private List<MazeRoom> currentRooms = new List<MazeRoom>( );

    public void StartCorruption( List< MazeRoom > roomList ){
        currentRooms = roomList;
       // StartCoroutine( CorruptRoom( ) );
    }

    private IEnumerator CorruptRoom( )
    {
        for ( int corruptedRooms = 0; corruptedRooms < currentRooms.Count; )
        {
            int roomCorruptionSpread = Random.Range( 0, currentRooms.Count );
            if ( !currentRooms[ roomCorruptionSpread ].roomIsCorrupted )
            {
                yield return StartCoroutine( currentRooms[ roomCorruptionSpread ].CorruptRoom( ) );
                corruptedRooms++;
            }
            if ( corruptedRooms >= currentRooms.Count )
            {
                StopAllCoroutines( );
            }
        }
    }
}