#include "stdafx.h"
#include "Beam.h"

Beam::Beam(D3DXVECTOR2 position, D3DXVECTOR2 scale)
{
	animation = new Animation();

	wstring spriteFile = Textures + L"./My/Bullet.png";
	wstring shaderFile = Shaders + L"Effect.fx";

	Clip* clip = new Clip();

	{
		clip = new Clip(PlayMode::End);

		clip->AddFrame(new Sprite(spriteFile, shaderFile, 38, 173, 53, 198), 0.1f);

		animation->AddClip(clip);
	}

	animation->Position(position);
	animation->Scale(scale);
	animation->Play(0);

	Position(position);
}

Beam::~Beam()
{
	SafeDelete(animation);
}

Sprite * Beam::GetSprite()
{
	return animation->GetSprite();
}

void Beam::Update(D3DXMATRIX & V, D3DXMATRIX & P)
{
	
	animation->RotationDegree(0, 0, 90);
	animation->Play(0);
	animation->Position(position);
	animation->Update(V, P);
}

void Beam::Render()
{
	animation->Render();
}

void Beam::MoveRight()
{
	position.x += 500.0f * Time::Delta();

	for (bool val : Hit) {

		if(val) Position(D3DXVECTOR2(1000, 1000));
	}

	for (bool val2 : Hit2) {

		if (val2) Position(D3DXVECTOR2(1000, 1000));
	}

	for (bool val3 : Hit3) {

		if (val3) Position(D3DXVECTOR2(1000, 1000));
	}

	if (position.x > 400 )
		Position(D3DXVECTOR2(1000, 1000));

}

