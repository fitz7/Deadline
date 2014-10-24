// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "GameFramework/Actor.h"
#include "CorruptionSpread.generated.h"

/**
 * 
 */
UCLASS()
class DEADLINE_API ACorruptionSpread : public AActor
{
	GENERATED_UCLASS_BODY()
	UPROPERTY( Category=Grid, VisibleDefaultsOnly, BlueprintReadOnly )
	TSubobjectPtr< class USceneComponent > testRoot;
	UPROPERTY( Category=Grid, EditAnywhere, BlueprintReadOnly )
	float blockSpacing;
	int32 size;
	virtual void BeginPlay( ) override;
};
