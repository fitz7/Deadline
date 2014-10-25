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
	private:
		void generate();
		int sizeX;
		int sizeY;
		int moveEW(int, int);
		int moveNS(int, int);
		std::vector< std::vector<Cell>> grid;
		bool isGoodMove(int, int, int,int,int, std::vector< std::vector<Cell>>);

};
