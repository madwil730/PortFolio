#pragma once


class StartEarth
{

private:


	Animation * animation;
	D3DXVECTOR2 position;


public:

	StartEarth(D3DXVECTOR2 position, D3DXVECTOR2 scale);
	~StartEarth();

	void Update(D3DXMATRIX&V, D3DXMATRIX&P);
	void Render();

};