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
	size = 7;
	blockCount = size * size;
}

void ACorruptionSpread::BeginPlay( )
{
	Super::BeginPlay( );
	CreateStubRoom( );
	//StartCorruptionSpread( );
	StartCorruptionSpread( );
}

//void ACorruptionSpread::Tick( float deltaTime )
//{
//	Super::Tick( deltaTime );
//	UE_LOG( LogTemp, Warning, TEXT( "Test %f" ), deltaTime );
//}

void ACorruptionSpread::CreateStubRoom( )
{
	const int32 numberOfBlocks = size * size;
	for ( int32 blockIndex = 0; blockIndex < numberOfBlocks; blockIndex++ )
	{
		const float xOffSet = ( blockIndex / size ) * blockSpacing;
		const float yOffSet = ( blockIndex % size ) * blockSpacing;
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
    UE_LOG( LogTemp, Warning, TEXT( "TEST TIMER TEST" ) );

}