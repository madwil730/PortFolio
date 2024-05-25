#include "stdafx.h"
#include "Following.h"

Following::Following(IFollow * focus)
	:focus(focus)
{
}

Following::~Following()
{
}

void Following::Change(IFollow * focus)
{
	this->focus = focus;
}

void Following::Update()
{
	if (focus == NULL)
		return;

	D3DXVECTOR2 location, size;

	focus->Focus(&location, &size);

	//ȭ�� �߽����� ��Ŀ��
	D3DXVECTOR2 temp = location;

	temp.x -= (float)Width * 0.5f;
	temp.y -= (float)Height * 0.5f;

	temp.x += size.x * 0.5f;
	temp.y += size.y * 0.5f;

	position = temp;

	__super::Update();
}
