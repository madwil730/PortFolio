#pragma once

enum class State3
{
	Idle = 0, Attacked
};

class Inveder3
{

private:

	State3 state3;
	Animation* animation;
	D3DXVECTOR2 position = D3DXVECTOR2(1000, 1000);
	bool Hit[10] = { false };
	
	float movespeed = 50.0f;


public:

	Inveder3(D3DXVECTOR2 position, D3DXVECTOR2 scale);
	~Inveder3();

	Sprite* GetSprite();

	State3 GetState() { return state3; }

	void SetHit(bool val, int i) { Hit[i] = val; }
	bool GetHit(int i) { return Hit[i]; }

	void SetMoveSpeed(float i) { movespeed = i; }
	float GetMoveSpeed() { return movespeed; }

	void Moveleft();

	void Position(D3DXVECTOR2 vec) { position = vec; } // set
	D3DXVECTOR2 Position() { return position; } // get

	void Update(D3DXMATRIX&V, D3DXMATRIX&P);
	void Render();

	float Itime = 0.0f;

	int HitCount = 0;
	bool timecheck = false;
};