// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

/**
 * 
 */
class DEADLINE_API Cell
{


public:
	Cell();
	~Cell();
	void setDefault();
	int walls[4];
	int room;
	bool been;
	void removeWall(int, int);
private:		
	void removeN(int);
	void removeE(int);
	void removeS(int);
	void removeW(int);
	void setRoom(int);

};
