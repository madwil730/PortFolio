#pragma once
#include "Scene.h"

class Stage1 : public Scene
{
private:
	Sprite * background;
	class Player* player;

public:
	Stage1(SceneValues* values);
	~Stage1();

	void Update() override;
	void Render() override;

};
