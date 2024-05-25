#include "stdafx.h"
#include "Inveder3.h"
#include <ctime>
#include "Beam.h"
#include "Scenes/My/Space.h"

Inveder3::Inveder3(D3DXVECTOR2 position, D3DXVECTOR2 scale)
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

		clip->AddFrame(new Sprite(spriteFile, shaderFile, 0, 175, 15, 191), 0.5f);
		clip->AddFrame(new Sprite(spriteFile, shaderFile, 16, 174, 31, 189), 0.5f);
		clip->AddFrame(new Sprite(spriteFile, shaderFile, 33, 175, 48, 191), 0.5f);
		clip->AddFrame(new Sprite(spriteFile, shaderFile, 48, 176, 64, 192), 0.5f);

		animation->AddClip(clip);
	}

	//be attacked
	{
		clip = new Clip(PlayMode::End);

		clip->AddFrame(new Sprite(spriteFile, shaderFile, 1, 499, 14, 511), 0.1f);
		clip->AddFrame(new Sprite(spriteFile, shaderFile, 15, 498, 31, 513), 0.1f);
		clip->AddFrame(new Sprite(spriteFile, shaderFile, 30, 497, 46, 512), 0.1f);
		clip->AddFrame(new Sprite(spriteFile, shaderFile, 46, 497, 63, 511), 0.1f);

		animation->AddClip(clip);
	}

	animation->Position(position);
	animation->Scale(scale);
	animation->Play(0);



}

Inveder3::~Inveder3()
{
	SafeDelete(animation);
}

Sprite * Inveder3::GetSprite()
{
	return animation->GetSprite();
}

void Inveder3::Update(D3DXMATRIX&V, D3DXMATRIX&P)
{
	for (bool val : Hit) {


		if (val) HitCount += 1;

		if (HitCount == 3) {

			Space::score += 500;
			state3 = State3::Attacked;
			timecheck = true;

			HitCount = 0;

		}

	}

	animation->Position(position);

	animation->Play((UINT)state3);
	animation->Update(V, P);

}

void Inveder3::Render()
{
	animation->Render();

	//ImGui::LabelText("Inveder3Time", "%f", Itime);
	//ImGui::LabelText("Inveder3Hit", "%d", Hit);
}


void Inveder3::Moveleft()
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
			state3 = State3::Idle;
			timecheck = false;
		}
	}


	if (position.x < -410) {
		position.x = tempx;
		position.y = tempy;
	}
}

