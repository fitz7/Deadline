// Fill out your copyright notice in the Description page of Project Settings.

#include "Deadline.h"
#include "Grid.h"
#include "stack"
#include "ctime"
#include <vector>


#define NORTH   0
#define SOUTH   2
#define EAST    1
#define WEST    3

using namespace std;



Grid::Grid(int x, int y)
{
	srand(time(0));
	sizeX = x;
	sizeY = y;
	grid.resize(sizeX);
	for (int i = 0; i < sizeX; i++)
		grid[i].resize(sizeY);
	for (int i = 0; i < sizeX; i++){
		for (int j = 0; j < sizeY; j++){
			grid[i][j].setDefault();

		}
	}
}
void Grid::generate() {
	stack<int> xValues;
	stack<int> yValues;
	int nGood = 0;
	int direction = 0;
	int locX = 1, locY = 1;
	int roomNum = 1;
	do{
		
		for (int i = 0; i < 4; i++)
		{
			if (isGoodMove(locX, locY, sizeX, sizeY, i, grid))
				nGood++;
		}


		if (nGood == 0){
			locX = xValues.top();
			locY = yValues.top();
			xValues.pop();
			yValues.pop();
		}
		else if (nGood >= 1){
			xValues.push(locX);
			yValues.push(locY);

			//int direction = 0;
			do{
				direction = rand() % 4;
			} while (!isGoodMove(locX, locY, sizeX, sizeY, direction, grid));
			//the below code is disgusting.
			//grid[locY][locX].been = true;
			//grid[locY][locX].room = roomNum;
			//int doorchance = rand() % 10;
			int walltype = 0;
			//if (doorchance == 1) {
			//	walltype = 2;
			//	roomNum++;
			//}	  
			grid[locY][locX].removeWall(direction, walltype);
			locX = moveEW(direction, locX);
			locY = moveNS(direction, locY);
			grid[locY][locX].removeWall(reverseDirection(direction), walltype);

		}
		grid[locY][locX].been = true;
		nGood = 0;
	} while (!xValues.empty());
}
int Grid::moveEW(int direction, int x)
{
	if (direction == EAST)
		return x + 1;
	else if (direction == WEST)
		return x - 1;
	else
		return x;
}
int Grid::moveNS(int direction, int y)
{
	if (direction == NORTH)
		return y - 1;
	else if (direction == SOUTH)
		return y + 1;
	else
		return y;
}
bool Grid::isGoodMove(int x, int y, int sizeX, int sizeY, int direction, std::vector< std::vector<Cell>> grid) {
	x = moveEW(direction, x);
	y = moveNS(direction, y);

	if (grid[y][x].been || x >= (sizeX - 1) || x <= 0 || y <= 0 || y >= (sizeY - 1)){
		return false;
	}

	else if (!grid[y][x].been){	  		
			return true;	 		
	}
	
	
	return false;
}

int Grid::reverseDirection(int direction)
{
	if (direction == NORTH)
		return SOUTH;
	if (direction == SOUTH)
		return NORTH;
	if (direction == EAST)
		return WEST;
	if (direction == WEST)
		return EAST;
	
	return 10;
}



Grid::~Grid()
{

}
