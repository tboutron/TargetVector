// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "CoreMinimal.h"
#include "GameUIManagerSubsystem.h"
#include "TVGameUIManagerSubsystem.generated.h"

/**
 * 

UCLASS()
class EOSLEARNING_API UTVGameUIManagerSubsystem : public UGameUIManagerSubsystem
{
	GENERATED_BODY()
	
};

 */

UCLASS()
class EOSLEARNING_API UTVGameUIManagerSubsystem : public UGameUIManagerSubsystem
{
	GENERATED_BODY()

public:

	UTVGameUIManagerSubsystem();

	virtual void Initialize(FSubsystemCollectionBase& Collection) override;
	virtual void Deinitialize() override;

private:
	bool Tick(float DeltaTime);
	void SyncRootLayoutVisibilityToShowHUD();

	FTSTicker::FDelegateHandle TickHandle;
};
