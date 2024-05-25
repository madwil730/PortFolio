#pragma once
#include "stdafx.h"

class IFollow
{
public:
	virtual void Focus(D3DXVECTOR2* position, D3DXVECTOR2* size) = 0;
};