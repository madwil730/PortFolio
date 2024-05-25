#pragma once

#include "Systems/IExecute.h"

class DrawAnimation : public IExecute
{
public:
	virtual void Initialize() override;
	virtual void Ready() {}
	virtual void Destroy() {}
	virtual void Update()  override;
	virtual void PreRender() override;
	virtual void Render()  override;
	virtual void PostRender() {}
	virtual void ResizeScreen() {}

	bool movecheck = true;
	bool ComparisonCheck = false; // 비교설명문 나오기 위한 체크
	bool inventoryCheck = false; //플레이어 인벤토리 체크
	bool Maircheck = false; // 적 인벤토리 에서 마우스 아이템을 빈화면에 놓았을때 체크
	bool Kaircheck = false; // 내 인벤토리 에서 마우스 아이템을 빈화면에 놓았을때 체크
	bool MM = false; // 적 인벤토리 -> 내 인벤토리 마우스 체크
	bool EE = false; // 내 인벤토리 -> 장비창 마우스 체크
	bool KK = false; // 내 인벤토리 -> 스킬창 마우스 체크
	bool AttachCkeck = false; // 무기 바꾸기 체크

	bool Die = false;

	float a;

private:
	void Kachujin();	
	void ShadowKachujin();
	void Shops();
	void MutantAlgorithm();
	void BossAlgorithm();
	void SkillandItem();
	void mousemove();


private:
	Shader * WeaponSahder;
	Shader * shader;
	Shader * Shadowshader;
	Shader * bbShader;
	Shader * TerrainShader;
	Shader * ParticleShader;

	Render2D* P_Inventory; // 플레이어 인벤토리
	Render2D* E_Inventory; // 적 인베토리
	Render2D* S_Inventory; // 상인 인베토리
	Render2D* Equipment; // 장비창
	Render2D* render2D; // rendertargetview
	Render2D* Comparison2D; // 장비 성능 비교창

	Texture* T_P_inventory; // 플레이어 인벤토리
	Texture* T_E_inventory;	// 적 인베토리
	Texture* T_S_inventory;	// 상인 인베토리
	Texture* T_Equipment;	// 장비창
	Texture* T_Comparison2D; // 장비 비교창

	ModelAnimator* kachujin = NULL;	
	ModelAnimator* shadowkachujin = NULL;	
	ModelAnimator* Shop = NULL;	

	ModelRender* weapon;
	ModelRender* weapon2;
	ModelRender* ShadowWeapon;
	ModelRender* ShadowWeapon2;

	MeshRender* sphere; // 3개의 구체

	class Monster* monster;
	class Damage* damage;
	class Inventory* inventory;
	class Terrain* terrain;
	class ParticleEditor* particleEditor[4];
	class ParticleSystem* particle[7];
	class Player* player;

	Billboard* bb[5];
	Billboard* Bossbillboard; // 보스 체력 빌보드
	Billboard* tree; 
	Billboard* gress[20]; 

	int ma, mb; // 적 인벤토리 마우스 놓으면 2000,2000 으로 보내주기 위한 자료형
	int k; // 내 인벤토리 마우스 놓으면 2000,2000 으로 보내주기 위한 자료형
	float beAttackedTime = 0;
	int money = 900 ; // 돈
	char moneys[20]; // int -> string 
	
	int attack = 0;
	char C_attack[20];

	int defense = 0;
	char C_defense[20];
	

	Collider* Kcollider; // 플레이어 히트 박스
	Transform* Init; // 플레이어 보정
	Transform* Ktransform; // 플레이어 히트 박스

	Collider* Scollider; // 상점 히트 박스
	Transform* Stransform; // 상점 히트 박스

	Collider* Mcollider[5]; // 몬스터 히트 박스
	Transform* Mtransform[5]; // 몬스터 히트 박스

	Collider* BossCollider; // 보스 히트 박스
	Transform* BossTransform; // 보스 히트 박스

	Collider* PlayerSkill; // 스킬 콜라이더
	Collider* PlayerThrow[3]; // 투사체 콜라이더

	int throwcount = 2;
	bool throwcheck[3];

	Sky* sky;
	Vector3 Direction = {0,0,0}; //방향 정하기 용


	float orbitDis = 7;
	Vector3 KVector;
	Vector3 camera[32];

	Vector3 any[10];
	Vector3 CameraPosition;

	Collider* rect[10];

	float Dot;
	int MouseValueCount = 10;

	Vector3 T;
	Matrix test;

	Shadow* shadow;
};

//ImGui::LabelText("Camera Position", "%f,%f,%f", Context::Get()->GetCamera()->position.x, Context::Get()->GetCamera()->position.y, Context::Get()->GetCamera()->position.z);