#pragma once
#include "./Object/My/Beam.h"


enum class Direction { forward = 0, up, down, destroy, flash };

class Ship {

private:

	Animation* animation;
	Direction direction;

	D3DXVECTOR2 position = D3DXVECTOR2(0, 0);

	bool Hit[10] = {false};
	bool Hit2[10] = { false };
	bool Hit3[10] = { false };

	bool Hit4[2] = { false };

public:

	static bool death;

	Ship(D3DXVECTOR2 position, D3DXVECTOR2 Scale);
	~Ship();

	Sprite* GetSprite();

	void Play();

	void SetState(int i) { direction = (Direction)i; }
	Direction GetState() { return direction; }

	//position get set
	void Position(D3DXVECTOR2 vec) { position = vec; }
	D3DXVECTOR2 Position() { return position; }

	void SetHit(bool val, int i) { Hit[i] = val; }
	bool GetHit(int i) { return Hit[i]; }

	void SetHit2(bool val, int i) { Hit2[i] = val; }
	bool GetHit2(int i) { return Hit2[i]; }

	void SetHit3(bool val, int i) { Hit3[i] = val; }
	bool GetHit3(int i) { return Hit3[i]; }

	void SetHit4(bool val, int i) { Hit3[i] = val; }
	bool GetHit4(int i) { return Hit3[i]; }

	void Update(D3DXMATRIX&V, D3DXMATRIX&P);
	void Render();

	bool hitcheck = false;
};