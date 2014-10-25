// Fill out your copyright notice in the Description page of Project Settings.

#pragma once
#include <vector>
#include "Cell.h"
/**
 * 
 */

class DEADLINE_API Grid

{

	public:
		Grid(int, int);
		~Grid();
		std::vector< std::vector<Cell>> grid;
		void generate();
	private:
		
		int sizeX;
		int sizeY;
		int moveEW(int, int);
		int moveNS(int, int);
		
		bool isGoodMove(int, int, int,int,int, std::vector< std::vector<Cell>>);
		int reverseDirection(int);
		std::vector< std::vector<Cell>> RemoveWalls(int, int, int, int, int, int, std::vector< std::vector<Cell>>);
		std::vector< std::vector<Cell>> AddDoor(int, int, int, int, int, int, std::vector< std::vector<Cell>>);
};
