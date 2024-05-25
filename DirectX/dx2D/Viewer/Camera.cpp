#include "stdafx.h"
#include "Camera.h"

Camera::Camera()
	:position(0, 0)
{
	D3DXMatrixIdentity(&view);
}

Camera::~Camera()
{
}

void Camera::Position(float x, float y)
{
	Position(D3DXVECTOR2(x, y));
}

void Camera::Position(D3DXVECTOR2 & vec)
{
	position = vec;
}

void Camera::Update()
{
	D3DXMatrixTranslation(&view, -position.x, -position.y, 0.0f);
}
