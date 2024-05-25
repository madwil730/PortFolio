#include "stdafx.h"
#include "Convert.h"
#include "Converter.h"


void Convert::Initialize()
{
	//Airplane();
	//Tower();
	//Tank();
	//Kachujin();
	//Mutant();
	//Megan();
	//Weapon();
	Boss();
	//Ninja();
}

void Convert::Airplane()
{
	Converter* conv = new Converter();
	conv->ReadFile(L"B787/Airplane.fbx");
	conv->ExportMaterial(L"B787/Airplane", false);
	conv->ExportMesh(L"B787/Airplane");
	SafeDelete(conv);
}

void Convert::Tower()
{
	Converter* conv = new Converter();
	conv->ReadFile(L"Tower/Tower.fbx");
	conv->ExportMaterial(L"Tower/Tower", false);
	conv->ExportMesh(L"Tower/Tower");
	SafeDelete(conv);
}

void Convert::Tank()
{
	Converter* conv = new Converter();
	conv->ReadFile(L"Tank/Tank.fbx");
	conv->ExportMaterial(L"Tank/Tank", false);
	conv->ExportMesh(L"Tank/Tank");
	SafeDelete(conv);
}

void Convert::Kachujin()
{
	Converter* conv = new Converter();
	conv->ReadFile(L"Kachujin/Mesh.fbx");
	conv->ExportMaterial(L"Kachujin/Mesh");
	conv->ExportMesh(L"Kachujin/Mesh");
	SafeDelete(conv);

	conv = new Converter();
	conv->ReadFile(L"Kachujin/Idle.fbx");
	conv->ExportAnimClip(0, L"Kachujin/Idle");
	SafeDelete(conv);

	conv = new Converter();
	conv->ReadFile(L"Kachujin/Running.fbx");
	conv->ExportAnimClip(0, L"Kachujin/Running");
	SafeDelete(conv);

	conv = new Converter();
	conv->ReadFile(L"Kachujin/Jump.fbx");
	conv->ExportAnimClip(0, L"Kachujin/Jump");
	SafeDelete(conv);

	conv = new Converter();
	conv->ReadFile(L"Kachujin/Hip_Hop_Dancing.fbx");
	conv->ExportAnimClip(0, L"Kachujin/Hip_Hop_Dancing");
	SafeDelete(conv);

	conv = new Converter();
	conv->ReadFile(L"Kachujin/Attack.fbx");
	conv->ExportAnimClip(0, L"Kachujin/Attack");
	SafeDelete(conv);

	conv = new Converter();
	conv->ReadFile(L"Kachujin/Skill.fbx");
	conv->ExportAnimClip(0, L"Kachujin/Skill");
	SafeDelete(conv);

	conv = new Converter();
	conv->ReadFile(L"Kachujin/Skill2.fbx");
	conv->ExportAnimClip(0, L"Kachujin/Skill2");
	SafeDelete(conv);

	conv = new Converter();
	conv->ReadFile(L"Kachujin/Skill3.fbx");
	conv->ExportAnimClip(0, L"Kachujin/Skill3");
	SafeDelete(conv);

	conv = new Converter();
	conv->ReadFile(L"Kachujin/Backflip.fbx");
	conv->ExportAnimClip(0, L"Kachujin/Backflip");
	SafeDelete(conv);

	conv = new Converter();
	conv->ReadFile(L"Kachujin/Die.fbx");
	conv->ExportAnimClip(0, L"Kachujin/Die");
	SafeDelete(conv);
}

void Convert::Megan()
{
	Converter* conv = new Converter();
	conv->ReadFile(L"Megan/Mesh.fbx");
	conv->ExportMaterial(L"Megan/Mesh", false);
	conv->ExportMesh(L"Megan/Mesh");
	SafeDelete(conv);

	conv = new Converter();
	conv->ReadFile(L"Megan/Taunt.fbx");
	conv->ExportAnimClip(0, L"Megan/Taunt");
	SafeDelete(conv);

	conv = new Converter();
	conv->ReadFile(L"Megan/Dancing.fbx");
	conv->ExportAnimClip(0, L"Megan/Dancing");
	SafeDelete(conv);
}

void Convert::Mutant()
{
	Converter* conv = new Converter();
	conv->ReadFile(L"Mutant/Mesh.fbx");
	conv->ExportMaterial(L"Mutant/Mesh");
	conv->ExportMesh(L"Mutant/Mesh");
	SafeDelete(conv);

	conv = new Converter();
	conv->ReadFile(L"Mutant/Idle.fbx");
	conv->ExportAnimClip(0, L"Mutant/Idle");
	SafeDelete(conv);

	conv = new Converter();
	conv->ReadFile(L"Mutant/Attack.fbx");
	conv->ExportAnimClip(0, L"Mutant/Attack");
	SafeDelete(conv);

	conv = new Converter();
	conv->ReadFile(L"Mutant/Walking.fbx");
	conv->ExportAnimClip(0, L"Mutant/Walking");
	SafeDelete(conv);

	conv = new Converter();
	conv->ReadFile(L"Mutant/Die.fbx");
	conv->ExportAnimClip(0, L"Mutant/Die");
	SafeDelete(conv);
}

void Convert::Weapon()
{
	vector<wstring> names;
	names.push_back(L"Cutter.fbx");
	names.push_back(L"Cutter2.fbx");
	names.push_back(L"Dagger_epic.fbx");
	names.push_back(L"Dagger_small.fbx");
	names.push_back(L"Katana.fbx");
	names.push_back(L"LongArrow.obj");
	names.push_back(L"LongBow.obj");
	names.push_back(L"Rapier.fbx");
	names.push_back(L"Sword.fbx");
	names.push_back(L"Sword_epic.fbx");
	names.push_back(L"Sword2.fbx");

	for (wstring name : names)
	{
		Converter* conv = new Converter();
		conv->ReadFile(L"Weapon/" + name);

		String::Replace(&name, L".fbx", L"");
		String::Replace(&name, L".obj", L"");

		conv->ExportMaterial(L"Weapon/" + name, false);
		conv->ExportMesh(L"Weapon/" + name);
		SafeDelete(conv);
	}
}

void Convert::Boss()
{
	Converter* conv = new Converter();
	conv->ReadFile(L"Boss/Mesh.fbx");
	conv->ExportMaterial(L"Boss/Mesh");
	conv->ExportMesh(L"Boss/Mesh");
	SafeDelete(conv);

	conv = new Converter();
	conv->ReadFile(L"Boss/Idle.fbx");
	conv->ExportAnimClip(0, L"Boss/Idle");
	SafeDelete(conv);

	conv = new Converter();
	conv->ReadFile(L"Boss/BossJumpAttack.fbx");
	conv->ExportAnimClip(0, L"Boss/BossJumpAttack");
	SafeDelete(conv);

	conv = new Converter();
	conv->ReadFile(L"Boss/BossMagic1.fbx");
	conv->ExportAnimClip(0, L"Boss/BossMagic1");
	SafeDelete(conv);

	conv = new Converter();
	conv->ReadFile(L"Boss/BossMagic2.fbx");
	conv->ExportAnimClip(0, L"Boss/BossMagic2");
	SafeDelete(conv);

	conv = new Converter();
	conv->ReadFile(L"Boss/BossWeaponAttack.fbx");
	conv->ExportAnimClip(0, L"Boss/BossWeaponAttack");
	SafeDelete(conv);

	conv = new Converter();
	conv->ReadFile(L"Boss/BossWalk.fbx");
	conv->ExportAnimClip(0, L"Boss/BossWalk");
	SafeDelete(conv);

	conv = new Converter();
	conv->ReadFile(L"Boss/BossDeath.fbx");
	conv->ExportAnimClip(0, L"Boss/BossDeath");
	SafeDelete(conv);
}

void Convert::Ninja()
{
	Converter* conv = new Converter();
	conv->ReadFile(L"Ninja/Mesh.fbx");
	conv->ExportMaterial(L"Ninja/Mesh");
	conv->ExportMesh(L"Ninja/Mesh");
	SafeDelete(conv);

	conv = new Converter();
	conv->ReadFile(L"Ninja/Nidle.fbx");
	conv->ExportAnimClip(0, L"Ninja/Nidle");
	SafeDelete(conv);
}
