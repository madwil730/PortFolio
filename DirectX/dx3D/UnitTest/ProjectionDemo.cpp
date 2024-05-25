#include "stdafx.h"
#include "ProjectionDemo.h"
#include "Environment/Terrain.h"
#include "Viewer/Freedom.h"

void ProjectionDemo::Initialize()
{
	Context::Get()->GetCamera()->RotationDegree(0, 0, 0);
	Context::Get()->GetCamera()->Position(0, 0, -10);
	((Freedom*)Context::Get()->GetCamera())->Speed(100, 2);

	shader = new Shader(L"12_Terrain.fx");
	
	terrain = new Terrain(shader, L"Terrain/Gray256.png");
	terrain->BaseMap(L"Terrain/Dirt3.png");

	meshShader = new Shader(L"10_Mesh.fx");
	sphere = new MeshRender(meshShader, new MeshSphere(0.5f));
	sphere->AddTransform();
	sphere->UpdateTransforms();
}

void ProjectionDemo::Destroy()
{
	SafeDelete(shader);
	SafeDelete(terrain);

	SafeDelete(meshShader);
	SafeDelete(sphere);
}

void ProjectionDemo::Update()
{
	Vector3 position;
	sphere->GetTransform(0)->Position(&position);

	if (Keyboard::Get()->Press(VK_UP))
		position.z += 20.0f * Time::Delta();
	else if (Keyboard::Get()->Press(VK_DOWN))
		position.z -= 20.0f * Time::Delta();

	if (Keyboard::Get()->Press(VK_RIGHT))
		position.x += 20.0f * Time::Delta();
	else if (Keyboard::Get()->Press(VK_LEFT))
		position.x -= 20.0f * Time::Delta();

	Vector3 scale;
	sphere->GetTransform(0)->Scale(&scale);
	//position.y = terrain->GetHeight(position);
	position.y = terrain->GetHeightPick(position) + scale.y * 0.5f;

	sphere->GetTransform(0)->Position(position);

	static Vector3 light = Vector3(-1, -1, 1);
	ImGui::SliderFloat3("Light", light, -1, 1);
	shader->AsVector("LightDirection")->SetFloatVector(light);
	meshShader->AsVector("LightDirection")->SetFloatVector(light);

	sphere->Update();
	terrain->Update();
}

void ProjectionDemo::Render()
{
	
	Viewport* vp = Context::Get()->GetViewport();

	Matrix w, v, p;
	D3DXMatrixIdentity(&w);
	v = Context::Get()->View();
	p = Context::Get()->Projection();

	Vector3 result, position;
	sphere->GetTransform(0)->Position(&position);
	//position.y = terrain->GetHeightPick(position) + 0.5f;

	vp->Project(&result, position, w, v, p);
	
	

	i = i + 1;
	char a[10];

	sprintf(a, "%d", i);

	Gui::Get()->RenderText(result.x, result.y, 1, 1, 1,a );	

	terrain->Render();
	sphere->Render();

	ImGui::LabelText("Vp", "%0.2f, %0.2f, %0.2f", result.x, result.y, result.z);
	ImGui::LabelText("i", "%d", i);

}

