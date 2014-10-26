using UnityEngine;
using System.Collections;

public class CorruptionSpread : MonoBehaviour
{
    public int gridSize;
    private Vector3 blockSize = new Vector3( 1,1,1 );
    private Vector3 movementDirection = new Vector3( 0,0,0 );

    void Start( )
    {
        Create2DGrid( );
    }

    private void Create2DGrid( )
    {
        for ( int i = 0; i < gridSize; i++ )
        {
            Vector3 newGridPosition = transform.position + movementDirection;
            newGridPosition = new Vector3( Mathf.Round( newGridPosition.x / blockSize.x ),
                                           Mathf.Round( newGridPosition.y / blockSize.y ),
                                           Mathf.Round( newGridPosition.z / blockSize.z ) );
            transform.position = newGridPosition;
        }
    }
}