#include "stdafx.h"
#include "Ship.h"


bool Ship::death = false;

Ship::Ship(D3DXVECTOR2 position, D3DXVECTOR2 Scale)
{
	animation = new Animation();

	wstring spriteFile = Textures + L"./My/Ship.png";
	wstring shaderFile = Shaders + L"Effect.fx";

	Clip* clip;

	this->position = position;
	
	// forward
	{
		clip = new Clip(PlayMode::Loop);

		clip->AddFrame(new Sprite(spriteFile, shaderFile, 4, 93, 60, 129), 0.1f);
		
		animation->AddClip(clip);
	}

	// up
	{
		clip = new Clip(PlayMode::End);

		clip->AddFrame(new Sprite(spriteFile, shaderFile, 4, 93, 60, 129), 0.15f);
		clip->AddFrame(new Sprite(spriteFile, shaderFile, 198, 100, 252, 130), 0.15f);
		clip->AddFrame(new Sprite(spriteFile, shaderFile, 260, 98, 314, 128), 0.15f);

		animation->AddClip(clip);
	}

	// down
	{
		clip = new Clip(PlayMode::End);

		clip->AddFrame(new Sprite(spriteFile, shaderFile, 4, 93, 60, 129), 0.15f);
		clip->AddFrame(new Sprite(spriteFile, shaderFile, 69, 93, 124, 126), 0.15f);
		clip->AddFrame(new Sprite(spriteFile, shaderFile, 132, 91, 189, 129), 0.15f);

		animation->AddClip(clip);
	}

	//destroy
	{
		clip = new Clip(PlayMode::End);

		clip->AddFrame(new Sprite(spriteFile, shaderFile, 13, 177, 51, 191), 0.3f);
		clip->AddFrame(new Sprite(spriteFile, shaderFile, 70, 169, 119, 191), 0.3f);
		clip->AddFrame(new Sprite(spriteFile, shaderFile, 133, 161, 185, 191), 0.3f);
		clip->AddFrame(new Sprite(spriteFile, shaderFile, 197, 149, 250, 188), 0.3f);
		clip->AddFrame(new Sprite(spriteFile, shaderFile, 1, 1, 1, 1), 0.5f);

		animation->AddClip(clip);
	}

	// flash
	{
		clip = new Clip(PlayMode::Loop);

		clip->AddFrame(new Sprite(spriteFile, shaderFile, 4, 93, 60, 129), 1.0f);
		clip->AddFrame(new Sprite(spriteFile, shaderFile, 0, 0, 1, 1), 1.0f);

		animation->AddClip(clip);
	}

	animation->Position(position);
	
	animation->Scale(Scale);
	animation->Play((UINT)direction);

}

Ship::~Ship()
{
	SafeDelete(animation);
	
}

Sprite * Ship::GetSprite()
{
	return animation->GetSprite();
}


void Ship::Update(D3DXMATRIX & V, D3DXMATRIX & P)
{

	animation->Play((UINT)direction);
	animation->Position(position);
	animation->Update(V, P);

}

void Ship::Render()
{

	animation->Render();
}


void Ship::Play()
{
	for (bool val : Hit) {

		if (val) {

			direction = Direction::destroy;
			hitcheck = true;
			death = true;
		}
	}

	for (bool val : Hit2) {

		if (val) {

			direction = Direction::destroy;
			hitcheck = true;
			death = true;
		}
	}

	for (bool val : Hit3) {

		if (val) {

			direction = Direction::destroy;
			hitcheck = true;
			death = true;
		}
	}

	for (bool val : Hit4) {

		if (val) {

			direction = Direction::destroy;
			hitcheck = true;
			death = true;
		}
	}

	

	if (hitcheck) return;

	position = animation->Position();

	if (Key->Press('W') ) {

		position.y += 300.0f * Time::Delta();
		direction = Direction::up;
		animation->RotationDegree(0, 0, 0);

	}

	else if (Key->Press('S')) {

		position.y -= 300.0f * Time::Delta();
		direction = Direction::down;
		animation->RotationDegree(0, 0, 0);
	}

	if (Key->Press('A')) {

		position.x -= 300.0f * Time::Delta();
		direction = Direction::forward;
		animation->RotationDegree(0, 0, 0);
	}

	else if (Key->Press('D')) {

		position.x += 300.0f * Time::Delta();
		direction = Direction::forward;
		animation->RotationDegree(0, 0, 0);
	}
}
