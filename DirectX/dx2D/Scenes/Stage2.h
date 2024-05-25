#pragma once
#include "Scene.h"

class Stage2 : public Scene
{
private:
	Sprite * background;

	class Player* player;
	class Bullet* bullet;
	class Fire* fire;

public:
	Stage2(SceneValues* values);
	~Stage2();

	void Update() override;
	void Render() override;

};
