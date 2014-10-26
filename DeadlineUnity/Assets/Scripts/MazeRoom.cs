using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class MazeRoom : ScriptableObject {

	public int settingsIndex;

	public MazeRoomSettings settings;
	
	public List<MazeCell> cells = new List<MazeCell>();

    public bool roomIsCorrupted;

	public void Add (MazeCell cell) {
		cell.room = this;
		cells.Add(cell);
	}

	public void Assimilate (MazeRoom room) {
		for (int i = 0; i < room.cells.Count; i++) {
            
			Add(room.cells[i]);
		}
	}

    public int CountCells()
    {
        if (cells != null) return cells.Count;
        else return 0;
    }

    public MazeCell RandomCell()
    {
        return cells[(int)Mathf.Floor(UnityEngine.Random.Range(0, cells.Count))];
    }
    public void Hide () {
		for (int i = 0; i < cells.Count; i++) {
			cells[i].Hide();
		}
	}
	
	public void Show () {
		for (int i = 0; i < cells.Count; i++) {
			cells[i].Show();
		}
	}

    public IEnumerator CorruptRoom( )
    {
        for ( int i = 0; i < cells.Count; i++ )
        {
            //List<MeshRenderer> listOfMeshes = new List<MeshRenderer>( );
            //listOfMeshes = cells[ i ].GetComponentsInChildren<MeshRenderer>( true ).ToList( );
            //listOfMeshes[ 0 ].material = settings.corruptionMaterial;
            GameObject corruption = Instantiate( settings.corruptionBubble ) as GameObject;
            corruption.transform.parent = cells[ i ].transform;
            corruption.transform.position = cells[ i ].transform.position;
            yield return new WaitForSeconds( 0.009f );
        }
        roomIsCorrupted = true;
    }
}