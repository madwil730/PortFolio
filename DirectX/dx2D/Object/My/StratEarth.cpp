#include "stdafx.h"
#include "StartEarth.h"
#include <ctime>



StartEarth::StartEarth(D3DXVECTOR2 position, D3DXVECTOR2 scale)
{
	srand((unsigned)time(0));
	animation = new Animation();

	wstring spriteFile = Textures + L"./My/Back_Earth.png";
	wstring shaderFile = Shaders + L"Effect.fx";

	this->position = position;

	// idle
	Clip* clip;
	{
		clip = new Clip(PlayMode::Loop);

		clip->AddFrame(new Sprite(spriteFile, shaderFile, 0, 0, 180, 125), 0.5f);

		animation->AddClip(clip);
	}


	animation->Position(position);
	animation->Scale(scale);
	animation->Play(0);



}

StartEarth::~StartEarth()
{
	SafeDelete(animation);
}

void StartEarth::Update(D3DXMATRIX&V, D3DXMATRIX&P)
{


	animation->Position(position);

	animation->Play(0);
	animation->Update(V, P);

}

void StartEarth::Render()
{
	animation->Render();

}

