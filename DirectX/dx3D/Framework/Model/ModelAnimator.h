#pragma once

class ModelAnimator
{
public:
	ModelAnimator(Shader* shader);
	~ModelAnimator();

	float deathTime = 0;

	void Update();
	void Render();

	float time = 0;
	float death[3];

	bool playclip = false;
	bool AttackClip = false;
	bool goclip = false;
	bool onAttack = false; // 공격 체크 하는 시간

	float AttackTime = 0; // 공격시 걸리는 시간


	void ReadMaterial(wstring file);
	void ReadMesh(wstring file);
	void ReadClip(wstring file);
	void PlayClip(UINT instance, UINT clip, float speed = 1.0f, float takeTime = 1.0f , bool check = true);

	
	Collider* DistanceCollider;
	Collider* HitDistanceCollider;
	Collider* LongCollider;

	Model* GetModel() { return model; }
	Vector3 Direction;
	void Pass(UINT pass);

	Transform* AddTransform();
	Transform* GetTransform(UINT index) { return transforms[index]; }
	void UpdateTransforms();

	Matrix GetAttachTransform(UINT index);

	void CreateTexture();
	void CreateClipTransform(UINT index);
	void CreateComputeDesc();
	void OneFrame(int i, bool check);

private:
	struct ClipTransform
	{
		Matrix** Transform;

		ClipTransform()
		{			
			Transform = new Matrix*[MAX_MODEL_KEYFRAMES];

			for (UINT i = 0; i < MAX_MODEL_KEYFRAMES; i++)
				Transform[i] = new Matrix[MAX_MODEL_TRANSFORMS];
		}

		~ClipTransform()
		{
			for (UINT i = 0; i < MAX_MODEL_KEYFRAMES; i++)
				SafeDeleteArray(Transform[i]);

			SafeDeleteArray(Transform);
		}
	};
	ClipTransform* clipTransforms = NULL;

	ID3D11Texture2D* texture = NULL;
	ID3D11ShaderResourceView* srv = NULL;

private:
	struct KeyframeDesc
	{
		int Clip = 0;

		UINT CurrFrame = 0;
		UINT NextFrame = 0;

		float Time = 0.0f;
		float RunningTime = 0.0f;

		float Speed = 1.0f;

		Vector2 Padding;
	}; /* keyframeDesc*/

	struct TweenDesc
	{
		float TakeTime = 1.0f;
		float TweenTime = 0.0f;
		float RunningTime = 0.0f;
		float Padding;
		
		KeyframeDesc Curr;
		KeyframeDesc Next;

		TweenDesc()
		{
			Curr.Clip = 0;
			Next.Clip = -1;
		}
	} tweenDesc[MAX_MODEL_INSTANCE];

	ConstantBuffer* frameBuffer;
	ID3DX11EffectConstantBuffer* sFrameBuffer;

private:
	struct CS_InputDesc
	{
		Matrix Bone;
	};

	struct CS_OutputDesc
	{
		Matrix Result;
	};

	struct AttachDesc
	{
		UINT AttachBoneIndex = 35;
		float Padding[3];
	} attachDesc;

private:	
	Shader * shader;
	Model* model;

	vector<Transform*> transforms;
	Matrix worlds[MAX_MODEL_INSTANCE];

	VertexBuffer* instanceBuffer;


private:
	Shader * computeShader;
	StructuredBuffer* computeBuffer = NULL;

	CS_InputDesc* csInput = NULL;
	CS_OutputDesc* csOutput = NULL;

	ConstantBuffer* computeAttachBuffer;
	ID3DX11EffectConstantBuffer* sComputeAttachBuffer;
	ID3DX11EffectConstantBuffer* sComputeFrameBuffer;

	ID3DX11EffectShaderResourceVariable* sSrv;
	ID3DX11EffectUnorderedAccessViewVariable* sUav;
	
};