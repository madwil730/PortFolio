#pragma once

class Sprite
{
private:
	Shader* shader;
	Shader* boundShader;

	ID3D11Buffer* vertexBuffer;
	ID3D11Buffer* boundVetexBuffer;

	D3DXVECTOR2 position;
	D3DXVECTOR2 scale;
	D3DXVECTOR3 rotation;
	D3DXMATRIX world;

	D3DXVECTOR2 textureSize;

	wstring textureFile;
	ID3D11ShaderResourceView* srv;

	bool bDrawBound;
	bool bDrawCollision;
	bool bBloom;

private:
	struct Vertex
	{
		D3DXVECTOR3 Position;
		D3DXVECTOR2 Uv;
	};

	struct BoundVertex
	{
		D3DXVECTOR3 Position;
	};

	///디버그용
private:
	static float lA[4];
	static float lB[4];
	static float l[4];
	static bool lCheck[4];

private:
	void Initialize(wstring spriteFile, wstring shaderFile, float startX, float startY, float endX, float endY);
	void CreateBound();

public:
	Sprite(wstring textureFile, wstring shaderFile);
	Sprite(wstring textureFile, wstring shaderFile, float endX, float endY);
	Sprite(wstring textureFile, wstring shaderFile, float startX, float startY, float endX, float endY);
	virtual ~Sprite();

	void Update(D3DXMATRIX& V, D3DXMATRIX& P);
	void Render();

	void DrawBound(bool val) { bDrawBound = val; }
	void DrawCollision(bool val) { bDrawCollision = val; }
	void DrawBloom(bool val) { bBloom = val; }

	bool Aabb(D3DXVECTOR2 position);
	bool Aabb(Sprite* b);
	bool Obb(Sprite* b);

	static bool Aabb(Sprite* sprite, D3DXVECTOR2 position);
	static bool Aabb(Sprite* a, Sprite* b);
	static bool Obb(Sprite* a, Sprite* b);

private:
	struct ObbDesc
	{
		D3DXVECTOR2 Position; //위치
		D3DXVECTOR2 Direction[2]; //[0]회전된 X 방향, [1]회전된 Y 방향
		float Length[2]; //회전된 이후의 크기 
	};

											//충돌 체크를 시작할 위치, 회전값 획득, 체크할 길이
	static void CreateObb(OUT ObbDesc* out, D3DXVECTOR2& position, D3DXMATRIX& world, D3DXVECTOR2 lenght);
	static float SeparateAxis(D3DXVECTOR2& separate, D3DXVECTOR2& e1, D3DXVECTOR2& e2);
	static bool CheckObb(ObbDesc& obbA, ObbDesc& obbB);


public:
	void Position(float x, float y);
	void Position(D3DXVECTOR2& vec);
	D3DXVECTOR2 Position() { return position; }

	void Scale(float x, float y);
	void Scale(D3DXVECTOR2& vec);
	D3DXVECTOR2 Scale() { return scale; }

	void Rotation(float x, float y, float z);
	void Rotation(D3DXVECTOR3& vec);
	D3DXVECTOR3 Rotation() { return rotation; }

	void RotationDegree(float x, float y, float z);
	void RotationDegree(D3DXVECTOR3& vec);
	D3DXVECTOR3 RotationDegree();


	D3DXVECTOR2 TextureSize() { return textureSize; }
};

///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

class Sprites // <- SRV 관리 클래스
{
private:
	friend class Sprite;

private:
	static ID3D11ShaderResourceView* Load(wstring file);
	static void Remove(wstring file);

private:
	struct SpriteDesc
	{
		UINT RefCount = 0;
		ID3D11ShaderResourceView* SRV = NULL;
	};

	static map<wstring, SpriteDesc> spriteMap;

};
