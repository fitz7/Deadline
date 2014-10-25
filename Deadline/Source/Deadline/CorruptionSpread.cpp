// Fill out your copyright notice in the Description page of Project Settings.

#include "Deadline.h"
#include "CorruptionSpread.h"
#include "CorruptionBlock.h"

ACorruptionSpread::ACorruptionSpread( const class FPostConstructInitializeProperties& PCIP )
	: Super( PCIP )
{
	PrimaryActorTick.bCanEverTick = true;
	testRoot = PCIP.CreateDefaultSubobject< USceneComponent >( this, TEXT( "Dummy0" ) );
	RootComponent = testRoot;
	blockSpacing = 300.0f;
	roomCorruptionCounter = 0;
	blockSize = 8;
	blockCount = blockSize * blockSize;
}

void ACorruptionSpread::BeginPlay( )
{
	Super::BeginPlay( );
	CreateStubRoom( );
	StartCorruptionSpread( );
}

void ACorruptionSpread::CreateStubRoom( )
{
	for ( int32 blockIndex = 0; blockIndex < blockCount; blockIndex++ )
	{
		const float xOffSet = ( blockIndex / blockSize ) * blockSpacing;
		const float yOffSet = ( blockIndex % blockSize ) * blockSpacing;
		const FVector blockLocation = FVector( xOffSet, yOffSet, 0.0f ) + GetActorLocation( );
		ACorruptionBlock* newBlock = GetWorld( )->SpawnActor< ACorruptionBlock >( blockLocation, FRotator( 0, 0, 0 ) );
		roomArray.push_back( newBlock );
	}
}

void ACorruptionSpread::StartCorruptionSpread( )
{
	GetWorldTimerManager( ).SetTimer( this, &ACorruptionSpread::CorruptionSpread, 1.0f, true );
}

void ACorruptionSpread::CorruptionSpread( )
{
	int randomSpread = FMath::FloorToInt( FMath::FRandRange( 0, blockCount ) );
	
	UE_LOG( LogTemp, Warning, TEXT( "Test %d" ), randomSpread );

	if ( !roomArray[ randomSpread ]->blockCorrupted )
	{
		roomArray[ randomSpread ]->ChangeMeshToOrange( );
		roomCorruptionCounter++;
		return;
	}
	//If room is completey corrupted then end corruption spread for current room
	if ( roomCorruptionCounter >= blockCount )
	{
		GetWorldTimerManager( ).ClearTimer( this, &ACorruptionSpread::CorruptionSpread );
		return;
	}
	CorruptionSpread( );
}