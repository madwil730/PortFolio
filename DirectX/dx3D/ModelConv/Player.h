#pragma once


class Player
{
public:

	Player(Shader* shader) ;

	void Attack(ModelAnimator* model, float time);
	int attack = 100; // 숫자로 함수 체크

	float attackTime = 0;
	float AttackPower = 0;
	float DefensePower = 0;

	void SetRenderTarget(ModelAnimator* player);
	void RenderCamera();

	void AttackUp(float up);
	void DefenseUp(float up);

	struct Desc
	{
		Matrix View;
		Matrix Projection;
	} desc;

	//Shader * shader;
	UINT width = 500, height = 500;

	Vector3 position = {0,0,0};
	float radius = 65;

	RenderTarget* renderTarget;
	DepthStencil* depthStencil;
	Viewport* viewport;

	ConstantBuffer* buffer;
	ID3DX11EffectConstantBuffer* sBuffer;
	ID3DX11EffectShaderResourceVariable* sShadowMap;
};

