#pragma once
#include "IFollow.h"
#include "Camera.h"

class Following : public Camera
{
	IFollow* focus;

public:
	Following(IFollow* focus = NULL);
	~Following();

	void Change(IFollow* focus);

	void Update();
};