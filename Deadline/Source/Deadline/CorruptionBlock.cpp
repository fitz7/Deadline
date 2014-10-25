// Fill out your copyright notice in the Description page of Project Settings.

#include "Deadline.h"
#include "CorruptionBlock.h"


ACorruptionBlock::ACorruptionBlock(const class FPostConstructInitializeProperties& PCIP)
	: Super(PCIP)
{
	struct FConstructorStatics
	{
		ConstructorHelpers::FObjectFinderOptional<UStaticMesh> blockMesh;
		ConstructorHelpers::FObjectFinderOptional<UMaterialInstance> blueMaterial;
		ConstructorHelpers::FObjectFinderOptional< UMaterialInstance > orangeMaterial;
		FConstructorStatics( )
			: blockMesh( TEXT( "/Game/Meshes/TemplateCube_Rounded.TemplateCube_Rounded" ) ),
			blueMaterial( TEXT( "/Engine/TemplateResources/MI_Template_BaseBlue.MI_Template_BaseBlue" ) ),
			orangeMaterial( TEXT( "/Engine/TemplateResources/MI_Template_BaseOrange.MI_Template_BaseOrange" ) )
		{
		}
	};
	static FConstructorStatics constructorStatics;

	dummyRoot = PCIP.CreateDefaultSubobject< USceneComponent >( this, TEXT( "Dummy0" ) );
	RootComponent = dummyRoot;
	blockMesh = PCIP.CreateDefaultSubobject< UStaticMeshComponent >( this, TEXT( "BlockMesh0" ) );
	blockMesh->SetStaticMesh( constructorStatics.blockMesh.Get( ) );
	blockMesh->SetRelativeScale3D( FVector( 1.0f, 1.0f, 0.25f ) );
	blockMesh->SetRelativeLocation( FVector( 0.0f, 0.0f, 25.0f ) );
	blockMesh->SetMaterial( 0, constructorStatics.blueMaterial.Get( ) );
	blockMesh->AttachTo( dummyRoot );
}