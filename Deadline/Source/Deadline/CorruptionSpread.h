// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "GameFramework/Actor.h"
#include "CorruptionBlock.h"
#include <vector>
#include "CorruptionSpread.generated.h"

/**
 *
 */
UCLASS( )
class DEADLINE_API ACorruptionSpread : public AActor
{
	GENERATED_UCLASS_BODY( )
	UPROPERTY( Category = Grid, VisibleDefaultsOnly, BlueprintReadOnly )
    TSubobjectPtr< class USceneComponent > testRoot;
	UPROPERTY( Category = Grid, EditAnywhere, BlueprintReadOnly )
	float blockSpacing;
	float roomCorruptionCounter;
	int32 blockSize;
	int32 blockCount;
	std::vector<ACorruptionBlock*> roomArray;
	virtual void BeginPlay( ) override;
	void CreateStubRoom( );
	void StartCorruptionSpread( );
	void CorruptionSpread( );
};