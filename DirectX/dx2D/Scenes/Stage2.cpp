#include "stdafx.h"
#include "Stage2.h"

#include "./Object/Player.h"
#include "./Object/Bullet.h"
#include "./Object/Fire.h"

#include "./Viewer/Following.h"

Stage2::Stage2(SceneValues * values)
	:Scene(values)
{
	background = new Sprite(Textures + L"./stage/CastleStage.png", Shaders + L"Effect.fx");
	background->Scale(1.5f, 1.5f);
	background->Position(640, 320);

	player = new Player(D3DXVECTOR2(100, 170), D3DXVECTOR2(2.5f, 2.5f));
	bullet = new Bullet(Shaders + L"Effect.fx", D3DXVECTOR2(300, 300), 0, 0);
	fire = new Fire(Shaders + L"Effect.fx", D3DXVECTOR2(300, 170));

	SafeDelete(values->MainCamera);
	values->MainCamera = new Following(player);
}

Stage2::~Stage2()
{
	SafeDelete(background);
	SafeDelete(player);
	SafeDelete(bullet);
	SafeDelete(fire);
}

void Stage2::Update()
{
	D3DXMATRIX V = values->MainCamera->View();
	D3DXMATRIX P = values->Projection;

	background->Update(V, P);
	player->Update(V, P);
	bullet->Update(V, P);
	fire->Update(V, P);
}

void Stage2::Render()
{

	static bool bCheck = false; //AABB(Sprite vs position)
	static bool bCheck2 = false; //AABB(Sprite vs Sprite)
	static bool bCheck3 = false; //OBB

	Sprite* playerSprite = player->GetSprite();
	Sprite* fireSprite = fire->GetClip()->GetSprite();
	
	bCheck = Sprite::Aabb(playerSprite, bullet->Position());
	ImGui::LabelText("AABB", "%d", bCheck ? 1 : 0);
	playerSprite->DrawCollision(bCheck);

	/*bCheck2 = Sprite::Aabb(playerSprite, fireSprite);
	ImGui::LabelText("AABB2", "%d", bCheck2 ? 1 : 0);*/

	bCheck3 = Sprite::Obb(playerSprite, fireSprite);
	ImGui::LabelText("OBB", "%d", bCheck3 ? 1 : 0);
	playerSprite->DrawCollision(bCheck3);
	fireSprite->DrawCollision(bCheck3);
	
	//background->Render();
	player->Render();
	//bullet->Render();
	fire->Render();
}

