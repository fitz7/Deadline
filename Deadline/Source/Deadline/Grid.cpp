// Fill out your copyright notice in the Description page of Project Settings.

#include "Deadline.h"
#include "Grid.h"
#include "stack"
#include "ctime"
#include <vector>
#include "Cell.h"
#define NORTH 0
#define NORTH   0
#define SOUTH   1
#define EAST    2
#define WEST    3

using namespace std;



Grid::Grid(int x, int y)
{
	grid.resize(x);
	for (int i = 0; i < 10; i++)
		grid[i].resize(y);
}
void Grid::generate() {
	stack<int> xValues;
	stack<int> yValues;
	int ngGood = 0;
	int locX = 0, locY = 0;
	
	//for (int i = 0; i < 4 i++)
	//{
	//	grid[locX][locY].
	//}
}

void Grid::moveEW(int direction, int x)
{

}
void Grid::moveNS(int direction, int y)
{

}


Grid::~Grid()
{

}
