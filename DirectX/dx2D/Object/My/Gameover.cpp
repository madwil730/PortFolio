#include "stdafx.h"
#include "Gameover.h"

Gameover::Gameover(D3DXVECTOR2 position, D3DXVECTOR2 scale){

	animation = new Animation();

	wstring spriteFile = Textures + L"./My/gameover.png";
	wstring shaderFile = Shaders + L"Effect.fx";

	// idle
	Clip* clip;
	{
		clip = new Clip(PlayMode::Loop);

		clip->AddFrame(new Sprite(spriteFile, shaderFile, 0, 0, 800, 600), 0.5f);
		animation->AddClip(clip);
	}


	animation->Position(position);
	animation->Scale(scale);
	animation->Play(0);

}

Gameover::~Gameover()
{
	SafeDelete(animation);
}

void Gameover::Update(D3DXMATRIX&V, D3DXMATRIX&P)
{
	D3DXVECTOR2 position = animation->Position();

	animation->Position(position);

	animation->Play(0);
	animation->Update(V, P);
}

void Gameover::Render()
{
	animation->Render();
}

