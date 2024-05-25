#include "stdafx.h"
#include "sight.h"
#include "Environment/Terrain.h"
#include "Viewer/Freedom.h"

void Sight::Initialize()
{
	Context::Get()->GetCamera()->RotationDegree(11, 0, 0);
	Context::Get()->GetCamera()->Position(132, 42, -17);
	((Freedom*)Context::Get()->GetCamera())->Speed(100, 2);

	shader = new Shader(L"23_TerrainSpatting.fx");
	CpuShader = new Shader(L"47_CpuFrustum.fxo");

	terrain = new Terrain(shader, L"Terrain/Gray256.png");
	terrain->BaseMap(L"Terrain/Rock (Basic).jpg");
	terrain->LayerMap(L"Terrain/Path (Rocky).jpg", L"Terrain/Splatting.png");

	perFrame = new PerFrame(CpuShader);

	camera = new Fixity();
	camera->Position(0, 0, -50);
	perspective = new Perspective(1024, 768, 1, zFar, Math::PI * fov);

	frustum = new Frustum(camera, perspective);

	renderTarget = new RenderTarget(); // , 백버퍼 즉 2d 그림임
	depthStencil = new DepthStencil(200, 200); // 깊이 조절 , 보내줄 픽셀 정보를 토대로 그림
	viewport = new Viewport(D3D::Width(), D3D::Height()); // rtv 와 dsv를 뛰우기 위한 창

	render2D = new Render2D(L"40_PostEffect.fxo");

	render2D->GetTransform()->Position(100, 100, 0);
	render2D->GetTransform()->Scale(100, 100, 1);

	// 백버퍼 그리기 위해 rtv,dsv 뷰포트 필요
	// 화면에 출력되기 전에 그리기는 게 백 버퍼
}

void Sight::Destroy()
{
	SafeDelete(shader);
	SafeDelete(terrain);
}

void Sight::Update()
{
	ImGui::InputFloat("zFar", &zFar, 1.0f);
	ImGui::InputFloat("FOV", &fov, 1e-3f);
	perspective->Set(1024, 768, 1, zFar, Math::PI * fov);

	frustum->Update();
	perFrame->Update();
	terrain->Update();
	render2D->Update();
}

void Sight::PreRender() // 검은색 작은 화면
{


	renderTarget->Set(depthStencil);
	viewport->RSSetViewport();

	terrain->Render();
}

void Sight::Render() // 본 화면
{
	perFrame->Render(); // 이거 하는 이유 설명
	terrain->Render();
	render2D->SRV(renderTarget->SRV());
	render2D->Render();

	/*UINT drawCount = 0;
	Vector3 position;
	for (Transform* transform : transforms)
	{
		transform->Position(&position);

		if (frustum->CheckPoint(position) == true)
		{
			transform->Update();
			transform->Render();

			CpuShader->DrawIndexed(0, 0, 36 );

			drawCount++;
		}
	}

	string str = "Draw : " + to_string(drawCount) + ", Toal : " + to_string(transforms.size());
	Gui::Get()->RenderText(10, 60, 0, 1, 1, str);

	ImGui::LabelText("x", " %f", terrain->GetVertex()[3].Position.x);
	ImGui::LabelText("y", " %f", terrain->GetVertex()[3].Position.y);
	ImGui::LabelText("z", " %f", terrain->GetVertex()[3].Position.z);
	ImGui::LabelText("size", " %d", sizeof(terrain->GetVertex())/sizeof(terrain->GetVertex()[0]));*/
}

void Sight::CreateViewer()
{
	
}
