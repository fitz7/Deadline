// Fill out your copyright notice in the Description page of Project Settings.

#include "Deadline.h"
#include "floor.h"
#include "Grid.h"



Afloor::Afloor(const class FPostConstructInitializeProperties& PCIP)
	: Super(PCIP)
{
	//Super::BeginPlay();
	Grid* g = new Grid(10,10);
	
}


