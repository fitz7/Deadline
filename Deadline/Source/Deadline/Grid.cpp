// Fill out your copyright notice in the Description page of Project Settings.

#include "Deadline.h"
#include "Grid.h"
#include "Cell.cpp"
#include "stack"
#include "ctime"

#define NORTH 0
#define NORTH   0
#define SOUTH   1
#define EAST    2
#define WEST    3

using namespace std;

class Grid{
public:
	Grid(int, int);

private:
	int sizeX;
	int sizeY;
	Generate();
	moveEW();
	moveNS();
};

Grid::Grid(int x, int y)
{
	sizeX = x, sizeY = y;
	grid = new Cell[sizeX][sizeY]();
	generate();

}
Grid::Generate() {
	stack<int> xValues;
	stack<int> yValues;
	int ngGood = 0;
	int locX = 0, locY = 0;
	
	for (int i = 0; i < 4 i++)
	{
	
		//grid[locX][locY].walls[i]
	}
}

int moveEW(int direction, int x){
    if (direction == EAST)
            return x + 1;
    else if (direction == WEST)
            return x - 1;
    else
            return x;
}

int moveNS(int direction, int y){
    if (direction == NORTH)
            return y - 1;
    else if (direction == SOUTH)
            return y + 1;
    else
            return y;
}


Grid::~Grid()
{
	delete[] grid;
}
