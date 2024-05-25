#include "stdafx.h"
#include "Inveder.h"
#include <ctime>
#include "Beam.h"
#include "Scenes/My/Space.h"

Inveder::Inveder(D3DXVECTOR2 position, D3DXVECTOR2 scale)
{
	srand((unsigned)time(0));
	animation = new Animation();

	wstring spriteFile = Textures + L"./My/Enemy.png";
	wstring shaderFile = Shaders + L"Effect.fx";

	//this->position = position;

	// idle
	Clip* clip;
	{
		clip = new Clip(PlayMode::Loop);

		clip->AddFrame(new Sprite(spriteFile, shaderFile, 1, 2, 15, 14), 0.5f);
		clip->AddFrame(new Sprite(spriteFile, shaderFile, 17, 2, 31, 14), 0.5f);
		clip->AddFrame(new Sprite(spriteFile, shaderFile, 33, 2, 47, 14), 0.5f);

		animation->AddClip(clip);
	}

	//be attacked
	{
		clip = new Clip(PlayMode::End);

		clip->AddFrame(new Sprite(spriteFile, shaderFile, 1, 549, 14, 559), 0.1f);
		clip->AddFrame(new Sprite(spriteFile, shaderFile, 16, 548, 30, 559), 0.1f);
		clip->AddFrame(new Sprite(spriteFile, shaderFile, 31, 547, 46, 559), 0.1f);
		clip->AddFrame(new Sprite(spriteFile, shaderFile, 47, 545, 63, 559), 0.1f);

		animation->AddClip(clip);
	}

	animation->Position(position);
	animation->Scale(scale);
	animation->Play(0);

}

Inveder::~Inveder()
{
	SafeDelete(animation);
}

Sprite * Inveder::GetSprite()
{
	return animation->GetSprite();
}

void Inveder::Update(D3DXMATRIX&V, D3DXMATRIX&P)
{
	for (bool val : Hit) {

		if (val) {
			
			scorecheck = true;
			if(scorecheck)Space::score += 100;
			state = State::Attacked;
			timecheck = true;
			

		}

	}

	animation->Position(position);

	animation->Play((UINT)GetState());
	animation->Update(V, P);

	scorecheck = false;

}

void Inveder::Render()
{
	animation->Render();
	
	//ImGui::LabelText("InvederTime", "%f", Itime);
	//ImGui::LabelText("InvederHit", "%d", Hit);
}


void Inveder::Moveleft()
{
	
	position = animation->Position();

	int tempx = rand() % 200 + 400;
	int tempy = rand() % 601 - 300;


	position.x -= GetMoveSpeed() * Time::Delta();

	if (timecheck) {

		Itime += Time::Delta();

		if (Itime > 0.5) {

			position.x = rand() % 200 + 400;
			Itime = 0;
			state = State::Idle;
			timecheck = false;
		}
	}


	if (position.x < -410) {
		position.x = tempx;
		position.y = tempy;
	}
}

