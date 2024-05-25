#include "stdafx.h"
#include "DrawAnimation.h"
#include "Viewer/Freedom.h"
#include "Monster.h"
#include "Damage.h"
#include "Environment/Terrain.h"
#include "ParticleEditor.h"
#include "Inventory.h"
#include "Player.h"
#include <ctime>


void DrawAnimation::Initialize()
{
	srand((unsigned int)time(NULL));
	Context::Get()->GetCamera()->RotationDegree(0, 0, 0); // 마우스 움직이면 해당 값에 왼쪽 오른쪽 바꾸는 법 물어보기
	Context::Get()->GetCamera()->Position(1, 2, -3);
	((Freedom*)Context::Get()->GetCamera())->Speed(20, 2);

	//--------------- 초기화 --------------------------

	//WeaponSahder = new Shader(L"33_Animation.fx");
	shader = new Shader(L"43_Shadow.fxo");
	bbShader = new Shader(L"56_Billboard.fxo");
	ParticleShader = new Shader(L"57_ParticleViewer.fxo");
	TerrainShader = new Shader(L"23_TerrainSpatting.fx");

	terrain = new Terrain(TerrainShader, L"Terrain/Gray256.png");

	terrain->GetTransform()->Position(0, 0, 0);
	terrain->BaseMap(L"Terrain/Grass (Meadows).jpg");
	terrain->LayerMap(L"Terrain/Dirt3.png", L"Terrain/Splatting.png");

	particleEditor[0] = new ParticleEditor();

	particle[0] = new ParticleSystem(L"SwordEffect");
	particle[1] = new ParticleSystem(L"Skill1");
	particle[2] = new ParticleSystem(L"Skill2");
	particle[3] = new ParticleSystem(L"Skill3");
	particle[4] = new ParticleSystem(L"Skill3");
	particle[5] = new ParticleSystem(L"Skill3");
	particle[6] = new ParticleSystem(L"BossSkill");


	player = new Player(shader);

	Kachujin();
	ShadowKachujin();
	Shops();
	monster = new Monster();

	sphere = new MeshRender(shader, new MeshSphere(0.5f, 10, 10));

	for (int i = 0; i < 3; i++) // 초기화
	{
		sphere->AddTransform();
		sphere->GetTransform(i)->Position(2000, 2000, 2000);
		throwcheck[i] = true;
	}

	sky = new Sky(ParticleShader);
	sky->ScatteringPass(3);
	sky->RealTime(false, Math::PI - 1e-6f, 0.3f);

	damage = new Damage();
	inventory = new Inventory();

	weapon = new ModelRender(shader);
	weapon->ReadMaterial(L"Weapon/Sword");
	weapon->ReadMesh(L"Weapon/Sword");

	Transform attachTransform;
	attachTransform.Position(-10, 0, -10);
	attachTransform.Scale(0.5f, 0.5f, 0.5f);

	weapon2 = new ModelRender(shader);
	weapon2->ReadMaterial(L"Weapon/Katana");
	weapon2->ReadMesh(L"Weapon/Katana");

	ShadowWeapon2 = new ModelRender(shader);
	ShadowWeapon2->ReadMaterial(L"Weapon/Katana");
	ShadowWeapon2->ReadMesh(L"Weapon/Katana");

	kachujin->GetModel()->Attach(shader, weapon->GetModel(), 35, &attachTransform);

	//--------------- Render2D -------------------------- 

	P_Inventory = new Render2D();
	P_Inventory->GetTransform()->Position(1000, 400, 0);
	P_Inventory->GetTransform()->Scale(D3D::Width() * 0.2f, D3D::Height() * 0.5, 1);
	T_P_inventory = new Texture(L"P_inventory.png");

	E_Inventory = new Render2D();
	E_Inventory->GetTransform()->Position(200, 400, 0);
	E_Inventory->GetTransform()->Scale(D3D::Width() * 0.2f, D3D::Height() * 0.5, 1);
	T_E_inventory = new Texture(L"E_inventory.png");

	S_Inventory = new Render2D();
	S_Inventory->GetTransform()->Position(500, 400, 0);
	S_Inventory->GetTransform()->Scale(D3D::Width() * 0.2f, D3D::Height() * 0.5, 1);
	T_S_inventory = new Texture(L"S_inventory.png");

	Equipment = new Render2D();
	Equipment->GetTransform()->Position(500, 400, 0);
	Equipment->GetTransform()->Scale(D3D::Width() * 0.3f, D3D::Height() * 0.5, 1);
	T_Equipment = new Texture(L"Equipment.png");

	Comparison2D = new Render2D();
	Comparison2D->GetTransform()->Position(1000, 600, 0);
	Comparison2D->GetTransform()->Scale(D3D::Width() * 0.15f, D3D::Height() * 0.1, 1);
	T_Comparison2D = new Texture(L"status.png");

	render2D = new Render2D();
	render2D->GetTransform()->Position(480, D3D::Height() - 330, 0);
	render2D->GetTransform()->Scale(150, 320, 1);

	//--------------- Billboard --------------------------

	for (int i = 0; i < monster->EnemyNumber; i++)
	{
		bb[i] = new Billboard(bbShader);
		bb[i]->AddTexture(L"Green.png");
		bb[i]->Add(Vector3(0, 0, 0), Vector2(2, 0.5), 0);
		bb[i]->CreateBuffer();
	}


	tree = new Billboard(bbShader);
	tree->AddTexture(L"Terrain/Tree.png");


	tree->Add(Vector3(60, 10, 50), Vector2(10, 10), 0);
	tree->Add(Vector3(55, 10, 55), Vector2(10, 10), 1);
	tree->Add(Vector3(45, 10, 47), Vector2(10, 10), 2);
	tree->Add(Vector3(40, 10, 52), Vector2(10, 10), 3);
	tree->Add(Vector3(49, 10, 58), Vector2(10, 10), 4);

	//tree->GetTransform()->position.y = terrain->GetHeightPick(tree->GetTransform()->position);

	tree->CreateBuffer();


	Bossbillboard = new Billboard(bbShader);
	Bossbillboard->AddTexture(L"Green.png");
	Bossbillboard->Add(Vector3(0, 0, 0), Vector2(3, 0.5), 0);
	Bossbillboard->CreateBuffer();

	//--------------- Collider --------------------------

	Ktransform = new Transform();
	Init = new Transform();
	Init->Scale(100, 100, 50);
	Init->Position(-10, 0, -40);
	Kcollider = new Collider(Ktransform, Init);

	Stransform = new Transform();
	Stransform->Scale(1, 1.5, 1);
	Stransform->Position(100, 2, 65);
	Scollider = new Collider(Stransform);

	BossCollider = new Collider(new Transform());

	for (int i = 0; i < monster->EnemyNumber; i++)
	{
		Mtransform[i] = new Transform();
		Mtransform[i]->Scale(1, 1.5, 1);
		Mcollider[i] = new Collider(Mtransform[i]);
	}

	PlayerSkill = new Collider(new Transform());

	for (int i = 0; i < 3; i++)
	{
		PlayerThrow[i] = new Collider(new Transform());
	}

}

void DrawAnimation::Update()
{

	//----------------------- test----------------------
	Vector3 R;
	Context::Get()->GetCamera()->Rotation(&R);

	Vector3 val = Mouse::Get()->GetMoveValue();
	R.x = R.x + val.y * 2 * Time::Delta();
	R.y = R.y + val.x * 2 * Time::Delta();
	R.z = 0.0f;

	T; // 곱해줄 회적 각도

	T.x = sin(R.y) * cos(R.x); // 단위벡터이므로
	T.y = -sin(R.x) - 0.2;
	T.z = cos(R.y)*cos(R.x);
	if (Keyboard::Get()->Press('G')) // 가까워 지기
		orbitDis -= 10.0f*Time::Delta();
	else if (Keyboard::Get()->Press('H')) // 멀어지기
		orbitDis += 10.0f*Time::Delta();

	if (!inventoryCheck)
	{

		if (T.y < 0.12 && T.y > -0.8)
		{
			kachujin->GetTransform(0)->Rotation(0, R.y + 3, 0);
			Context::Get()->GetCamera()->Rotation(R.x, R.y, 0);
			Context::Get()->GetCamera()->Position(kachujin->GetTransform(0)->GetPosition() - (T*orbitDis));
		}

		////ImGui::LabelText("mouse val ", "%f,%f,%f", val.x, val.y, val.z); // x 측 오른쪽 + 왼쪽 -
		//ImGui::LabelText("T Value ", "%f,%f,%f", T.x, T.y, T.z); // x 측 오른쪽 + 왼쪽 -
		//ImGui::LabelText("Camera Position", "%f,%f,%f", Context::Get()->GetCamera()->position.x, Context::Get()->GetCamera()->position.y, Context::Get()->GetCamera()->position.z);
		//ImGui::LabelText("kachujin Rotation", "%f,%f,%f", kachujin->GetTransform(0)->rotation.x, kachujin->GetTransform(0)->rotation.y, kachujin->GetTransform(0)->rotation.z);
	}
	ImGui::LabelText("inventoryCheck", "%d", inventoryCheck);
	//----------------------------이동---------------------

	{
		static float theta = Math::PI - 1e-6f;
		sky->Theata(theta);
	}
	if (!Die)
	{
		if (Mouse::Get()->Press(1) == false)
		{
			const Vector3& F = Context::Get()->GetCamera()->Foward();
			const Vector3& R = Context::Get()->GetCamera()->Right();

			// 달리면 달리기 모션 안 달리면 idle 모션

			if (Keyboard::Get()->Up('W') || Keyboard::Get()->Up('S') || Keyboard::Get()->Up('A') || Keyboard::Get()->Up('D'))
			{
				kachujin->PlayClip(0, 0, 1.0f, 0.7f);
				movecheck = true;
			}

			// 이동
			Context::Get()->GetCamera()->Position(&CameraPosition);

			if (Keyboard::Get()->Press('W')) {

				kachujin->GetTransform(0)->position += Vector3(F.x, 0.0f, F.z) * 10 * Time::Delta();

				Direction = { 0,0,1 };

				if (movecheck)
				{
					kachujin->PlayClip(0, 1, 1.0f, 0.7f);
					movecheck = false;
				}
			}

			if (Keyboard::Get()->Press('S')) {

				kachujin->GetTransform(0)->position += Vector3(-F.x, 0.0f, -F.z) * 10 * Time::Delta();

				Direction = { 0,0,-1 };
				kachujin->GetTransform(0)->Rotation(0, R.y + 5, 0); 

				if (movecheck)
				{
					kachujin->PlayClip(0, 1, 1.0f, 0.7f);
					movecheck = false;
				}
			}


			if (Keyboard::Get()->Press('A')) {

				kachujin->GetTransform(0)->position += -R * 10 * Time::Delta();

				Direction = { -1,0,0 };
				kachujin->GetTransform(0)->Rotation(0, 8.2, 0);

				if (movecheck)
				{
					kachujin->PlayClip(0, 1, 1.0f, 0.7f);
					movecheck = false;
				}
			}


			if (Keyboard::Get()->Press('D')) {

				kachujin->GetTransform(0)->position += R * 10 * Time::Delta();
				Direction = { 1,0,0 };
				kachujin->GetTransform(0)->Rotation(0, 11.4, 0);

				if (movecheck)
				{
					kachujin->PlayClip(0, 1, 1.0f, 0.7f);
					movecheck = false;
				}

			}
		}
	}

	if (Keyboard::Get()->Down('I')) inventoryCheck = !inventoryCheck; // 인벤토리 활성 시

	if (Keyboard::Get()->Down('P')) inventory->Equipmentcheck = !inventory->Equipmentcheck; // 인벤토리 활성 시

	if (Kcollider->IsIntersect(Scollider) && Keyboard::Get()->Down('E')) inventory->ShopInventorycheck = !inventory->ShopInventorycheck; //상인 인벤토리 활성 키

	else if (Kcollider->IsIntersect(Scollider) == false) inventory->ShopInventorycheck = false; //상인 인벤토리 비활성 키

	for (int i = 0; i < 3; i++) // 몬스터 수 조절
	{
		if (Keyboard::Get()->Down('E') && monster->Mchoice[i] == 2 && kachujin->DistanceCollider->IsIntersect(Mcollider[i])) monster->Inventorycheck[i] = !monster->Inventorycheck[i]; // 적 시체에서 아이템 확인시

		else if (kachujin->DistanceCollider->IsIntersect(Mcollider[i]) == false) monster->Inventorycheck[i] = false;
	}

	SkillandItem(); // 스킬 & 아이템 사용
	mousemove(); // 마우스 이동

	//--------------------------update----------------------------------
	//Billboard
	for (int i = 0; i < 3; i++) // 몬스터 수 조절
	{
		bb[i]->vertices[0].Position = Vector3(monster->mutant->GetTransform(i)->position.x, 3, monster->mutant->GetTransform(i)->position.z);
		bb[i]->Update();
	}

	Bossbillboard->vertices[0].Position = Vector3(monster->boss->GetTransform(0)->position.x, 6, monster->boss->GetTransform(0)->position.z);
	Bossbillboard->Update();

	//tree->GetTransform()->position.y = terrain->GetHeightPick(tree->GetTransform()->position);
	//tree->GetTransform()->UpdateWorld();
	tree->Update();

	//model
	kachujin->GetTransform(0)->position.y = terrain->GetHeightPick(kachujin->GetTransform(0)->position);
	kachujin->UpdateTransforms();
	kachujin->Update();

	shadowkachujin->Update(); // rendertarget
	Shop->Update();

	sphere->GetTransform(0)->UpdateWorld();
	sphere->GetTransform(1)->UpdateWorld();
	sphere->GetTransform(2)->UpdateWorld();

	sphere->UpdateTransforms();
	sphere->Update();

	monster->sphere->GetTransform(0)->UpdateWorld();
	monster->sphere->UpdateTransforms();
	monster->sphere->Update();

	monster->boss->GetTransform(0)->position.y = terrain->GetHeightPick(monster->boss->GetTransform(0)->position);
	monster->boss->UpdateTransforms();
	monster->boss->Update();

	for (int i = 0; i < 3; i++) // 몬스터 수 조절
	{
		monster->mutant->GetTransform(i)->position.y = terrain->GetHeightPick(monster->mutant->GetTransform(i)->position);
	}

	for (int i = 0; i < 3; i++) // 몬스터 수 조절
	{
		if (monster->Mchoice[i] == 2)
			monster->mutant->OneFrame(i,true);
		else 
			monster->mutant->OneFrame(i,false);
		
	}

	//render 2D
	P_Inventory->Update(); // 플레이어 인벤토리 업데이트
	E_Inventory->Update(); // 몬스터 인벤토리 업데이트
	S_Inventory->Update(); // 상인 인벤토리 업데이트
	Equipment->Update(); // 장비창 업데이트
	Comparison2D->Update();

	damage->Health->Update(); // 내체력
	damage->Hit->Update(); // 적 체력
	damage->Skill->Update(); // 스킬창

	for (int i = 0; i < 16; i++)
	{
		inventory->myInventory[i].image->Update(); // 플레이어 인벤토리 아이템 업데이트
	}

	for (int i = 0; i < 6; i++)
	{
		inventory->mySkill[i].image->Update(); // 스킬 UI 창 업데이트
	}

	monster->Update();
	inventory->Update();
	render2D->Update(); // Rendertarget Update

	//Collider

	Matrix attach = kachujin->GetAttachTransform(0);
	Kcollider->GetTransform()->World(attach);


	Kcollider->Update();
	Scollider->Update();

	PlayerSkill->Update();
	monster->ColliderBossSkill->Update();
	monster->SphereCollider->Update();

	BossCollider->GetTransform()->Position(monster->boss->GetTransform(0)->GetPosition());
	BossCollider->GetTransform()->Scale(1, 2, 1);

	for (int i = 0; i < 3; i++) // 몬스터 수 조절
	{
		Vector3 temp = monster->mutant->GetTransform(i)->GetPosition();

		Mtransform[i]->Position(temp.x, temp.y + 1, temp.z);
		Mcollider[i]->Update();
	}

	for (int i = 0; i < 3; i++)
	{
		PlayerThrow[i]->Update();
	}

	kachujin->DistanceCollider->GetTransform()->Position(kachujin->GetTransform(0)->GetPosition());
	kachujin->DistanceCollider->GetTransform()->Scale(1, 3, 1);
	kachujin->DistanceCollider->Update();

	//Etc
	sky->Update();
	terrain->Update();

	//particleEditor[0]->SetSkill(Kcollider->GetTransform()->position);
	//particleEditor[0]->Update();

	particle[0]->Add(Kcollider->GetTransform()->position); // 기본 칼 이펙트

	if (player->attack == 1) // 스킬 1번
		particle[1]->Add(PlayerSkill->GetTransform()->position);

	if (player->attack == 2) // 스킬 2번
		particle[2]->Add(PlayerSkill->GetTransform()->position);

	particle[3]->Add(PlayerThrow[0]->GetTransform()->position); // 스킬 3번
	particle[4]->Add(PlayerThrow[1]->GetTransform()->position); // 스킬 3번
	particle[5]->Add(PlayerThrow[2]->GetTransform()->position); // 스킬 3번
	particle[6]->Add(monster->ColliderBossSkill->GetTransform()->position); // 스킬 3번

	for (int i = 0; i < 7; i++)
	{
		particle[i]->Update();
	}


	MutantAlgorithm(); // 뮤턴트 알고리즘 업데이트
	BossAlgorithm(); // 보스 알고리즘 업데이트
}

void DrawAnimation::PreRender()
{
	player->SetRenderTarget(shadowkachujin); 
	sky->PreRender();
}

void DrawAnimation::Render()
{
	//Billboard
	for (int i = 0; i < 3; i++) // 몬스터 수 조절
	{
		bb[i]->Apply();
		bb[i]->Render();
	}

	Bossbillboard->Apply();
	Bossbillboard->Render();
	//tree->Render();

	//Collider
	Kcollider->Render(Color(1, 0, 0, 1));
	Scollider->Render(Color(1, 0, 0, 1));
	PlayerSkill->Render(Color(0.5, 0, 0, 1));
	BossCollider->Render(Color(1, 0, 0, 1));

	monster->ColliderBossSkill->Render(Color(0.5, 0.7, 0, 1));
	monster->SphereCollider->Render(Color(1, 0, 0, 1));

	for (int i = 0; i < 3; i++)
	{
		PlayerThrow[i]->Render(Color(1, 0, 0, 1));
	}

	for (int i = 0; i < 3; i++) // 몬스터 수 조절
	{
		
		Mcollider[i]->Render(Color(0, 1, 0, 1));
	}

	kachujin->DistanceCollider->Render(Color(1, 0, 0, 1));
	monster->boss->DistanceCollider->Render(Color(1, 0.3, 0, 1));
	monster->boss->HitDistanceCollider->Render(Color(1, 0, 0, 1));
	monster->boss->LongCollider->Render(Color(1, 0.8, 0, 1));

	//Model
	kachujin->Pass(5);
	kachujin->Render();

	Shop->Render();

	monster->mutant->Render();
	monster->boss->Render();

	sphere->Render();
	sphere->Pass(3);

	monster->sphere->Render();

	//etc
	sky->Pass(4, 5, 6);
	sky->Render();

	terrain->Render();

	//render2D
	if (inventoryCheck) {

		P_Inventory->SRV(T_P_inventory->SRV());
		P_Inventory->Render();
	}

	if (inventory->Equipmentcheck) {

		Equipment->SRV(T_Equipment->SRV());
		Equipment->Render();
	}

	if (inventory->ShopInventorycheck) {

		S_Inventory->SRV(T_S_inventory->SRV());
		S_Inventory->Render();
	}

	for (int i = 0; i < monster->EnemyNumber; i++)
	{
		if (monster->Inventorycheck[i])
		{
			E_Inventory->SRV(T_E_inventory->SRV());
			E_Inventory->Render();
		}
	}

	if (ComparisonCheck) // 비교 렌더 2D
	{
		Comparison2D->SRV(T_Comparison2D->SRV());
		Comparison2D->Render();

		sprintf(C_attack, "%d", attack); // int -> string
		sprintf(C_defense, "%d", defense); // int -> string
		Gui::Get()->RenderText(952, 119, 245 / 255, 63 / 255, 1, C_attack);
		Gui::Get()->RenderText(952, 134, 20 / 255, 20 / 255, 1, C_defense);

	}

	damage->Health->SRV(damage->T_Health->SRV());
	damage->Health->Render();

	damage->Hit->SRV(damage->T_Hit->SRV());
	damage->Hit->Render();

	damage->Skill->SRV(damage->T_Skill->SRV());
	damage->Skill->Render();

	if (inventoryCheck)
	{
		for (int i = 0; i < 16; i++)
		{
			inventory->myInventory[i].image->SRV(inventory->myInventory[i].T_image->SRV()); // 플레이어 인벤토리 아이템 렌더
			inventory->myInventory[i].image->Render();
		}

		sprintf(moneys, "GOLD : %d", money); // int -> string
		Gui::Get()->RenderText(1058, 157, 1, 1, 102 / 255, moneys);

	}

	for (int i = 0; i < 6; i++)
	{
		inventory->mySkill[i].image->SRV(inventory->mySkill[i].T_image->SRV()); // 스킬 UI 렌더
		inventory->mySkill[i].image->Render();
	}

	monster->Render();
	inventory->Render();

	if (inventory->Equipmentcheck)
	{
		render2D->SRV(player->renderTarget->SRV()); // 플레이어 초상화
		render2D->Render();
	}

	// Particle
	// particleEditor[0]->Render();

	for (int i = 0; i < 7; i++)
	{
		particle[i]->Render();
	}

	Vector3 temp = Mouse::Get()->GetPosition();
	temp.y = D3D::Height() - temp.y;

	ImGui::LabelText("mouse", "%f,%f,%f", temp.x, temp.y, temp.z);

}


void DrawAnimation::Kachujin()
{
	kachujin = new ModelAnimator(shader);
	kachujin->ReadMaterial(L"Kachujin/Mesh");
	kachujin->ReadMesh(L"Kachujin/Mesh");
	kachujin->ReadClip(L"Kachujin/Idle"); //0
	kachujin->ReadClip(L"Kachujin/Running"); //1
	kachujin->ReadClip(L"Kachujin/Attack"); //2
	kachujin->ReadClip(L"Kachujin/Skill"); //3
	kachujin->ReadClip(L"Kachujin/Skill2"); //4
	kachujin->ReadClip(L"Kachujin/Skill3"); //5
	kachujin->ReadClip(L"Kachujin/Die"); //6

	Transform* transform = kachujin->AddTransform();
	transform->Position(100, 0, 60);
	transform->Scale(0.01f, 0.01f, 0.01f);
	transform->Rotation(0, 0.5, 0);

	kachujin->UpdateTransforms();
}

void DrawAnimation::ShadowKachujin()
{
	shadowkachujin = new ModelAnimator(shader);
	shadowkachujin->ReadMaterial(L"Kachujin/Mesh");
	shadowkachujin->ReadMesh(L"Kachujin/Mesh");
	shadowkachujin->ReadClip(L"Kachujin/Idle"); //0

	ShadowWeapon = new ModelRender(shader);
	ShadowWeapon->ReadMaterial(L"Weapon/Sword");
	ShadowWeapon->ReadMesh(L"Weapon/Sword");

	Transform attachTransform;
	attachTransform.Position(-10, 0, -10);
	attachTransform.Scale(0.5f, 0.5f, 0.5f);

	shadowkachujin->GetModel()->Attach(shader, ShadowWeapon->GetModel(), 35, &attachTransform);

	shadowkachujin->AddTransform();
	shadowkachujin->GetTransform(0)->Position(40, 0, 40);
	shadowkachujin->GetTransform(0)->Scale(0.01f, 0.01f, 0.01f);
	shadowkachujin->GetTransform(0)->Rotation(0, 4, 0);
	shadowkachujin->UpdateTransforms();
}

void DrawAnimation::Shops()
{
	Shop = new ModelAnimator(shader);
	Shop->ReadMaterial(L"Ninja/Mesh");
	Shop->ReadMesh(L"Ninja/Mesh");
	Shop->ReadClip(L"Ninja/Nidle");

	Transform* transform = Shop->AddTransform();
	transform->Position(100, 1, 65);
	transform->Scale(0.01f, 0.01f, 0.01f);

	Shop->GetTransform(0)->position.y = terrain->GetHeightPick(Shop->GetTransform(0)->position);
	Shop->UpdateTransforms();

	Shop->Pass(5);
}

Vector3 F[3];
Vector3 R[3];
void DrawAnimation::SkillandItem()
{
	Vector3 Ktemp = kachujin->GetTransform(0)->GetPosition();

	// 공격 버튼
	if (Mouse::Get()->Down(0)) // 마우스 왼쪽버튼
	{
		kachujin->PlayClip(0, 2, 1.0f, 1.5);
		player->attack = 0;
	}
	else if (Keyboard::Get()->Down('1'))
	{
		kachujin->PlayClip(0, 3, 1.0f, 1.0f);
		player->attack = 1;
		PlayerSkill->GetTransform()->Position(Ktemp.x + 5 * Direction.x, Ktemp.y + 2, Ktemp.z + 5 * Direction.z);
		PlayerSkill->GetTransform()->Scale(2, 2, 2);
	}
	else if (Keyboard::Get()->Down('2'))
	{
		kachujin->PlayClip(0, 4, 1.0f, 2.0f);
		player->attack = 2;
		PlayerSkill->GetTransform()->position.x = 10000;
	}
	else if (Keyboard::Get()->Down('3'))
	{
		throwcount++;
		if (throwcount > 3) throwcount = 0;

		for (int i = 0; i < 3; i++)
		{
			if (throwcount == i) // 첫번째 구
			{
				throwcheck[i] = true;
				F[i] = Context::Get()->GetCamera()->Foward();
				R[i] = Context::Get()->GetCamera()->Right();
			}
		}

		if (throwcount == 3) // 리로드
		{
			kachujin->PlayClip(0, 5, 1.0f, 1.0f);
			player->attack = 3;

			for (int i = 0; i < 3; i++)
				throwcheck[i] = false;
		}
	}

	// 공격 모션 호출
	if (player->attack == 0) // 일반공격
	{
		player->Attack(kachujin, 2);
	}

	else if (player->attack == 1) //스킬 1번
	{
		player->Attack(kachujin, 4.5);
	}

	else if (player->attack == 2 && PlayerSkill->GetTransform()->position.x != 1000) //스킬 2번
	{
		player->Attack(kachujin, 6);
		PlayerSkill->GetTransform()->Position(Ktemp.x + 1 * T.x, Ktemp.y + 1, Ktemp.z + 1 * T.z);
		PlayerSkill->GetTransform()->Rotation(0, kachujin->GetTransform(0)->rotation.y, 0);
		PlayerSkill->GetTransform()->Scale(2, 2, 3);
	}

	else if (player->attack == 3) //스킬 3번
	{
		player->Attack(kachujin, 3);

	}

	// 스킬 사라지기
	if (player->attackTime > 4 && player->attack == 1)
		PlayerSkill->GetTransform()->Position(1500, 0, 1500);

	else if (player->attackTime > 5.95 && player->attack == 2)
		PlayerSkill->GetTransform()->Position(1000, 0, 1000);

	//-------------------던지기 체크---------------

	for (int i = 0; i < 3; i++)
	{
		PlayerThrow[i]->GetTransform()->Position(sphere->GetTransform(i)->GetPosition());
		PlayerThrow[i]->GetTransform()->Scale(1.5, 1.5, 1.5);
	}

	if (throwcheck[0])
	{
		sphere->GetTransform(0)->position += Vector3(F[0].x, 0.0f, F[0].z) * 1;

	}
	else if (!throwcheck[0])
	{
		sphere->GetTransform(0)->Position(Ktemp.x - 2, Ktemp.y + 1, Ktemp.z);
	}

	if (throwcheck[1])
	{
		sphere->GetTransform(1)->position += Vector3(F[1].x, 0.0f, F[1].z) * 1;

	}
	else if (!throwcheck[1])
	{
		sphere->GetTransform(1)->Position(Ktemp.x, Ktemp.y + 3, Ktemp.z);
	}

	if (throwcheck[2])
	{
		sphere->GetTransform(2)->position += Vector3(F[2].x, 0.0f, F[2].z) * 1;
	}
	else if (!throwcheck[2])
	{
		sphere->GetTransform(2)->Position(Ktemp.x + 2, Ktemp.y + 1, Ktemp.z);
	}

	// 아이템 먹기
	if (Keyboard::Get()->Down('4'))
	{
		if (inventory->mySkill[3].str == L"redPortion.png")
		{
			damage->Hit->GetTransform()->scale.x -= 10;
			damage->Hit->GetTransform()->position.x += 5;
			damage->Hit->GetTransform()->UpdateWorld();

			inventory->mySkill[3].image->GetTransform()->Position(2000, 0, 0);
		}
	}

	// 아이템 먹기
	if (Keyboard::Get()->Down('5'))
	{
		if (inventory->mySkill[4].str == L"redPortion.png")
		{
			damage->Hit->GetTransform()->scale.x -= 10;
			damage->Hit->GetTransform()->position.x += 5;
			damage->Hit->GetTransform()->UpdateWorld();
		}
	}
}

void DrawAnimation::mousemove()
{
	Vector3 temp = Mouse::Get()->GetPosition();
	temp.y = D3D::Height() - temp.y;

	// 적 인벤토리 -> 내 인벤토리
	if (Mouse::Get()->Press(0))
	{
		for (int i = 0; i < monster->EnemyNumber; i++)
		{
			if (monster->Inventorycheck[i])
			{
				for (int j = 0; j < 16; j++)
				{
					if (temp.x > monster->EnemyInventory[i][j].image->GetTransform()->GetPosition().x - 25 &&
						temp.x < monster->EnemyInventory[i][j].image->GetTransform()->GetPosition().x + 25 &&
						temp.y > monster->EnemyInventory[i][j].image->GetTransform()->GetPosition().y - 30 &&
						temp.y < monster->EnemyInventory[i][j].image->GetTransform()->GetPosition().y + 30)
					{
						monster->EnemyInventory[i][j].image->GetTransform()->Position(temp.x, temp.y, temp.z);
						monster->EnemyInventory[i][j].image->GetTransform()->UpdateWorld();

						ma = i;
						mb = j;
						MM = true;
						Maircheck = true;
					}
				}
			}
		}
	}

	else if (Mouse::Get()->Up(0))
	{
		if (inventoryCheck) {

			for (int i = 0; i < 16; i++)
			{
				if (temp.x > inventory->myInventory[i].x - 25 && temp.x < inventory->myInventory[i].x + 25 &&
					temp.y > inventory->myInventory[i].y - 30 && temp.y < inventory->myInventory[i].y + 30)
				{
					inventory->myInventory[i].image->GetTransform()->Position(inventory->myInventory[i].x, inventory->myInventory[i].y, 0);
					inventory->myInventory[i].str = monster->EnemyInventory[ma][mb].str;
					inventory->myInventory[i].price = monster->EnemyInventory[ma][mb].price;
					inventory->myInventory[i].T_image = new Texture(inventory->myInventory[i].str);

					monster->EnemyInventory[ma][mb].image->GetTransform()->Position(2000, 2000, 0);
					Maircheck = false;
				}
			}
		}
		if (Maircheck) monster->EnemyInventory[ma][mb].image->GetTransform()->Position(monster->EnemyInventory[ma][mb].x, monster->EnemyInventory[ma][mb].y, 0);

	}

	// 인벤토리 -> 스킬 창
	if (inventoryCheck)
	{
		if (Mouse::Get()->Press(0))
		{
			for (int i = 0; i < 16; i++)
			{
				if (temp.x > inventory->myInventory[i].image->GetTransform()->GetPosition().x - 25 &&
					temp.x < inventory->myInventory[i].image->GetTransform()->GetPosition().x + 25 &&
					temp.y > inventory->myInventory[i].image->GetTransform()->GetPosition().y - 30 &&
					temp.y < inventory->myInventory[i].image->GetTransform()->GetPosition().y + 30)
				{
					inventory->myInventory[i].image->GetTransform()->Position(temp.x, temp.y, temp.z);
					inventory->myInventory[i].image->GetTransform()->UpdateWorld();

					k = i;
					Kaircheck = true;
					KK = true;
				}
			}
		}
		else if (Mouse::Get()->Up(0) && KK)
		{
			for (int i = 0; i < 6; i++)
			{
				if (temp.x > inventory->mySkill[i].x - 30 && temp.x < inventory->mySkill[i].x + 30 &&
					temp.y > inventory->mySkill[i].y - 15 && temp.y < inventory->mySkill[i].y + 15)
				{
					inventory->mySkill[i].image->GetTransform()->Position(inventory->mySkill[i].x, inventory->mySkill[i].y, 0);
					inventory->mySkill[i].image->GetTransform()->Scale(30, 30, 0);
					inventory->mySkill[i].str = inventory->myInventory[k].str;
					inventory->mySkill[i].T_image = new Texture(inventory->mySkill[i].str);
					inventory->myInventory[k].image->GetTransform()->Position(2000, 2000, 0);

					Kaircheck = false;
				}
			}

			if (Kaircheck) inventory->myInventory[k].image->GetTransform()->Position(inventory->myInventory[k].x, inventory->myInventory[k].y, 0);
			KK = false;
		}
	}

	//아이템 비교 설명문

	if (inventoryCheck)
	{
		if (Mouse::Get()->Down(1))
		{
			for (int i = 0; i < 16; i++)
			{
				if (temp.x > inventory->myInventory[i].image->GetTransform()->GetPosition().x - 25 &&
					temp.x < inventory->myInventory[i].image->GetTransform()->GetPosition().x + 25 &&
					temp.y > inventory->myInventory[i].image->GetTransform()->GetPosition().y - 30 &&
					temp.y < inventory->myInventory[i].image->GetTransform()->GetPosition().y + 30)
				{
					if (inventory->myInventory[i].str == L"ring1.png")
					{
						attack = inventory->gear[4].Attack;
						defense = inventory->gear[4].Defense;
						ComparisonCheck = !ComparisonCheck;
					}

					else if (inventory->myInventory[i].str == L"ring2.png")
					{
						attack = -1 * attack + inventory->gear[5].Attack;
						defense = -1 * defense + inventory->gear[5].Defense;
						ComparisonCheck = !ComparisonCheck;
					}

					else if (inventory->myInventory[i].str == L"Sword.png")
					{
						attack = inventory->gear[3].Attack;
						defense = inventory->gear[3].Defense;
						ComparisonCheck = !ComparisonCheck;
					}
				}
			}
		}
	}

	// 내 인벤토리 -> 장비창
	if (inventoryCheck && inventory->Equipmentcheck)
	{
		if (Mouse::Get()->Press(0))
		{
			for (int i = 0; i < 16; i++)
			{
				if (temp.x > inventory->myInventory[i].image->GetTransform()->GetPosition().x - 25 &&
					temp.x < inventory->myInventory[i].image->GetTransform()->GetPosition().x + 25 &&
					temp.y > inventory->myInventory[i].image->GetTransform()->GetPosition().y - 30 &&
					temp.y < inventory->myInventory[i].image->GetTransform()->GetPosition().y + 30)
				{
					inventory->myInventory[i].image->GetTransform()->Position(temp.x, temp.y, temp.z);
					inventory->myInventory[i].image->GetTransform()->UpdateWorld();

					k = i;
					Kaircheck = true;
					EE = true;
				}
			}
		}
		else if (Mouse::Get()->Up(0) && EE)
		{
			for (int i = 0; i < 7; i++)
			{
				if (temp.x > inventory->equipment[i].x - 20 && temp.x < inventory->equipment[i].x + 20 &&
					temp.y > inventory->equipment[i].y - 25 && temp.y < inventory->equipment[i].y + 25)
				{
					inventory->equipment[i].image->GetTransform()->Position(inventory->equipment[i].x, inventory->equipment[i].y, 0);
					inventory->equipment[i].image->GetTransform()->Scale(30, 30, 0);
					inventory->equipment[i].str = inventory->myInventory[k].str;
					inventory->equipment[i].T_image = new Texture(inventory->equipment[i].str);
					inventory->myInventory[k].image->GetTransform()->Position(2000, 2000, 0);

					if (inventory->myInventory[k].str == L"ring1.png")
					{
						inventory->goods[4].Attack = 2;
						inventory->goods[4].Defense = 0;
					}

					else if (inventory->myInventory[k].str == L"ring2.png")
					{
						inventory->goods[4].Attack = 1;
						inventory->goods[4].Defense = 1;
					}

					else if (inventory->myInventory[k].str == L"Sword.png") // 칼 바꾸기
					{
						inventory->goods[3].Attack = 3;
						inventory->goods[3].Defense = 0;

						Transform attachTransform;
						attachTransform.Position(-10, 0, -10);
						attachTransform.Scale(1, 1, 1);

						kachujin->GetModel()->Attach(shader, weapon2->GetModel(), 35, &attachTransform);
						kachujin->CreateTexture();

						shadowkachujin->GetModel()->Attach(shader, weapon2->GetModel(), 35, &attachTransform);
						shadowkachujin->CreateTexture();

						Ktransform = new Transform();
						Init = new Transform();
						Init->Scale(100, 100, 100);
						Init->Position(-10, 0, -40);
						Kcollider = new Collider(Ktransform, Init);

						AttachCkeck = true;
					}

					Kaircheck = false;
				}
			}

			if (Kaircheck) inventory->myInventory[k].image->GetTransform()->Position(inventory->myInventory[k].x, inventory->myInventory[k].y, 0);
			EE = false;
		}
	}

	// 시장거래
	if (inventory->ShopInventorycheck && inventoryCheck)
	{
		if (Mouse::Get()->Down(1))
		{
			for (int i = 0; i < 8; i++) // 사기
			{
				if (temp.x > inventory->shopInventory[i].image->GetTransform()->GetPosition().x - 25 &&
					temp.x < inventory->shopInventory[i].image->GetTransform()->GetPosition().x + 25 &&
					temp.y > inventory->shopInventory[i].image->GetTransform()->GetPosition().y - 30 &&
					temp.y < inventory->shopInventory[i].image->GetTransform()->GetPosition().y + 30)
				{
					if (money - inventory->shopInventory[i].price > 0) { //  살 수 있는 가격이면

						for (int j = 0; j < 16; j++)
						{
							if (inventory->myInventory[j].price == 0)
							{
								inventory->myInventory[j].image->GetTransform()->Position(inventory->myInventory[j].x, inventory->myInventory[j].y, 0);
								inventory->myInventory[j].str = inventory->shopInventory[i].str;
								inventory->myInventory[j].T_image = new Texture(inventory->myInventory[j].str);
								inventory->myInventory[j].price = inventory->shopInventory[i].price;

								money = money - inventory->shopInventory[i].price;
								break;
							}
						}
					}

					break;
				}
			}

			for (int i = 0; i < 16; i++) // 팔기
			{
				if (temp.x > inventory->myInventory[i].image->GetTransform()->GetPosition().x - 25 &&
					temp.x < inventory->myInventory[i].image->GetTransform()->GetPosition().x + 25 &&
					temp.y > inventory->myInventory[i].image->GetTransform()->GetPosition().y - 30 &&
					temp.y < inventory->myInventory[i].image->GetTransform()->GetPosition().y + 30)
				{
					inventory->myInventory[i].image->GetTransform()->Position(2000, 2000, 0);
					inventory->myInventory[i].image->GetTransform()->UpdateWorld();
					money = money + inventory->myInventory[i].price;
					inventory->myInventory[i].price = 0;
					break;
				}
			}
		}
	}
}

void DrawAnimation::BossAlgorithm()
{
	//--------------보스 알고리즘--------------

	// 몬스터 활동
	if (monster->Bosschoice == 0)
		monster->BossInstruction(kachujin, monster->boss, 0, Bossbillboard);

	//몬스터 죽음
	if (Bossbillboard->vertices[0].Scale.x < 0 && monster->Bosschoice == 0)
	{
		monster->BossInstruction(kachujin, monster->boss, 0, false); // 평소 해동 비활성
		monster->boss->PlayClip(0, 4, 1.0f, 0.7f);
		monster->boss->onAttack = false;
		monster->Bosschoice = 1;
	}

	// 몬스터 공격
	if (monster->boss->onAttack) // 몬스터 타격시  플레이어 체력 깎임
		damage->BeAttacked(monster->boss, 1.8, 3.5);
	
	if (Bossbillboard->vertices[0].Scale.x < 1.5)
	{
		if (monster->ColliderBossSkill->IsIntersect(kachujin->DistanceCollider))
		{
			damage->BeAttacked(monster->boss, 1, 2, 6, 3.3);
		}
	}

	if (monster->SphereCollider->IsIntersect(kachujin->DistanceCollider))
	{
		monster->SphereCollider->GetTransform()->Position(1000, 1000, 0);
		monster->LongCheck = false;
		
		damage->Hit->GetTransform()->scale.x += 8; // 5
		damage->Hit->GetTransform()->position.x -= 5.5; // 2.5
		damage->Hit->GetTransform()->UpdateWorld();
	}
		

	// 플레이어 공격

	if (PlayerSkill->IsIntersect(BossCollider) && player->attackTime > 0.5)
	{
		Bossbillboard->vertices[0].Scale.x -= (0.3 + inventory->goods[3].Attack + inventory->goods[4].Attack) * Time::Delta();
	}

	else if (Kcollider->IsIntersect(BossCollider) && player->attackTime > 0.5) // 만약 플레이어에게 공격당하면 
	{
		Bossbillboard->vertices[0].Scale.x -= 0.5 * Time::Delta();
	}

	/*for (int i = 0; i < 3; i++)
	{
		if (PlayerThrow[i]->IsIntersect(BossCollider) && player->attackTime > 0.1)
			Bossbillboard->vertices[0].Scale.x -= 1 * Time::Delta();
	}*/

	//ImGui::LabelText("bossbillboard", "%f", Bossbillboard->vertices[0].Scale.x);

}

void DrawAnimation::MutantAlgorithm()
{
	if (damage->Hit->GetTransform()->scale.x > 1000 && Die == false)
	{
		kachujin->PlayClip(0, 6, 1.0f, 1);
		Die = true;
	}
	//--------------------------몬스터 알고리즘----------------------------------
	//행동
	for (int i = 0; i < monster->EnemyNumber; i++)
	{
		if (monster->Mchoice[i] == 0) monster->Instruction(kachujin, monster->mutant, i); // 평상시 몬스터 행동

		//몬스터 죽음
		if (bb[i]->vertices[0].Scale.x < 0 && monster->Mchoice[i] == 0)
		{
			monster->Instruction(kachujin, monster->mutant, i, false); // 평소 해동 비활성
			monster->mutant->PlayClip(i, 3, 1.0f, 0.7f);
			monster->onAttack[i] = false;
			monster->Mchoice[i] = 2;
		}

		//플레이어 공격
		if (Kcollider->IsIntersect(Mcollider[i]) && player->attackTime > 0.5) // 만약 플레이어에게 공격당하면 
		{
			bb[i]->vertices[0].Scale.x -= (0.5 + inventory->goods[3].Attack + inventory->goods[4].Attack) * Time::Delta();
		}

		// 플레이어 스킬
		else if (PlayerSkill->IsIntersect(Mcollider[i]) && player->attackTime > 0.5)
		{
			bb[i]->vertices[0].Scale.x -= 2 * Time::Delta();
		}

		for (int j = 0; j < 3; j++)
		{
			if (PlayerThrow[j]->IsIntersect(Mcollider[i]))
				bb[i]->vertices[0].Scale.x -= 5;
		}

		//몬스터 공격
		if (monster->onAttack[i]) // 몬스터 타격시  플레이어 체력 깎임
			damage->BeAttacked(monster->mutant, 1.8, 3.5, 5 - inventory->goods[4].Defense, 2.5 - inventory->goods[4].Defense);
	}

	ImGui::LabelText("Scale", "%f", damage->Hit->GetTransform()->scale.x);
}




