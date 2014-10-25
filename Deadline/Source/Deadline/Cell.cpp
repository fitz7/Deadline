#include "Deadline.h"
#include "Cell.h"

#define NORTH   0
#define EAST   1
#define SOUTH    2
#define WEST   3

Cell::Cell()
{	 //(N,E,S,W)
	int walls[] = { 1, 1, 1, 1 };
	int room = 0;
	bool been = false;
}

void Cell::removeWall(int wall, int ID)
{
	walls[wall] = ID;
}

void Cell::setRoom(int roomNum)
{
	room = roomNum;
}
Cell::~Cell()
{
}

