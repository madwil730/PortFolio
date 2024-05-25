#pragma once

class Inventory
{
public:

	struct SmyInventory
	{
		float x, y;
		wstring str;
		int price = 0;
		Render2D* image;
		Texture* T_image;
	} myInventory[16];

	struct ShopInventory
	{
		float x, y;
		wstring str;
		int price = 0;
		Render2D* image;
		Texture* T_image;
	} shopInventory[16];

	struct Equipment
	{
		float x, y;
		wstring str;
		int price = 0;
		Render2D* image;
		Texture* T_image;
	} equipment[7];

	struct Skill
	{
		float x, y;
		wstring str;
		bool Inventorycheck;
		Render2D* image;
		Texture* T_image;
	} mySkill[6];

	struct Gear
	{	
		wstring str;
		float Attack = 0;
		float Defense = 0;
	} gear[6];

	struct Goods
	{
		float Attack = 0;
		float Defense = 0;
	}goods[5];

	//0 head
	//1 armer
	//2 glove
	//3 sword
	//4 ring
	
	bool ShopInventorycheck = false;
	bool Equipmentcheck = false;
	void Update();
	void Render();
	void Gear();
	Inventory();

};

