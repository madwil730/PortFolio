#include "stdafx.h"
#include "rock.h"
#include <ctime>
#include "Beam.h"
#include "Scenes/My/Space.h"

rock::rock(D3DXVECTOR2 position, D3DXVECTOR2 scale)
{
	srand((unsigned)time(0));
	animation = new Animation();

	wstring spriteFile = Textures + L"./My/rock.png";
	wstring shaderFile = Shaders + L"Effect.fx";

	this->position = position;

	// idle
	Clip* clip;
	{
		clip = new Clip(PlayMode::Loop);

		clip->AddFrame(new Sprite(spriteFile, shaderFile, 14, 14, 102, 84), 0.5f);

		animation->AddClip(clip);
	}


	animation->Position(position);
	animation->Scale(scale);
	animation->Play(0);



}

rock::~rock()
{
	SafeDelete(animation);
}

Sprite * rock::GetSprite()
{
	return animation->GetSprite();
}

void rock::Update(D3DXMATRIX&V, D3DXMATRIX&P)
{
	

	animation->Position(position);

	animation->Play(0);
	animation->Update(V, P);

}

void rock::Render()
{
	animation->Render();

	//ImGui::LabelText("rockTime", "%f", Itime);
	//ImGui::LabelText("rockHit", "%d", Hit);
}


void rock::Moveleft()
{

	position = animation->Position();

	int tempx = rand() % 200 + 700;


	position.x -= GetMoveSpeed() * Time::Delta();


	if (position.x < -410) {
		position.x = tempx;
	}
}

