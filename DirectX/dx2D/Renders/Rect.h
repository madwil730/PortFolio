#pragma once

class Rect
{
private:
	Shader * shader;
	ID3D11Buffer* vertexBuffer;

	D3DXVECTOR2 position; //Translation;
	D3DXVECTOR2 scale;	//Scaling;
	D3DXCOLOR color;

private:
	struct Vertex
	{
		D3DXVECTOR3 Position;
		D3DXCOLOR Color;
	};

private:
	void CreateBuffer(wstring shaderFile);

public:
	Rect(wstring shaderFile);
	Rect(wstring shaderFile, D3DXVECTOR2 position, D3DXVECTOR2 scale, D3DXCOLOR color = D3DXCOLOR(0xaa000000));
	virtual ~Rect();

	virtual void Update(D3DXMATRIX& V, D3DXMATRIX& P);
	void Render();

public:
	void Position(float x, float y);
	void Position(D3DXVECTOR2& vec);
	D3DXVECTOR2 Position() { return position; }

	void Scale(float x, float y);
	void Scale(D3DXVECTOR2& vec);
	D3DXVECTOR2 Scale() { return scale; }

	void Color(float r, float g, float b);
	void Color(D3DXCOLOR& vec);
	D3DXCOLOR Color() { return color; }

	static bool PtInRect(Rect* rect, D3DXVECTOR2 ptMouse);

};