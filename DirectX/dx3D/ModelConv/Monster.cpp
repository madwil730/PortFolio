#include "stdafx.h"
#include "Monster.h"
#include "Damage.h"
#include "Environment/SkyCube.h"
#include "Viewer/Freedom.h"
#include <ctime>


Monster::Monster()
{
	srand((unsigned int)time(NULL));
	shader = new Shader(L"33_Animation.fx");
	TransformBossSkill = new Transform();
	TransformBossSkill->Position(2000, 2000, 0);
	TransformBossSkill->Scale(1, 1, 1);
	ColliderBossSkill = new Collider(TransformBossSkill);

	SphereTransform = new Transform();
	SphereTransform->Position(2000, 2000, 0);
	SphereTransform->Scale(1, 1, 1);
	SphereCollider = new Collider(SphereTransform);

	Mutant();
	Boss();
	SetInventory();
	SetItem();
	Sphere();

	x[0],x[1],x[2],z[0],z[1],z[2] = 0;
	Mchoice[0], Mchoice[1], Mchoice[2], Mchoice[3], Mchoice[4] = 0;
	Inventorycheck[0], Inventorycheck[1], Inventorycheck[2], Inventorycheck[3], Inventorycheck[4] = false;

	mutant->GetTransform(3)->Position(1000, 1000, 1000);
	mutant->GetTransform(4)->Position(1000, 1000, 1000);

}

void Monster::Instruction(ModelAnimator * kachujin, ModelAnimator * monster, int i, bool Judgment)
{
	if (Judgment)
	{

		// mutant 
		if (fabsf(D3DXVec3Length(&kachujin->GetTransform(0)->GetPosition()) - D3DXVec3Length(&monster->GetTransform(i)->GetPosition())) < 1) { // &써야하는 이유 묻기 & 이거 쓰면 인자값의 주소가 리턴되는 거 아니냐고 묻기

			if (AttackClip[i]) { // 공격 한다면

				monster->PlayClip(i, 2, 1.0f, 0.7f);
				AttackClip[i] = false; // 공격 클립 활성
				onAttack[i] = true; // 히트 여부 활성
			}

		}

		else if (fabsf(D3DXVec3Length(&kachujin->GetTransform(0)->GetPosition()) - D3DXVec3Length(&monster->GetTransform(i)->GetPosition())) < 8) {

			goPlayer(kachujin, monster, i);

			if (AttackClip[i] == false) { // 따라가기

				monster->PlayClip(i, 1, 1.0f, 0.7f);
				onAttack[i] = false; // 히트 여부 비활성
				AttackTime[i] = 0;
				AttackClip[i] = true;
			}
		}

		else
			RandomMove(monster, i);
	}
	

	//ImGui::LabelText("onattack", "%d", onAttack[i]);
}

void Monster::BossInstruction(ModelAnimator * kachujin,  ModelAnimator* monster, int i, Billboard* bill)
{

	if (bill->vertices[0].Scale.x < 1.5)
	{
		bossSkillTime += Time::Delta();
	}
		
	monster->HitDistanceCollider->GetTransform()->Position(monster->GetTransform(0)->GetPosition());
	monster->HitDistanceCollider->GetTransform()->Scale(2, 2, 2);
	monster->HitDistanceCollider->Update();
	monster->DistanceCollider->GetTransform()->Position(monster->GetTransform(0)->GetPosition());
	monster->DistanceCollider->GetTransform()->Scale(15, 10, 15);
	monster->DistanceCollider->Update();
	monster->LongCollider->GetTransform()->Position(monster->GetTransform(0)->GetPosition());
	monster->LongCollider->GetTransform()->Scale(30, 10, 30);
	monster->LongCollider->Update();


	// 보스 공격
	if (kachujin->DistanceCollider->IsIntersect(monster->HitDistanceCollider)) {
		if (monster->AttackClip) { // 공격 한다면

			monster->PlayClip(0, 2, 1.0f, 0.7f);
			monster->AttackClip = false; // 공격 클립 활성
			monster->onAttack = true; // 히트 여부 활성
		}

		if (bossSkillTime > 5 && bossSkillTime < 5.1)
		{
			// 보스 스킬 체크
			monster->PlayClip(0, 3, 1.0f, 0.7f);

			Vector3 temp = monster->GetTransform(0)->GetPosition();
			ColliderBossSkill->GetTransform()->Position(temp);
			ColliderBossSkill->GetTransform()->Scale(7, 2, 7);
		}

		else if (bossSkillTime > 8)
			ColliderBossSkill->GetTransform()->Position(2000, 2000, 0);

		if (bossSkillTime > 10)
		{
			bossSkillTime = 0;
			monster->PlayClip(0, 2, 1.0f, 0.7f);
		}

		LongDistance = false;
		MoveDistance = false;
	}
	else
	{
		LongDistance = true;
		MoveDistance = true;
	}
	// 보스 이동
	if (kachujin->DistanceCollider->IsIntersect(monster->DistanceCollider) && MoveDistance) {

		goPlayer(kachujin, monster, i);

		if (monster->AttackClip == false) { // 따라가기

			monster->PlayClip(0, 1, 1.0f, 0.7f);
			monster->onAttack = false; // 히트 여부 비활성
			monster->AttackTime = 0;
			monster->AttackClip = true;
		}

		LongDistance = false;
		LongCheck = false;

	}
	else
		LongDistance = true;

	//보스 원거리 공격
	 if (kachujin->DistanceCollider->IsIntersect(monster->LongCollider) && LongDistance) {

		if (!LongCheck) {

			monster->PlayClip(0, 5, 1.0f, 0.7f);

			D3DXVec3Normalize(&Long,
				new D3DXVECTOR3(
					kachujin->GetTransform(0)->position.x - boss->GetTransform(0)->position.x,
					kachujin->GetTransform(0)->position.y - boss->GetTransform(0)->position.y,
					kachujin->GetTransform(0)->position.z - boss->GetTransform(0)->position.z)
			);

			sphere->GetTransform(0)->Position(boss->GetTransform(0)->GetPosition().x, 2, boss->GetTransform(0)->GetPosition().z);
			LongCheck = true;
		}
	}

	 SphereCollider->GetTransform()->Position(sphere->GetTransform(0)->GetPosition());

	sphere->GetTransform(0)->position.x += Long.x * 10 * Time::Delta();
	sphere->GetTransform(0)->position.z += Long.z * 10 * Time::Delta();

	
	ImGui::LabelText("Long", "%d", LongDistance);
	ImGui::LabelText("move", "%d", MoveDistance);

}



void Monster::RandomMove(ModelAnimator* i, int j)
{
	i->time += Time::Delta(); // 초기화 안하면 작동 안됨 질문좀

	if (i->time < 5)
	{
		if (playclip[j]) {
			i->PlayClip(j, 1, 1.0f, 0.7f); // 걷기 클립

			x[j] = rand() % 3 - 1.5;
			z[j] = rand() % 3 - 1.5;

			playclip[j] = false;
		}

		if (x[j] > -1 && x[j] < 1 && z[j] > 0) i->GetTransform(j)->Rotation(0, 9.45, 0);  // 앞

		else if (x[j] > -1 && x[j] < 1 && z[j] < 0) i->GetTransform(j)->Rotation(0, 0, 0); //뒤

		else if (x[j] > 0) i->GetTransform(j)->Rotation(0, 11.4, 0); // 오른쪽

		else if (x[j] > 0) i->GetTransform(j)->Rotation(0, 8.2, 0); //왼쪽

		i->GetTransform(j)->position.x += x[j] * Time::Delta();
		i->GetTransform(j)->position.z += z[j] * Time::Delta();
		i->GetTransform(j)->UpdateWorld();
	}

	else if (i->time > 5)
	{
		if (!playclip[j]) {
			i->PlayClip(j, 0, 1.0f, 0.7f);
			playclip[j] = true;
		}
	}

	if (i->time > 9) i->time = 0;

	i->UpdateTransforms();
}

void Monster::goPlayer(ModelAnimator* kachujin, ModelAnimator* i, int j)
{

	if (kachujin->GetTransform(0)->position.x - i->GetTransform(j)->position.x > -1 && kachujin->GetTransform(0)->position.x - i->GetTransform(j)->position.x < 1) {

		if (kachujin->GetTransform(0)->position.z > i->GetTransform(j)->position.z) {

			i->GetTransform(j)->position.z += 2 * Time::Delta();
			i->GetTransform(j)->Rotation(0, 9.45, 0);
			i->Direction = { 0,0,1 };
		}

		else {

			i->GetTransform(j)->position.z += -2 * Time::Delta();
			i->GetTransform(j)->Rotation(0, 0, 0);
			i->Direction = { 0,0,-1 };
		}
	}

	else if (kachujin->GetTransform(0)->position.x >= i->GetTransform(j)->position.x) {

		i->GetTransform(j)->position.x += 2 * Time::Delta();
		i->GetTransform(j)->Rotation(0, 11.4, 0);
		i->Direction = { -1,0,0 };

	}

	else if (kachujin->GetTransform(0)->position.x <= i->GetTransform(j)->position.x) {

		i->GetTransform(j)->position.x += -2 * Time::Delta();
		i->GetTransform(j)->Rotation(0, 8.2, 0);
		i->Direction = { +1,0,0 };
	}

	i->GetTransform(j)->UpdateWorld();
	i->UpdateTransforms();
}

void Monster::Mutant()
{
	
	
	mutant = new ModelAnimator(shader);
	mutant->ReadMaterial(L"Mutant/Mesh");
	mutant->ReadMesh(L"Mutant/Mesh");
	mutant->ReadClip(L"Mutant/Idle");
	mutant->ReadClip(L"Mutant/Walking");
	mutant->ReadClip(L"Mutant/Attack");
	mutant->ReadClip(L"Mutant/Die");

	for (int i = 0; i < 5; i++) // 몬스터 수 조절
	{
		Transform* transform = mutant->AddTransform();
		transform->Position(100 + i*5, 0, 100);
		transform->Scale(0.01f, 0.01f, 0.01f);
	}

	mutant->UpdateTransforms();
	mutant->Pass(2);
	
}

void Monster::Space()
{
	space = new ModelAnimator(shader);
	space->ReadMaterial(L"Space/Mesh");
	space->ReadMesh(L"Space/Mesh");
	space->ReadClip(L"Space/Idle");
	space->ReadClip(L"Space/Walk");
	space->ReadClip(L"Space/Magic");

	Transform* transform = space->AddTransform();
	transform->Position(5, 0, -5);
	transform->Scale(0.01f, 0.01f, 0.01f);

	space->UpdateTransforms();
	space->Pass(2);
}

void Monster::Boss()
{
	boss = new ModelAnimator(shader);
	boss->ReadMaterial(L"Boss/Mesh");
	boss->ReadMesh(L"Boss/Mesh");
	boss->ReadClip(L"Boss/Idle"); //0
	boss->ReadClip(L"Boss/BossWalk"); //1
	boss->ReadClip(L"Boss/BossWeaponAttack"); //2
	boss->ReadClip(L"Boss/BossMagic2"); //3
	boss->ReadClip(L"Boss/BossDeath"); //4
	boss->ReadClip(L"Boss/BossMagic1"); //5

	bossWeapon = new ModelRender(shader);
	bossWeapon->ReadMaterial(L"Weapon/Dagger_epic");
	bossWeapon->ReadMesh(L"Weapon/Dagger_epic");

	Transform attachTransform;
	attachTransform.Position(-10, 0, -10);
	attachTransform.Scale(1, 2, 1);

	boss->GetModel()->Attach(shader, bossWeapon->GetModel(), 35, &attachTransform);
	
	Transform* transform = boss->AddTransform();
	transform->Position(80, 1, 70);
	transform->Scale(0.02f, 0.02f, 0.02f);

	boss->UpdateTransforms();
	boss->Pass(2);
}

void Monster::Sphere()
{
	sphere = new MeshRender(shader, new MeshSphere(0.5f, 20, 20));

	Transform* transform = NULL;
	transform = sphere->AddTransform();
	transform->Position(1000, 1000, 10);
	transform->Scale(1, 1, 1);

	sphere->UpdateTransforms();

	sphere->Pass(0);
}

void Monster::SetInventory()
{
	for (int i = 0; i < EnemyNumber; i++)
	{
		for (int j = 0; j < 16; j += 1)
		{
			EnemyInventory[i][j].image = new Render2D();
			EnemyInventory[i][j].image->GetTransform()->Position(2000, 2000, 0);
			EnemyInventory[i][j].image->GetTransform()->Scale(50, 50, 1);
			EnemyInventory[i][j].T_image = new Texture(L"Green.png");

			if (j < 4) {
				EnemyInventory[i][j].y = 510;
				EnemyInventory[i][j].x = 100 + j * 60;
			}

			else if (j >= 4 && j < 8)
			{
				EnemyInventory[i][j].y = 430;
				EnemyInventory[i][j].x = 100 + (j % 4) * 60;
			}

			else if (j >= 8 && j < 12)
			{
				EnemyInventory[i][j].y = 345;
				EnemyInventory[i][j].x = 100 + (j % 8) * 60;
			}

			else if (j >= 12 && j < 16)
			{
				EnemyInventory[i][j].y = 265;
				EnemyInventory[i][j].x = 100 + (j % 12) * 60;
			}
		}
	}
}

void Monster::SetItem()
{
	//EnemyInventory[0][0].image = new Render2D();
	EnemyInventory[0][0].image->GetTransform()->Position(EnemyInventory[0][0].x, EnemyInventory[0][0].y, 0);
	EnemyInventory[0][0].image->GetTransform()->Scale(50, 50, 1);
	EnemyInventory[0][0].str = L"redPortion.png";
	EnemyInventory[0][0].T_image = new Texture(EnemyInventory[0][0].str);
	EnemyInventory[0][0].price = 50;

	//EnemyInventory[1][0].image = new Render2D();
	EnemyInventory[1][0].image->GetTransform()->Position(EnemyInventory[1][0].x, EnemyInventory[1][0].y, 0);
	EnemyInventory[1][0].image->GetTransform()->Scale(50, 50, 1);
	EnemyInventory[1][0].str = L"redPortion.png";
	EnemyInventory[1][0].T_image = new Texture(EnemyInventory[1][0].str);
	EnemyInventory[1][0].price = 50;

	//EnemyInventory[1][1].image = new Render2D();
	EnemyInventory[1][1].image->GetTransform()->Position(EnemyInventory[1][1].x, EnemyInventory[1][1].y, 0);
	EnemyInventory[1][1].image->GetTransform()->Scale(50, 50, 1);
	EnemyInventory[1][1].str = L"Necklace.PNG";
	EnemyInventory[1][1].T_image = new Texture(EnemyInventory[1][1].str);
	EnemyInventory[1][1].price = 50;

	//EnemyInventory[1][0].image = new Render2D();
	EnemyInventory[2][0].image->GetTransform()->Position(EnemyInventory[2][0].x, EnemyInventory[2][0].y, 0);
	EnemyInventory[2][0].image->GetTransform()->Scale(50, 50, 1);
	EnemyInventory[2][0].str = L"Rock.PNG";
	EnemyInventory[2][0].T_image = new Texture(EnemyInventory[2][0].str);
	EnemyInventory[2][0].price = 100;




}

void Monster::Update()
{
	// 몬스터 인벤토리 아이템 업데이트

	EnemyInventory[0][0].image->Update();
	EnemyInventory[1][0].image->Update();
	EnemyInventory[1][1].image->Update();
	EnemyInventory[2][0].image->Update();
}

void Monster::Render()
{
	// 몬스터 인벤토리 아이템 렌더

	if (Inventorycheck[0])
	{
		EnemyInventory[0][0].image->SRV(EnemyInventory[0][0].T_image->SRV()); 
		EnemyInventory[0][0].image->Render();
	}

	if (Inventorycheck[1])
	{
		EnemyInventory[1][0].image->SRV(EnemyInventory[1][0].T_image->SRV()); // 첫번째
		EnemyInventory[1][0].image->Render();

		EnemyInventory[1][1].image->SRV(EnemyInventory[1][1].T_image->SRV()); // 두번째
		EnemyInventory[1][1].image->Render();
	}

	if (Inventorycheck[2])
	{
		EnemyInventory[2][0].image->SRV(EnemyInventory[2][0].T_image->SRV()); // 첫번째
		EnemyInventory[2][0].image->Render();
	}
	
}




