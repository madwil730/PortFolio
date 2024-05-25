#pragma once

class Transform
{
public:
	Transform();
	Transform(Shader* shader);
	~Transform();

	void Set(Transform* transform);

	void SetShader(Shader* shader);

	void Position(float x, float y, float z);
	void Position(Vector3& vec);
	void Position(Vector3* vec);

	void Scale(float x, float y, float z);
	void Scale(Vector3& vec);
	void Scale(Vector3* vec);

	void Rotation(float x, float y, float z);
	void Rotation(Vector3& vec);
	void Rotation(Vector3* vec);

	void RotationDegree(float x, float y, float z);
	void RotationDegree(Vector3& vec);
	void RotationDegree(Vector3* vec);

	Vector3 GetPosition() { return position; }
	Vector3 GetScale() { return scale; }

	Vector3 Foward();
	Vector3 Up();
	Vector3 Right();
	Vector3 position;
	Vector3 scale;


	void World(Matrix& matrix);
	Matrix& World() { return bufferDesc.World; }

	Vector3 rotation;
	void UpdateWorld();

private:
	

public:
	void Update();
	void Render();

private:
	struct BufferDesc
	{
		Matrix World;
	} bufferDesc;

private:
	Shader * shader;

	ConstantBuffer* buffer;
	ID3DX11EffectConstantBuffer* sBuffer;
	

	
};