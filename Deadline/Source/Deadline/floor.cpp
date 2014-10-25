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

	Grid* g = new Grid(Y, X);
	g->generate();
	FString line;
	for (int i = 1; i <= Y-1; i++){
		for (int j = 1; j <= Y-1; j++)
		{
			int thisroom = g->grid[j][i].room;
			line += FString::FromInt(thisroom);
		}
		//UE_LOG(LogTemp, Log, TEXT("%s"), line);
		line = "";
	}


}


