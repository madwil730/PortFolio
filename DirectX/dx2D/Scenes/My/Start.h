#pragma once

class Start : public Scene
{

private:


	class Ship* ship;
	class StartEarth* startearth;


public:

	Start(SceneValues* scene);
	~Start();


	int time = 0;

	void Update();
	void Render();

};