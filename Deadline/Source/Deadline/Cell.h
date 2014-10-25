// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

/**
 * 
 */
class DEADLINE_API Cell
{


public:
	Cell(int);
	~Cell();
	int walls[4];
	int room;
private:
	void removeN();
	void removeE();
	void removeS();
	void removeW();
	void setRoom(int);

};
