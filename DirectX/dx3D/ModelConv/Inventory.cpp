#include "stdafx.h"
#include "Inventory.h"


Inventory::Inventory()
{
	// 내 인벤토리
	for(int i = 0 ; i < 16 ; i += 1)
	{
		myInventory[i].image = new Render2D();
		myInventory[i].image->GetTransform()->Position(2000, 2000, 0);
		myInventory[i].image->GetTransform()->Scale(50, 50, 1);
		myInventory[i].T_image = new Texture(L"Green.png"); // 쓰레기값 입력

		if (i < 4) {
			myInventory[i].y = 510;
			myInventory[i].x = 905 + i * 60;
		}

		else if (i >= 4 && i < 8)
		{
			myInventory[i].y = 430;
			myInventory[i].x = 905 + (i % 4) * 60;
		}

		else if (i >= 8 && i < 12)
		{
			myInventory[i].y = 345;
			myInventory[i].x = 905 + (i % 8) * 60;
		}

		else if (i >= 12 && i < 16)
		{
			myInventory[i].y = 265;
			myInventory[i].x = 905 + (i % 12) * 60;
		}
	}

	//상인
	for (int i = 0; i < 8; i += 1)
	{
		shopInventory[i].image = new Render2D();
		shopInventory[i].image->GetTransform()->Position(2000, 2000, 0);
		shopInventory[i].image->GetTransform()->Scale(50, 50, 1);
		shopInventory[i].T_image = new Texture(L"Green.png"); // 쓰레기값 입력

		if (i < 4) {
			shopInventory[i].y = 510;
			shopInventory[i].x = 400 + i * 60;
		}

		else if (i >= 4 && i < 8)
		{
			shopInventory[i].y = 430;
			shopInventory[i].x = 400 + (i % 4) * 60;
		}
	}

	// 스킬창
	for (int i = 0; i < 6; i += 1)
	{
		mySkill[i].image = new Render2D();
		mySkill[i].image->GetTransform()->Position(2000, 2000, 0);
		mySkill[i].image->GetTransform()->Scale(50, 50, 1);
		mySkill[i].T_image = new Texture(L"Green.png"); // 쓰레기값 입력

		mySkill[i].x = 180 + (i % 6) * 60;
		mySkill[i].y = 80;
	}

	// 장비창
	for(int i  = 0; i < 7; i++)
	{
		equipment[i].image = new Render2D();
		equipment[i].image->GetTransform()->Position(2000, 2000, 0);
		equipment[i].image->GetTransform()->Scale(50, 50, 1);
		equipment[i].T_image = new Texture(L"Green.png"); // 쓰레기값 입력
	}

	equipment[0].x = 578; //머리
	equipment[0].y = 510;

	equipment[1].x = 578; //갑옷
	equipment[1].y = 412;
			  
	equipment[2].x = 578; //장갑
	equipment[2].y = 362;

	equipment[3].x = 620; //칼
	equipment[3].y = 412;

	equipment[4].x = 380; //반지
	equipment[4].y = 410;

	equipment[5].x = 380; //목걸이
	equipment[5].y = 465;

	equipment[6].x = 578; //망토
	equipment[6].y = 461;

	shopInventory[0].image = new Render2D();
	shopInventory[0].image->GetTransform()->Position(shopInventory[0].x, shopInventory[0].y, 0);
	shopInventory[0].image->GetTransform()->Scale(50, 50, 1);
	shopInventory[0].str = L"redPortion.png";
	shopInventory[0].T_image = new Texture(shopInventory[0].str);
	shopInventory[0].price = 50;

	shopInventory[1].image = new Render2D();
	shopInventory[1].image->GetTransform()->Position(shopInventory[1].x, shopInventory[1].y, 0);
	shopInventory[1].image->GetTransform()->Scale(50, 50, 1);
	shopInventory[1].str = L"ring1.png";
	shopInventory[1].T_image = new Texture(shopInventory[1].str);
	shopInventory[1].price = 150;

	shopInventory[2].image = new Render2D();
	shopInventory[2].image->GetTransform()->Position(shopInventory[2].x, shopInventory[2].y, 0);
	shopInventory[2].image->GetTransform()->Scale(50, 50, 1);
	shopInventory[2].str = L"ring2.png";
	shopInventory[2].T_image = new Texture(shopInventory[2].str);
	shopInventory[2].price = 200;

	shopInventory[3].image = new Render2D();
	shopInventory[3].image->GetTransform()->Position(shopInventory[3].x, shopInventory[3].y, 0);
	shopInventory[3].image->GetTransform()->Scale(50, 50, 1);
	shopInventory[3].str = L"Sword.png";
	shopInventory[3].T_image = new Texture(shopInventory[3].str);
	shopInventory[3].price = 300;

	//mySkill[0].image = new Render2D();
	//mySkill[0].image->GetTransform()->Position(mySkill[0].x, mySkill[0].y, 0);
	//mySkill[0].image->GetTransform()->Scale(50, 50, 1);
	//mySkill[0].T_image = new Texture(L"Skill1.png");

	Gear();
}

void Inventory::Update()
{
	shopInventory[0].image->Update();
	shopInventory[1].image->Update();
	shopInventory[2].image->Update();
	shopInventory[3].image->Update();

	for (int i = 0; i < 7; i++)
	{
		equipment[i].image->Update();
	}
}

void Inventory::Render()
{
	if (ShopInventorycheck)
	{
		shopInventory[0].image->SRV(shopInventory[0].T_image->SRV());
		shopInventory[0].image->Render();

		shopInventory[1].image->SRV(shopInventory[1].T_image->SRV());
		shopInventory[1].image->Render();

		shopInventory[2].image->SRV(shopInventory[2].T_image->SRV());
		shopInventory[2].image->Render();

		shopInventory[3].image->SRV(shopInventory[3].T_image->SRV());
		shopInventory[3].image->Render();
	}

	if (Equipmentcheck)
	{
		for (int i = 0; i < 7; i++)
		{
			equipment[i].image->SRV(equipment[i].T_image->SRV());
			equipment[i].image->Render();
		}
	}
}

void Inventory::Gear()
{
	gear[0].str = L"ring1";
	gear[0].Attack = 1;
	gear[0].Defense = 1;

	gear[1].str = L"Armer";
	gear[1].Attack = 2;
	gear[1].Defense = 0;

	gear[2].str = L"Armer";
	gear[2].Attack = 0;
	gear[2].Defense = 2;

	gear[3].str = L"Katana";
	gear[3].Attack = 3;
	gear[3].Defense = 0;

	gear[4].str = L"ring1";
	gear[4].Attack = 2;
	gear[4].Defense = 0;

	gear[5].str = L"ring2";
	gear[5].Attack = 1;
	gear[5].Defense = 1;

}
