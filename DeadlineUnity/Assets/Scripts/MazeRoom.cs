using System;
using UnityEngine;
using System.Collections.Generic;

public class MazeRoom : ScriptableObject {

	public int settingsIndex;

	public MazeRoomSettings settings;
	
	private List<MazeCell> cells = new List<MazeCell>();

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
        return cells[Math.Floor(Random.Range(0, cells.Count))];
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
}