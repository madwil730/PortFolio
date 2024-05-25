#include "stdafx.h"
#include "Stage1.h"

#include "./Object/Player.h"
#include "./Viewer/Following.h"

Stage1::Stage1(SceneValues * values)
	:Scene(values)
{
	background = new Sprite(Textures + L"./stage/Stage.png", Shaders + L"Effect.fx");
	background->Scale(2.5f, 2.5f);
	background->Position(0, 300);

	player = new Player(D3DXVECTOR2(100, 170), D3DXVECTOR2(2.5f, 2.5f));

	SafeDelete(values->MainCamera);
	values->MainCamera = new Following(player);
}

Stage1::~Stage1()
{
	SafeDelete(background);
	SafeDelete(player);
}

void Stage1::Update()
{
	D3DXMATRIX V = values->MainCamera->View();
	D3DXMATRIX P = values->Projection;

	background->Update(V, P);
	player->Update(V, P);
}

void Stage1::Render()
{
	background->Render();
	player->Render();
}

