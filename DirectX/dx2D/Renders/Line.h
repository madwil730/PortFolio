#pragma once

class Line
{
private:
	struct Vertex
	{
		D3DXVECTOR3 Position;
	};
	Vertex vertices[2];

private:
	Shader* shader;
	ID3D11Buffer* vertexBuffer;	

	D3DXVECTOR2 position;
	D3DXVECTOR2 scale;

	D3DXVECTOR2 left;
	D3DXVECTOR2 right;

	float slope;

	bool bDrawCollision;

private:
	void CreateBuffer(wstring shaderFile, D3DXVECTOR2 position, D3DXVECTOR2 position2);


public:
	Line(D3DXVECTOR2 position, D3DXVECTOR2 position2);
	~Line();

	void Update(D3DXMATRIX& V, D3DXMATRIX& P);
	void Render();

	void Left(D3DXVECTOR2& val) { left = val; }
	void Right(D3DXVECTOR2& val) { right = val; }
	D3DXVECTOR2 Left() { return left; }
	D3DXVECTOR2 Right() { return right; }

	float Slope() { return slope; }

	void DrawCollision(bool val) { bDrawCollision = val; }
};