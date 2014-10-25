#include "Deadline.h"
#include "Cell.h"


Cell::Cell(int roomNum)
{	 //(N,E,S,W)
	int walls[] = { 1, 1, 1, 1 };
	int room = 0;
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
void Cell::removeN()
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

