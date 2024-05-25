#pragma once

class Gameover
{

private:


	Animation* animation;


public:

	Gameover(D3DXVECTOR2 position, D3DXVECTOR2 scale);
	~Gameover();

	

	void Update(D3DXMATRIX&V, D3DXMATRIX&P);
	void Render();
};