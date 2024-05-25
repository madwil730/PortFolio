#pragma once

enum class State2
{
	Idle = 0, Attacked
};

class Inveder2
{

private:

	State2 state2;
	Animation* animation;
	D3DXVECTOR2 position = D3DXVECTOR2(1500, 1500);
	bool Hit[10] = { false };

	float movespeed = 50.0f;


public:

	Inveder2(D3DXVECTOR2 position, D3DXVECTOR2 scale);
	~Inveder2();

	Sprite* GetSprite();

	State2 GetState() { return state2; }

	void SetHit(bool val, int i) { Hit[i] = val; }
	bool GetHit(int i) { return Hit[i]; }

	void Moveleft();

	void Position(D3DXVECTOR2 vec) { position = vec; } // set
	D3DXVECTOR2 Position() { return position; } // get

	void SetMoveSpeed(float i) { movespeed = i; }
	float GetMoveSpeed() { return movespeed; }

	void Update(D3DXMATRIX&V, D3DXMATRIX&P);
	void Render();

	float Itime = 0.0f;

	int HitCount = 0;
	bool timecheck = false;
};