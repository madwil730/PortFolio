
#include "stdafx.h"
#include "Player.h"


Player::Player(Shader* shader)
{
	
	renderTarget = new RenderTarget();
	depthStencil = new DepthStencil();
	viewport = new Viewport((float)width, (float)height);

	buffer = new ConstantBuffer(&desc, sizeof(Desc)); // 카메라 설정
	sBuffer = shader->AsConstantBuffer("CB_PlayerCam");
}

void Player::SetRenderTarget(ModelAnimator* player)
{
	renderTarget->Set(depthStencil);

	static float x = 0, y = 0, width = D3D::Width(), height = D3D::Height();
	viewport->Set(width, height, x, y);
	viewport->RSSetViewport();

	player->Pass(6); 
	player->Render();
	
	RenderCamera();

	buffer->Apply();
	sBuffer->SetConstantBuffer(buffer->Buffer());
}

void Player::RenderCamera()
{
	Vector3 up = Vector3(0, 1, 0);
	Vector3 direction = Context::Get()->Direction();
	Vector3 position = direction * radius * -2.0f;
	Vector3 position2 = {41.29,1.45,41.40};
	Vector3 position3 = { 1,0,0 };

	D3DXMatrixLookAtLH(&desc.View, &position2, &position3, &up);// 이거 짤리는 이유 설명좀
	desc.Projection = Context::Get()->Projection();


	Vector3 cube;
	D3DXVec3TransformCoord(&cube, &this->position, &desc.View);

	//ImGui::LabelText("rotation", "%f,%f,%f", Context::Get()->GetCamera()->position.x, Context::Get()->GetCamera()->position.y, Context::Get()->GetCamera()->position.z);
}

void Player::AttackUp(float up)
{
	AttackPower = AttackPower + up;
}

void Player::DefenseUp(float up)
{
	DefensePower = DefensePower + up;
}

void Player::Attack(ModelAnimator* model, float time)
{

	attackTime += Time::Delta();
	if (attackTime > time) // 끝나는 시간
	{
		attack = 100;
		model->PlayClip(0, 0, 1.0f, 0.7f);
		attackTime = 0;
	}
	
}
