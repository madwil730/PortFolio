#pragma once
#include "Systems/IExecute.h"

class WaterDemo : public IExecute
{
public:
	virtual void Initialize() override;
	virtual void Ready() override {}
	virtual void Destroy() override;
	virtual void Update() override;
	virtual void PreRender() override;
	virtual void Render() override;
	virtual void PostRender() override;
	virtual void ResizeScreen() override {}

private:
	void Mesh();
	void Kachujin();

private:
	Shader * shader;

	Shadow* shadow;	


	Material* floor;
	MeshRender* grid;

	ModelAnimator* kachujin = NULL;




};
