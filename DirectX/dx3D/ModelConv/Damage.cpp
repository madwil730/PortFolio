#include "stdafx.h"
#include "Damage.h"


Damage::Damage()
{

	Health = new Render2D();
	Health->GetTransform()->Position(660, 50, 0);
	Health->GetTransform()->Scale(D3D::Width() * 0.8f, 20, 0);
	T_Health = new Texture(L"Green.png");

	Hit = new Render2D();
	Hit->GetTransform()->Position(1175, 50, 0);
	Hit->GetTransform()->Scale(0, 20, 1); // ü�¹� �����Ϸ� ǥ���ϴϱ� ������ Ŀ�� ���ʸ� Ŀ���� ����� �� ��ĳ��?
	T_Hit = new Texture(L"Red.png");

	Skill = new Render2D();
	Skill->GetTransform()->Position(340, 80, 0);
	Skill->GetTransform()->Scale(D3D::Width() * 0.3f, 30, 1); // ü�¹� �����Ϸ� ǥ���ϴϱ� ������ Ŀ�� ���ʸ� Ŀ���� ����� �� ��ĳ��?
	T_Skill = new Texture(L"SkillUI.png");
}


void Damage::BeAttacked(ModelAnimator* i, float flow, float endflow, float scale, float position)
{
	i->AttackTime += Time::Delta();
	if (i->AttackTime >= flow && i->AttackTime < flow+0.1 ) // �´� �ð���
	{
		Hit->GetTransform()->scale.x += scale; // 5
		Hit->GetTransform()->position.x -= position; // 2.5
		Hit->GetTransform()->UpdateWorld();
	}

	if (i->AttackTime >= endflow) i->AttackTime = 0;
}

	

