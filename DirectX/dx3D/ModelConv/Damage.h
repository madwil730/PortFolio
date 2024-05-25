#pragma once


class Damage
{
public:

	Damage();
	void BeAttacked(ModelAnimator* i, float flow, float endflow , float scale = 5, float position = 2.5);
	//void MonsterBeAttacked(ModelAnimator* i, float flow, float endflow);
	
	Render2D* Health;
	Render2D* Hit;
	Render2D* Skill;

	Texture* T_Health;
	Texture* T_Hit;
	Texture* T_Skill;

	float time; 
};