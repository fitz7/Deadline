// Fill out your copyright notice in the Description page of Project Settings.

#include "Deadline.h"
#include "floor.h"
#include "Grid.h"



Afloor::Afloor(const class FPostConstructInitializeProperties& PCIP)
	: Super(PCIP)
{


}
void Afloor::BeginPlay()
{
	Super::BeginPlay();
	start();
}
void Afloor::start()
{
	static const int X = 10;
	static const int Y = 10;

	Grid* g = new Grid(X, Y);
	g->generate();
	FString line;
	for (int i = 0; i < X; i++){
		for (int j = 0; j < Y; j++)
		{
			int thisroom = g->grid[i][j].room;
			line += FString::FromInt(thisroom);
		}
		UE_LOG(LogClass, Log, TEXT("%s"), line);
		line = "";
	}


}


