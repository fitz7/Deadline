// Fill out your copyright notice in the Description page of Project Settings.

#include "Deadline.h"
#include "Grid.h"
#include "stack"
#include "ctime"
#include <vector>


#define NORTH   0
#define SOUTH   1
#define EAST    2
#define WEST    3

using namespace std;



Grid::Grid(int x, int y)
{
	sizeX = x;
	sizeY = y;
	grid.resize(sizeX);
	for (int i = 0; i < sizeX; i++)
		grid[i].resize(sizeY);
}
void Grid::generate() {
	stack<int> xValues;
	stack<int> yValues;
	int nGood = 0;
	int direction = 0;
	int locX = 0, locY = 0;
	int roomNum = 1;
	do{
		grid[locY][locX].been = true;
		grid[locX][locY].room = roomNum;
		for (int i = 0; i < 4; i++)
		{
			if (isGoodMove(locX, locY,sizeX,sizeY, i, grid))
				nGood++;
		}

		if (nGood == 1){
			if (isGoodMove(locX, locY, sizeX, sizeY, NORTH, grid))
				locY = moveNS(NORTH, locY);

			else if (isGoodMove(locX, locY, sizeX, sizeY, SOUTH, grid))
				locY = moveNS(SOUTH, locY);
			else if (isGoodMove(locX, locY, sizeX, sizeY, EAST, grid))
				locX = moveEW(EAST, locX);
			else if (isGoodMove(locX, locY, sizeX, sizeY, WEST, grid))
				locX = moveEW(WEST, locX);
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
			do{
				direction = rand() % 4;
			} while (!isGoodMove(locX, locY, sizeX, sizeY, direction, grid));
			//the below code is disgusting.
			int doorchance = rand() % 10;
			int walltype = 0;
			if (doorchance == 1) {
				walltype = 2;
				roomNum++;
			}
				
			grid[locY][locX].removeWall(direction, walltype);
			locX = moveEW(direction, locX);
			locY = moveNS(direction, locY);
			grid[locY][locX].removeWall(reverseDirection(direction), walltype);

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
	
	if (grid[y][x].been || x >= (sizeX - 1) || x <= 0 || y <= 0 || y >= (sizeY - 1)){
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
	else return EAST;
	
}



Grid::~Grid()
{

}
