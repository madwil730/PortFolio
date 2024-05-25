#pragma once

class Beam {

private:
	
	Animation* animation;
	D3DXVECTOR2 position = D3DXVECTOR2(500, 500);
	D3DXVECTOR2 scale = D3DXVECTOR2(1.0f, 1.0f);
	bool Hit[10] = { false };
	bool Hit2[10] = { false };
	bool Hit3[10] = { false };

public:

	Beam(D3DXVECTOR2 position, D3DXVECTOR2 scale);
	~Beam();

	Sprite* GetSprite();

	void MoveRight();

	void Position(D3DXVECTOR2 vec) { position = vec; } // set
	D3DXVECTOR2 Position() { return position; } // get

	void SetHit(bool val, int i) { Hit[i] = val; }
	bool GetHit(int i) { return Hit[i]; }

	void SetHit2(bool val, int i) { Hit2[i] = val; }
	bool GetHit2(int i) { return Hit2[i]; }

	void SetHit3(bool val, int i) { Hit3[i] = val; }
	bool GetHit3(int i) { return Hit3[i]; }

	void Update(D3DXMATRIX&V, D3DXMATRIX&P);
	void Render();

};