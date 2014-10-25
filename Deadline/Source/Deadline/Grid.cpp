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
			//messy as hell should be in a function
			int newlocX = locX;
			int newlocY = locY;
			if (isGoodMove(locX, locY, sizeX, sizeY, NORTH, grid))	{
				int newlocY = moveNS(NORTH, locY);
				int newlocX = locX;
			}
			else if (isGoodMove(locX, locY, sizeX, sizeY, SOUTH, grid))	  {
				int newlocY = moveNS(SOUTH, locY);
				int newlocX = locX;
			}
			else if (isGoodMove(locX, locY, sizeX, sizeY, EAST, grid))		{
				int newlocX = moveEW(EAST, locX);
				int newlocY = locY;
			}
			else if (isGoodMove(locX, locY, sizeX, sizeY, WEST, grid))	{
				int newlocX = moveEW(WEST, locX);
				int newlocY = locY;
			}
			int doorchance = rand() % 10;
			if (doorchance == 1) {
			AddDoor(direction, locX, locY, newlocX, newlocY, roomNum, grid);
				roomNum++;
			}
			else{
				RemoveWalls(direction, locX, locY, newlocX, newlocY, roomNum, grid);
			}
			locX = newlocX;
			locY = newlocY;
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
				direction = rand() % 4;
			} while (!isGoodMove(locX, locY, sizeX, sizeY, direction, grid));
			//the below code is disgusting.

			int newlocX = moveEW(newdirection, locX);
			int newlocY = moveNS(newdirection, locY);
			int doorchance = rand() % 10;
			if (doorchance == 1) {
				AddDoor(direction, locX, locY, newlocX, newlocY, roomNum, grid);
				roomNum++;
			}
			else{
				RemoveWalls(direction, locX, locY, newlocX, newlocY, roomNum, grid);
			}
			locX = newlocX;
			locY = newlocY;	  
		}
		nGood = 0;
	} while (!xValues.empty());
}

void Grid::RemoveWalls(int direction, int currentX, int currentY, int nextX, int nextY, int roomNum, std::vector< std::vector<Cell>> grid)
{
	grid[currentY][currentX].been = true;
	grid[currentY][currentX].room = roomNum;
	grid[currentY][currentX].removeWall(direction, 0);
	grid[nextY][nextX].removeWall(reverseDirection(direction), 0);
}
void Grid::AddDoor(int direction, int currentX, int currentY, int nextX, int nextY, int roomNum, std::vector< std::vector<Cell>> grid)
{
	grid[currentY][currentX].been = true;
	grid[currentY][currentX].room = roomNum;
	grid[currentY][currentX].removeWall(direction, 2);
	grid[nextY][nextX].removeWall(reverseDirection(direction), 2);

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
