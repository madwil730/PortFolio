#pragma once

class SkyRect
{
private:
	D3DXVECTOR2 position;
	D3DXVECTOR2 scale;

private:
	struct Vertex
	{
		D3DXVECTOR3 Position;
	};

	//C++ 11
	struct LinearColorVertex
	{
		D3DXCOLOR Center = D3DXCOLOR(0xFF0080FF);
		D3DXCOLOR Apex = D3DXCOLOR(0xFF9BCDFF);
		float Height = 4.5f;
	} sky;

private:
	Shader * shader;
	ID3D11Buffer* vertexBuffer;

private:
	void CreateBuffer(wstring shaderFile);

public:
	SkyRect(wstring shaderFile);
	~SkyRect();

	void Udpate(D3DXMATRIX& V, D3DXMATRIX &P);
	void Render();

	void Position(float x, float y);
	void Position(D3DXVECTOR2& vec);
	D3DXVECTOR2 Position() { return position; }


	void Scale(float x, float y);
	void Scale(D3DXVECTOR2& vec);
	D3DXVECTOR2 Scale() { return scale; }


};