// Fill out your copyright notice in the Description page of Project Settings.

#include "Deadline.h"
#include "Grid.h"
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
	Generate();
	moveEW();
	moveNS();
};

Grid::Grid(int x, int y)
{
	grid = new Cell[x][y]();
	generate();

}
Grid::Generate() {
	stack<int> xValues;
	stack<int> yValues;
	int ngGood = 0;
	int locX = 1, locY = 1;
}

Grid::moveEW(int direction, int x)
{

}
Grid::moveNS(int direction, int y)
{

}


Grid::~Grid()
{
	delete[] grid;
}
