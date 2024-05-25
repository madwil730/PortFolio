#pragma once


class Monster 
{
public:

	Monster();
	void RandomMove(ModelAnimator* i, int j );
	void goPlayer(ModelAnimator* kachujin, ModelAnimator* i , int j);
	void Instruction(ModelAnimator* kachujin, ModelAnimator* monster, int i, bool Judgment= true);
	void BossInstruction(ModelAnimator* kachujin, ModelAnimator* monster, int i, Billboard* bill);

	float MutantWalkTime = 0;
	float CameraTime = 0;

	//--- ����Ʈ ����--------
	float x[5];
	float z[5];
	bool playclip[5];
	bool AttackClip[5];
	bool onAttack[5] = {false}; // ���� üũ �ϴ� �ð�
	bool LongCheck = false; // ���Ÿ� ���� üũ

	bool MoveDistance = true;
	bool LongDistance = true;

	bool test = false;

	float AttackTime[5]; // ���ݽ� �ɸ��� �ð�

	int Mchoice[5] = { 0 }; // ���� �ൿ �˰��� ����
	int Bosschoice = 0;
	int EnemyNumber = 5; // ���� ��ü �� 

	//-----------------------
	float bossSkillTime = 0;
	Vector3 Long; // ���Ÿ� ����ȭ ����

	Shader * shader;
	ModelAnimator* mutant;
	ModelAnimator* space = NULL;
	ModelAnimator* boss = NULL;

	ModelRender* bossWeapon;
	MeshRender* sphere;

	Transform* SphereTransform;
	Collider* SphereCollider;

	Transform* TransformBossSkill;
	Collider* ColliderBossSkill;


	void Mutant();
	void Space();
	void Boss();
	void SetInventory();
	void SetItem();
	void Sphere();

	void Update();
	void Render();

	struct EmyInventory  // �� �κ��丮
	{
		float x, y;
		int price;
		wstring str;
		Render2D* image;
		Texture* T_image;
	} EnemyInventory[5][16];

	bool Inventorycheck[5] = { false }; // �� �κ��丮 Ȱ��Ű ���� üũ
};



