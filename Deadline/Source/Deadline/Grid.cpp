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

		if (nGood == 1){
			grid[locY][locX].been = true;
			grid[locX][locY].room = roomNum;

			int doorchance = rand() % 10;
			int walltype = 0;
			if (doorchance == 1) {
				walltype = 2;
				roomNum++;
			}

			if (isGoodMove(locX, locY, sizeX, sizeY, NORTH, grid))	{
			locY = moveNS(NORTH, locY);
			grid[locY][locX].removeWall(NORTH, walltype);
			grid[locY][locX].removeWall(reverseDirection(NORTH), walltype);
		}
			else if (isGoodMove(locX, locY, sizeX, sizeY, SOUTH, grid))	  {
				grid[locY][locX].removeWall(SOUTH, walltype);
				locY = moveNS(SOUTH, locY);
				grid[locY][locX].removeWall(reverseDirection(SOUTH), walltype);
			}
			else if (isGoodMove(locX, locY, sizeX, sizeY, EAST, grid))	  {	 	
				grid[locY][locX].removeWall(SOUTH, walltype);
				locX = moveEW(EAST, locX);
				grid[locY][locX].removeWall(reverseDirection(EAST), walltype);
			}
			else if (isGoodMove(locX, locY, sizeX, sizeY, WEST, grid))	  {
				grid[locY][locX].removeWall(WEST, walltype);
				locX = moveEW(WEST, locX);
				grid[locY][locX].removeWall(reverseDirection(WEST), walltype);
			}  				

			
		}
		else if (nGood == 0){
			locX = xValues.top();
			locY = yValues.top();
			xValues.pop();
			yValues.pop();
		}
		else if (nGood > 1){
			xValues.push(locX);
			yValues.push(locY);
			int newdirection = 0;
			do{
				 newdirection = rand() % 4;
			} while (!isGoodMove(locX, locY, sizeX, sizeY, newdirection, grid));
			//the below code is disgusting.
			grid[locY][locX].been = true;
			grid[locX][locY].room = roomNum;
			int doorchance = rand() % 10;
			int walltype = 0;
			if (doorchance == 1) {
				walltype = 2;
				roomNum++;
			}	  
			grid[locY][locX].removeWall(newdirection, walltype);
			locX = moveEW(newdirection, locX);
			locY = moveNS(newdirection, locY);
			grid[locY][locX].removeWall(reverseDirection(newdirection), walltype);

		}
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

	if (x >= (sizeX - 1) || x <= 0 || y <= 0 || y >= (sizeY - 1)){
		return false;
	}
	if (grid[y][x].been){
		return false;
	}
	if (direction == NORTH){
		if (!grid[y][x - 1].been &&  !grid[y - 1][x].been && !grid[y][x + 1].been &&  !grid[y - 1][x - 1].been && !grid[y - 1][x + 1].been){
			return true;
		}
	}
	if (direction == SOUTH){
		if (!grid[y][x - 1].been &&  !grid[y + 1][x].been && !grid[y][x + 1].been && !grid[y + 1][x - 1].been && !grid[y + 1][x + 1].been){
			return true;
		}
	}
	if (direction == EAST){
		if (!grid[y][x + 1].been &&  !grid[y - 1][x].been && !grid[y + 1][x].been && !grid[y - 1][x + 1].been && !grid[y + 1][x + 1].been){
			return true;
		}
	}
	if (direction == WEST){
		if (!grid[y][x - 1].been &&  !grid[y - 1][x].been && !grid[y + 1][x].been && !grid[y - 1][x - 1].been && !grid[y + 1][x - 1].been){
			return true;
		}
	}
	return false;
}

int Grid::reverseDirection(int direction)
{
	if (direction == NORTH)
		return SOUTH;
	else if (direction == SOUTH)
		return NORTH;
	else if (direction == EAST)
		return WEST;
	else if (direction == WEST)
	 return EAST;
	else return 10;

}



Grid::~Grid()
{

}
