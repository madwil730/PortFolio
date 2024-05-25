#pragma once

#include "Systems/IExecute.h"

class Sight : public IExecute
{

public:
	virtual void Initialize() override;
	virtual void Ready() override {};
	virtual void Destroy() override;
	virtual void Update() override;
	virtual void PreRender() override;
	virtual void Render() override;
	virtual void PostRender() override {};
	virtual void ResizeScreen() override {};

	void CreateViewer();

private:
	Shader * shader;
	class Terrain* terrain;

	Material * red;

	Texture* heightMap;

	vector<Transform*> transforms;

	float fov = 0.25f;
	float zFar = 100.0f;

	RenderTarget * renderTarget;
	DepthStencil* depthStencil;
	Viewport* viewport;
	Render2D* render2D;
	PerFrame* perFrame;

	Fixity* camera;
	Perspective* perspective;
	Frustum* frustum;

	Shader* CpuShader;
};