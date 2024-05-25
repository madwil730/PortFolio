#pragma once

#include "Systems/IExecute.h"

class CpuBillboardDemo : public IExecute
{
public:
	virtual void Initialize() override;
	virtual void Ready() override {};
	virtual void Destroy() override;
	virtual void Update() override;
	virtual void PreRender() override {};
	virtual void Render() override;
	virtual void PostRender() override {};
	virtual void ResizeScreen() override {};

private:
	Shader * shader;
	class Terrain* terrain;
	class SkyCube* sky;

	Shader* bbShader;
	class Billboard* bb[1000];
};