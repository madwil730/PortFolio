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
	bool ComparisonCheck = false; // �񱳼��� ������ ���� üũ
	bool inventoryCheck = false; //�÷��̾� �κ��丮 üũ
	bool Maircheck = false; // �� �κ��丮 ���� ���콺 �������� ��ȭ�鿡 �������� üũ
	bool Kaircheck = false; // �� �κ��丮 ���� ���콺 �������� ��ȭ�鿡 �������� üũ
	bool MM = false; // �� �κ��丮 -> �� �κ��丮 ���콺 üũ
	bool EE = false; // �� �κ��丮 -> ���â ���콺 üũ
	bool KK = false; // �� �κ��丮 -> ��ųâ ���콺 üũ
	bool AttachCkeck = false; // ���� �ٲٱ� üũ

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

	Render2D* P_Inventory; // �÷��̾� �κ��丮
	Render2D* E_Inventory; // �� �κ��丮
	Render2D* S_Inventory; // ���� �κ��丮
	Render2D* Equipment; // ���â
	Render2D* render2D; // rendertargetview
	Render2D* Comparison2D; // ��� ���� ��â

	Texture* T_P_inventory; // �÷��̾� �κ��丮
	Texture* T_E_inventory;	// �� �κ��丮
	Texture* T_S_inventory;	// ���� �κ��丮
	Texture* T_Equipment;	// ���â
	Texture* T_Comparison2D; // ��� ��â

	ModelAnimator* kachujin = NULL;	
	ModelAnimator* shadowkachujin = NULL;	
	ModelAnimator* Shop = NULL;	

	ModelRender* weapon;
	ModelRender* weapon2;
	ModelRender* ShadowWeapon;
	ModelRender* ShadowWeapon2;

	MeshRender* sphere; // 3���� ��ü

	class Monster* monster;
	class Damage* damage;
	class Inventory* inventory;
	class Terrain* terrain;
	class ParticleEditor* particleEditor[4];
	class ParticleSystem* particle[7];
	class Player* player;

	Billboard* bb[5];
	Billboard* Bossbillboard; // ���� ü�� ������
	Billboard* tree; 
	Billboard* gress[20]; 

	int ma, mb; // �� �κ��丮 ���콺 ������ 2000,2000 ���� �����ֱ� ���� �ڷ���
	int k; // �� �κ��丮 ���콺 ������ 2000,2000 ���� �����ֱ� ���� �ڷ���
	float beAttackedTime = 0;
	int money = 900 ; // ��
	char moneys[20]; // int -> string 
	
	int attack = 0;
	char C_attack[20];

	int defense = 0;
	char C_defense[20];
	

	Collider* Kcollider; // �÷��̾� ��Ʈ �ڽ�
	Transform* Init; // �÷��̾� ����
	Transform* Ktransform; // �÷��̾� ��Ʈ �ڽ�

	Collider* Scollider; // ���� ��Ʈ �ڽ�
	Transform* Stransform; // ���� ��Ʈ �ڽ�

	Collider* Mcollider[5]; // ���� ��Ʈ �ڽ�
	Transform* Mtransform[5]; // ���� ��Ʈ �ڽ�

	Collider* BossCollider; // ���� ��Ʈ �ڽ�
	Transform* BossTransform; // ���� ��Ʈ �ڽ�

	Collider* PlayerSkill; // ��ų �ݶ��̴�
	Collider* PlayerThrow[3]; // ����ü �ݶ��̴�

	int throwcount = 2;
	bool throwcheck[3];

	Sky* sky;
	Vector3 Direction = {0,0,0}; //���� ���ϱ� ��


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