#pragma once

class Background
{
private:
	Sprite * background;
	D3DXVECTOR2 position;
	D3DXVECTOR2 scale;
	D3DXVECTOR2 textureSize;

public:
	Background(wstring shaderFile);
	Background(wstring shaderFile, D3DXVECTOR2 position, D3DXVECTOR2 scale);
	~Background();

	void Update(D3DXMATRIX& V, D3DXMATRIX& P);
	void Render();

	void Position(float x, float y);
	void Position(D3DXVECTOR2& vec);
	D3DXVECTOR2 Position();

	void Scale(float x, float y);
	void Scale(D3DXVECTOR2& vec);
	D3DXVECTOR2 Scale();

	Sprite* GetSprite();

	D3DXVECTOR2 TextureSize() { return textureSize; }

private:
	void Initialize();
};