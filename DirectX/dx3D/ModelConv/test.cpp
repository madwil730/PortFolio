#include "stdafx.h"
#include "test.h"

void test::Initialize()
{
	Context::Get()->GetCamera()->RotationDegree(10, 0, 0);
	Context::Get()->GetCamera()->Position(0, 18, -78);
	((Freedom *)Context::Get()->GetCamera())->Speed(20, 2);

	shader = new Shader(L"54_Water.fxo");

	shadow = new Shadow(shader, Vector3(0, 0, 0), 65);


	Mesh();

	Kachujin();

}

void test::Destroy()
{
	SafeDelete(shader);
	SafeDelete(shadow);

}

void test::Update()
{
	UINT& type = Context::Get()->FogType();
	ImGui::InputInt("FogType", (int*)&type);
	type %= 3;

	ImGui::ColorEdit3("FogColor", Context::Get()->FogColor());
	ImGui::SliderFloat("FogMin", &Context::Get()->FogDistance().x, 0.0f, 10.0f);
	ImGui::SliderFloat("FogMax", &Context::Get()->FogDistance().y, 0.0f, 300.0f);
	ImGui::SliderFloat("FogDensity", &Context::Get()->FogDensity(), 0.0f, 300.0f);



	grid->Update();

	kachujin->Update();



}

void test::PreRender()
{

	//Depth
	{
		shadow->Set();

		floor->Render();
		grid->Pass(0);
		grid->Render();



		kachujin->Pass(2);
		kachujin->Render();
	}
}

void test::Render()
{


	floor->Render();
	grid->Pass(7);
	grid->Render();

	kachujin->Pass(9);
	kachujin->Render();

}

void test::PostRender()
{
	//gBuffer->DebugRender();
	//sky->PostRender();
}

void test::Mesh()
{
	//Create Material
	{
		//¹Ù´Ú
		floor = new Material(shader);
		floor->DiffuseMap("Floor.png");
		floor->SpecularMap("Floor_Specular.png");
		floor->NormalMap("Floor_Normal.png");
		floor->Specular(1, 1, 1, 15);
		floor->Emissive(0.2f, 0.2f, 0.2f, 0.3f);

	}

	//Create Mesh
	{
		Transform* transform = NULL;



		grid = new MeshRender(shader, new MeshGrid(15, 15));
		transform = grid->AddTransform();
		transform->Position(0, 0, 0);
		transform->Scale(20, 1, 20);




	}


	grid->UpdateTransforms();

}



void test::Kachujin()
{


	kachujin = new ModelAnimator(shader);
	kachujin->ReadMaterial(L"Kachujin/Mesh");
	kachujin->ReadMesh(L"Kachujin/Mesh");
	kachujin->ReadClip(L"Kachujin/Idle");
	kachujin->ReadClip(L"Kachujin/Running");
	kachujin->ReadClip(L"Kachujin/Jump");
	kachujin->ReadClip(L"Kachujin/Hip_Hop_Dancing");

	Transform attachTransform;

	attachTransform.Position(-10, 0, -10);
	attachTransform.Scale(1.0f, 1.0f, 1.0f);



	Transform* transform = NULL;

	transform = kachujin->AddTransform();
	transform->Position(-25, 0, -30);
	transform->Scale(0.075f, 0.075f, 0.075f);
	kachujin->PlayClip(0, 0, 0.25f);



	kachujin->UpdateTransforms();




}

