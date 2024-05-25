#pragma once

class ParticleSystem : public Renderer
{
public:
	ParticleSystem(wstring file);
	~ParticleSystem();

	void Reset();

	void Add(Vector3& position);

public:
	void Update();

private:
	void MapVertices();
	void Activate();
	void Deactivate();

public:
	void Render();

public:
	ParticleData& GetData() { return data; }

	void SetTexture(wstring file)
	{
		SafeDelete(map);
		map = new Texture(file);
	}


private:
	void ReadFile(wstring file);

private:
	struct VertexParticle
	{
		Vector3 Position;
		Vector2 Corner;
		Vector3 Velocity;
		Vector4 Random; //x:�ֱ�, y:ũ��, z:ȸ��, w:����
		float Time;
	};

private:
	struct Desc
	{
		Color MinColor;
		Color MaxColor;

		Vector3 Gravity;
		float EndVelocity;

		Vector2 StartSize;
		Vector2 EndSize;

		Vector2 RotateSpeed;
		float ReadyTime;
		float ReadyRandomTime;

		float CurrentTime;
		float Padding[3];
	} desc;

private:
	ParticleData data;

	ConstantBuffer* buffer;
	ID3DX11EffectConstantBuffer* sBuffer;

	Texture* map = NULL;
	ID3DX11EffectShaderResourceVariable* sMap;

	VertexParticle* vertices = NULL;
	UINT* indices = NULL;

	float currentTime = 0.0f;
	float lastAddTime = 0.0f;

	UINT leadCount = 0;
	UINT gpuCount = 0;

	UINT activeCount = 0;
	UINT deactiveCount = 0;

};