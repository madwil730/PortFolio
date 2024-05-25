#include "stdafx.h"
#include "Space.h"
#include <Windows.h>
#include <ctime>
#include "./Object/Background.h"
#include "./Object/My/Ship.h"
#include "./Object/My/Beam.h"
#include "./Object/My/Inveder.h"
#include "./Object/My/Inveder2.h"
#include "./Object/My/Inveder3.h"
#include "./Object/My/rock.h"
#include "./Object/My/Gameover.h"

int Space::score = 0;
float Space::shipcheck = 0;

Space::Space(SceneValues * values) : Scene(values)
{
	SafeDelete(values->MainCamera);
	values->MainCamera = new Camera;
	srand((unsigned)time(0));

	background1 = new Background(Shaders + L"Effect.fx", D3DXVECTOR2(0, 35), D3DXVECTOR2(2.0f, 1.0f));
	background2 = new Background(Shaders + L"Effect.fx", D3DXVECTOR2(background1->TextureSize().x, 35), D3DXVECTOR2(2.0f, 1.0f));

	gameover = new Gameover(D3DXVECTOR2(0, 0), D3DXVECTOR2(1.0f, 1.0f));

	ship = new Ship(playerPos, playerSize);

	rocks[0] = new rock(D3DXVECTOR2(650, 300), D3DXVECTOR2(2.0f, 7.0f));
	rocks[1] = new rock(D3DXVECTOR2(900, -300), D3DXVECTOR2(4.0f, 5.0f));

	for (int i = 0; i < sizeof(inveder)/sizeof(inveder[0]); i++) {
		tempx[i] = rand() % 700 + 400;
		tempy[i] = rand() % 601 - 300;
	}

	for (int i = 0; i < sizeof(inveder) / sizeof(inveder[0]); i++) {

		inveder[i] = new Inveder(D3DXVECTOR2(tempx[i], tempy[i]), invederSize);
	}
	
	for (int i = 0; i < sizeof(inveder2) / sizeof(inveder2[0]); i++) {

		inveder2[i] = new Inveder2(D3DXVECTOR2(tempx[i], tempy[i]), invederSize);
	}

	for (int i = 0; i < sizeof(inveder3) / sizeof(inveder3[0]); i++) {

		inveder3[i] = new Inveder3(D3DXVECTOR2(tempx[i], tempy[i]), invederSize);
	}


	for (int i = 0; i < sizeof(beam) / sizeof(beam[0]); i++)
		beam[i] = new Beam(beamPos, beamSize);

}

Space::~Space()
{
	SafeDelete(background1);
	SafeDelete(background2);

	SafeDelete(values->MainCamera);

	SafeDelete(ship);
	SafeDelete(gameover);


	for (int i = 0; i < sizeof(beam) / sizeof(beam[0]); i++) SafeDelete(beam[i]);
	for (int i = 0; i < sizeof(inveder) / sizeof(inveder[0]); i++) SafeDelete(inveder[i]);
	for (int i = 0; i < sizeof(inveder2) / sizeof(inveder2[0]); i++) SafeDelete(inveder2[i]);
	for (int i = 0; i < sizeof(inveder3) / sizeof(inveder3[0]); i++) SafeDelete(inveder3[i]);
}

void Space::Update()
{
	D3DXMATRIX V = values->MainCamera->View();
	D3DXMATRIX P = values->Projection;

	values->MainCamera->Update();

	//-----background-----
	background1->Update(V, P);
	background2->Update(V, P);

	if (background1->Position().x < -1024)  background1->Position(background2->Position().x + 1024, 35);
	if (background2->Position().x < -1024)  background2->Position(background1->Position().x + 1024, 35);

	//-----gameover-----

	gameover->Update(V, P);

	//-----ship-----

	ship->Update(V, P);
	ship->Play();

	for (int j = 0; j < sizeof(inveder) / sizeof(inveder[0]); j++)
		ship->SetHit(Sprite::Aabb(inveder[j]->GetSprite(), ship->GetSprite()) && touch && (UINT)inveder[j]->GetState() == 0, j);

	for (int j = 0; j < sizeof(inveder2) / sizeof(inveder2[0]); j++)
		ship->SetHit2(Sprite::Aabb(inveder2[j]->GetSprite(), ship->GetSprite()) && touch && (UINT)inveder2[j]->GetState() == 0, j);

	for (int j = 0; j < sizeof(inveder3) / sizeof(inveder3[0]); j++)
		ship->SetHit3(Sprite::Aabb(inveder3[j]->GetSprite(), ship->GetSprite()) && touch && (UINT)inveder3[j]->GetState() == 0, j);

	for (int j = 0; j < sizeof(rocks) / sizeof(rocks[0]); j++)
		ship->SetHit4(Sprite::Aabb(rocks[j]->GetSprite(), ship->GetSprite()) && touch, j);

	touch = true;

	//-----rock-----
	rocks[0]->Update(V, P);
	rocks[1]->Update(V, P);

	if (score > 20000) {
		rocks[0]->Moveleft();
		rocks[1]->Moveleft();
	}

	//-----beam-----

	if (Key->Down(VK_SPACE)) {

		check = true;

		//빔을 안 쏴더라면
		if (!beamshot[0]) {
			beam[0]->Position(D3DXVECTOR2(ship->Position().x+(ship->GetSprite()->TextureSize().x / 2), ship->Position().y-5));
			beamshot[0] = true;
		}
		else if (!beamshot[1]) {
			beam[1]->Position(D3DXVECTOR2(ship->Position().x + (ship->GetSprite()->TextureSize().x / 2), ship->Position().y-5));
			beamshot[1] = true;
		}
		else if (!beamshot[2]) {
			beam[2]->Position(D3DXVECTOR2(ship->Position().x + (ship->GetSprite()->TextureSize().x / 2), ship->Position().y-5));
			beamshot[2] = true;
		}
		else if (!beamshot[3]) {
			beam[3]->Position(D3DXVECTOR2(ship->Position().x + (ship->GetSprite()->TextureSize().x / 2), ship->Position().y-5));
			beamshot[3] = true;
		}
		else if (!beamshot[4]) {
			beam[4]->Position(D3DXVECTOR2(ship->Position().x + (ship->GetSprite()->TextureSize().x / 2), ship->Position().y-5));
			beamshot[4] = true;
		}
		else if (!beamshot[5]) {
			beam[5]->Position(D3DXVECTOR2(ship->Position().x + (ship->GetSprite()->TextureSize().x / 2), ship->Position().y-5));
			beamshot[5] = true;
		}
	}

	//빔 쏘면 사라지는거 적 1
	for (int j = 0; j < sizeof(inveder) / sizeof(inveder[0]); j++) {

		for (int i = 0; i < sizeof(beam) / sizeof(beam[0]); i++) {

			beam[i]->SetHit(Sprite::Aabb(inveder[j]->GetSprite(), beam[i]->GetSprite()) &&beam[i] && (UINT)inveder[j]->GetState() == 0 , j); // 이거 물어보기
		}
	}

	//빔 쏘면 사라지는거 적 2
	for (int j = 0; j < sizeof(inveder2) / sizeof(inveder2[0]); j++) {

		for (int i = 0; i < sizeof(beam) / sizeof(beam[0]); i++) {

			beam[i]->SetHit2(Sprite::Aabb(inveder2[j]->GetSprite(), beam[i]->GetSprite()) && beam[i] && (UINT)inveder2[j]->GetState() == 0, j);
		}
	}

	//빔 쏘면 사라지는거 적 3
	for (int j = 0; j < sizeof(inveder3) / sizeof(inveder3[0]); j++) {

		for (int i = 0; i < sizeof(beam) / sizeof(beam[0]); i++) {

			beam[i]->SetHit3(Sprite::Aabb(inveder3[j]->GetSprite(), beam[i]->GetSprite()) && beam[i] && (UINT)inveder3[j]->GetState() == 0, j);
		}
	}

	for (int i = 0; i < sizeof(beam) / sizeof(beam[0]); i++) {

		if (check) {
			beam[i]->Update(V, P);
			beam[i]->MoveRight();
		}

		if (beam[i]->Position().x > 400) beamshot[i] = false;
	}
	
	//------------------------inveder-------------------------------------


	for (int i = 0; i < sizeof(inveder) / sizeof(inveder[0]); i++) {

		inveder[i]->Moveleft();
		inveder[i]->Update(V, P);

		if (score > 6000) inveder[i]->SetMoveSpeed(250.0f);

		else if (score > 3000) inveder[i]->SetMoveSpeed(200.0f);

		else if (score > 1000) inveder[i]->SetMoveSpeed(100.0f);

		else if (score == 0) inveder[i]->SetMoveSpeed(50.0f);

	}

	for (int i = 0; i < sizeof(beam) / sizeof(beam[0]); i++) {

		for (int j = 0; j < sizeof(inveder) / sizeof(inveder[0]); j++) {

		inveder[j]->SetHit(Sprite::Aabb(inveder[j]->GetSprite(), beam[i]->GetSprite()) && beamshot[i] && (UINT)inveder[j]->GetState() == 0, i);

		}
	}

	//------------------------inveder2-------------------------------------

	for (int i = 0; i < sizeof(inveder2) / sizeof(inveder2[0]); i++) 
		inveder2[i]->Update(V, P); // 업데이트 질문하기

	if (score >= 5000 ) {

		for (int i = 0; i < sizeof(inveder2) / sizeof(inveder2[0]); i++) {

			inveder2[i]->Moveleft();
			

			if (score > 12000) inveder2[i]->SetMoveSpeed(250.0f);

			else if (score > 10000) inveder2[i]->SetMoveSpeed(200.0f);

			else if (score > 7000) inveder2[i]->SetMoveSpeed(100.0f);

			else if (score == 5000) inveder2[i]->SetMoveSpeed(50.0f);

		}

		for (int i = 0; i < sizeof(beam) / sizeof(beam[0]); i++) {

			for (int j = 0; j < sizeof(inveder2) / sizeof(inveder2[0]); j++) {

				inveder2[j]->SetHit(Sprite::Aabb(inveder2[j]->GetSprite(), beam[i]->GetSprite()) && beamshot[i] && (UINT)inveder2[j]->GetState() == 0, i);
			}
		}
	}

	//------------------------inveder3-------------------------------------

	for (int i = 0; i < sizeof(inveder3) / sizeof(inveder3[0]); i++)
		inveder3[i]->Update(V, P); // 업데이트 질문하기


	if (score >= 10000) {

		for (int i = 0; i < sizeof(inveder3) / sizeof(inveder3[0]); i++) {
			inveder3[i]->Moveleft();

			if (score > 30000) inveder3[i]->SetMoveSpeed(300.0f);

			else if (score > 25000) inveder3[i]->SetMoveSpeed(250.0f);

			else if (score > 20000) inveder3[i]->SetMoveSpeed(150.0f);

			else if (score == 10000) inveder3[i]->SetMoveSpeed(50.0f);

		}

		for (int i = 0; i < sizeof(beam) / sizeof(beam[0]); i++) {

			for (int j = 0; j < sizeof(inveder3) / sizeof(inveder3[0]); j++) {

				inveder3[j]->SetHit(Sprite::Aabb(inveder3[j]->GetSprite(), beam[i]->GetSprite()) && beamshot[i] && (UINT)inveder3[j]->GetState() == 0, i);
			}
		}
	}


}


void Space::Render()
{

	srand((unsigned)time(0));

	background1->Render();
	background2->Render();

	//돌 10000 넘으면 출현
	if (score > 20000) {

		rocks[0]->Render();
		rocks[1]->Render();
	}
	
	//우주선
	ship->Render();


	// 빔
	for (int i = 0; i < sizeof(beam) / sizeof(beam[0]); i++) {

		if (check) beam[i]->Render();
		//ImGui::LabelText("BeamPositoin", "%0.2f, %0.2f", beam[i]->Position().x, beam[i]->Position().y);
		//ImGui::LabelText("Beamshot", "%d", beamshot[i]);
	}
	
	// 적 1
	for (int i = 0; i < sizeof(inveder) / sizeof(inveder[0]); i++) {
		inveder[i]->Render();
		/*ImGui::LabelText("Positoin","%0.2f, %0.2f",  inveder[i]->Position().x, inveder[i]->Position().y);
		ImGui::LabelText("inveder number", "%d", i);
		ImGui::LabelText("invederHit", "%d", inveder[i]->GetHit(i));
		*/
		//ImGui::LabelText("inveder state", "%d", (UINT)inveder[i]->GetState());
		//ImGui::LabelText("inveder tempx", "%d", tempx[i]);
	}


	// 4000점 넘으면 적 2 출현
	if (score >= 4000) {

		for (int i = 0; i < sizeof(inveder2) / sizeof(inveder2[0]); i++) {

			inveder2[i]->Render();
			ImGui::LabelText("Positoin", "%0.2f, %0.2f", inveder2[i]->Position().x, inveder2[i]->Position().y);

		}
	}

	
	// 7000점 넘으면 적 3 출현
	if (score >= 10000) {

		for (int i = 0; i < sizeof(inveder3) / sizeof(inveder3[0]); i++) {

			inveder3[i]->Render();
			ImGui::LabelText("Positoin3","%0.2f, %0.2f",  inveder3[i]->Position().x, inveder3[i]->Position().y);

		}
	}

	if (Ship::death) {

		shipcheck += Time::Delta();

		if (shipcheck > 2) {

			ship->Position(D3DXVECTOR2(-300, 0));
			gameover->Render();
			ship->hitcheck = false;
			ship->SetState(0);
		}
	}

	DirectWrite::GetDC()->BeginDraw();
	{
		RECT rect = { 10, 10, 500, 200 };

		rect.top += 0;
		rect.bottom += 0;
		wstring text = L"점수 : " + to_wstring(score);
		DirectWrite::RenderText(text, rect);

	}
	DirectWrite::GetDC()->EndDraw();

	ImGui::LabelText("shipcheck", "%0.2f", shipcheck);
}

