#pragma once


class rock
{

private:

	
	Animation* animation;
	D3DXVECTOR2 position = D3DXVECTOR2(1000, 1000);
	float movespeed = 50.0f;


public:

	rock(D3DXVECTOR2 position, D3DXVECTOR2 scale);
	~rock();

	Sprite* GetSprite();

	void SetMoveSpeed(float i) { movespeed = i; }
	float GetMoveSpeed() { return movespeed; }

	void Moveleft();

	void Position(D3DXVECTOR2 vec) { position = vec; } // set
	D3DXVECTOR2 Position() { return position; } // get

	void Update(D3DXMATRIX&V, D3DXMATRIX&P);
	void Render();

};