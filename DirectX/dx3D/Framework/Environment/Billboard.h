#pragma once

class Billboard : public Renderer
{
public:
	Billboard(Shader* shader);
	~Billboard();

	void Add(Vector3& position, Vector2& scale, UINT mapIndex);
	void AddTexture(wstring file);

	void CreateBuffer();
	void Update();
	void Render();
	void Apply();

	struct  VertexScale
	{
		Vector3 Position;
		Vector2 Scale;
		UINT MapIndex;
	};

	vector<VertexScale> vertices;
	vector<wstring> textureFiles;
	TextureArray* textures = NULL;
	ID3DX11EffectShaderResourceVariable* sMaps;

	VertexScale* v;
};