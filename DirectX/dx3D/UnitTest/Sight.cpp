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

	renderTarget = new RenderTarget(); // , ����� �� 2d �׸���
	depthStencil = new DepthStencil(200, 200); // ���� ���� , ������ �ȼ� ������ ���� �׸�
	viewport = new Viewport(D3D::Width(), D3D::Height()); // rtv �� dsv�� �ٿ�� ���� â

	render2D = new Render2D(L"40_PostEffect.fxo");

	render2D->GetTransform()->Position(100, 100, 0);
	render2D->GetTransform()->Scale(100, 100, 1);

	// ����� �׸��� ���� rtv,dsv ����Ʈ �ʿ�
	// ȭ�鿡 ��µǱ� ���� �׸���� �� �� ����
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

void Sight::PreRender() // ������ ���� ȭ��
{


	renderTarget->Set(depthStencil);
	viewport->RSSetViewport();

	terrain->Render();
}

void Sight::Render() // �� ȭ��
{
	perFrame->Render(); // �̰� �ϴ� ���� ����
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
