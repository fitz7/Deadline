// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "GameFramework/Actor.h"
#include "CorruptionBlock.generated.h"

/**
 * 
 */
UCLASS()
class DEADLINE_API ACorruptionBlock : public AActor
{
	GENERATED_UCLASS_BODY()	
	UPROPERTY( Category = Block, VisibleDefaultsOnly, BlueprintReadOnly )
	TSubobjectPtr < class USceneComponent > dummyRoot;
	UPROPERTY( Category = Block, VisibleDefaultsOnly, BlueprintReadOnly )
	TSubobjectPtr< class UStaticMeshComponent > blockMesh;
};
