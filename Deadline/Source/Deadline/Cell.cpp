#include "Deadline.h"
#include "Cell.h"


Cell::Cell()
{	 //(N,E,S,W)
	int walls[] = { 1, 1, 1, 1 };
	int room = 0;
	bool been = false;
}

void Cell::removeN()
{
	walls[0] = 0;
}
void Cell::removeE()
{
	walls[1] = 0;
}
void Cell::removeS()
{
	walls[2] = 0;
}
void Cell::removeW()
{
	walls[3] = 0;
}
void Cell::setRoom(int roomNum)
{
	room = roomNum;
}
Cell::~Cell()
{
}

