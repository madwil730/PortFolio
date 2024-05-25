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

	//--- 뮤턴트 조건--------
	float x[5];
	float z[5];
	bool playclip[5];
	bool AttackClip[5];
	bool onAttack[5] = {false}; // 공격 체크 하는 시간
	bool LongCheck = false; // 원거리 공격 체크

	bool MoveDistance = true;
	bool LongDistance = true;

	bool test = false;

	float AttackTime[5]; // 공격시 걸리는 시간

	int Mchoice[5] = { 0 }; // 몬스터 행동 알고리즘 제어
	int Bosschoice = 0;
	int EnemyNumber = 5; // 몬스터 객체 수 

	//-----------------------
	float bossSkillTime = 0;
	Vector3 Long; // 원거리 정규화 백터

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

	struct EmyInventory  // 적 인벤토리
	{
		float x, y;
		int price;
		wstring str;
		Render2D* image;
		Texture* T_image;
	} EnemyInventory[5][16];

	bool Inventorycheck[5] = { false }; // 적 인벤토리 활성키 여부 체크
};



