#include "stdafx.h"
#include "Inveder2.h"
#include <ctime>
#include "Beam.h"
#include "Scenes/My/Space.h"

Inveder2::Inveder2(D3DXVECTOR2 position, D3DXVECTOR2 scale)
{
	srand((unsigned)time(0));
	animation = new Animation();

	wstring spriteFile = Textures + L"./My/Enemy.png";
	wstring shaderFile = Shaders + L"Effect.fx";

	this->position = position;

	// idle
	Clip* clip;
	{
		clip = new Clip(PlayMode::Loop);

		clip->AddFrame(new Sprite(spriteFile, shaderFile, 1, 160, 15, 174), 0.5f);
		clip->AddFrame(new Sprite(spriteFile, shaderFile, 16, 160, 31, 174), 0.5f);
		clip->AddFrame(new Sprite(spriteFile, shaderFile, 32, 159, 47, 173), 0.5f);
		clip->AddFrame(new Sprite(spriteFile, shaderFile, 48, 162, 63, 176), 0.5f);

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

Inveder2::~Inveder2()
{
	SafeDelete(animation);
}

Sprite * Inveder2::GetSprite()
{
	return animation->GetSprite();
}

void Inveder2::Update(D3DXMATRIX&V, D3DXMATRIX&P)
{
	for (bool val : Hit) {


		if(val) HitCount += 1;

		if (HitCount == 2) {

			Space::score += 300;
			state2 = State2::Attacked;
			timecheck = true;

			HitCount = 0;

		}

	}

	animation->Position(position);
	animation->Play((UINT)state2);
	animation->Update(V, P);


}

void Inveder2::Render()
{
	animation->Render();
	
	//ImGui::LabelText("Inveder2Time", "%f", Itime);
	//ImGui::LabelText("Inveder2Hit", "%d", HitCount);
}


void Inveder2::Moveleft()
{
	
	int tempx = rand() % 200 + 400;
	int tempy = rand() % 601 - 300;


	position.x -= movespeed  * Time::Delta();

	if (timecheck) {

		Itime += Time::Delta();

		if (Itime > 0.5) {

			position.x = rand() % 200 + 400;
			Itime = 0;
			state2 = State2::Idle;
			timecheck = false;
		}
	}


	if (position.x < -410) {
		position.x = tempx;
		position.y = tempy;
	}
}

