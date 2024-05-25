#pragma once


class Space : public Scene
{

private:

	class Background* background1;
	class Background* background2;

	class Ship* ship;

	class Beam* beam[6];

	class Inveder* inveder[10];
	class Inveder2* inveder2[10];
	class Inveder3* inveder3[10];

	class Gameover* gameover;

	class rock* rocks[2];

	int tempy[10] = { 0 };
	int tempx[10] = { 0 };

	D3DXVECTOR2 playerPos = D3DXVECTOR2(-300, 0);
	D3DXVECTOR2 playerSize = D3DXVECTOR2(1.0f, 1.0f);

	D3DXVECTOR2 beamPos = D3DXVECTOR2(1003, 1003);
	D3DXVECTOR2 beamSize = D3DXVECTOR2(1.0f, 1.0f);

	D3DXVECTOR2 invederPos = D3DXVECTOR2(1000, 1000);
	D3DXVECTOR2 invederSize = D3DXVECTOR2(2.0f, 2.0f);

	bool beamshot[6] = { false };
	bool bAabb = false;

	

public:

	static int score;

	static float shipcheck ; // 우주선 파괴 체크

	Space(SceneValues* scene);
	~Space();

	void Update() override;
	void Render() override;

	bool check = false;
	bool aabbcheck = false; // 테스트용 
	bool touch = false;
};