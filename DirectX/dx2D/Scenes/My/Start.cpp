#include "stdafx.h"
#include "Start.h"
#include "./Object/My/Ship.h"
#include "./Object/My/StartEarth.h"

Start::Start(SceneValues * values) : Scene(values)
{
	SafeDelete(values->MainCamera);
	values->MainCamera = new Camera;

	ship = new Ship(D3DXVECTOR2(-100, 20), D3DXVECTOR2(1, 1));
	startearth = new StartEarth(D3DXVECTOR2(0, 0), D3DXVECTOR2(4.5, 4.8));

	ship->SetState(4);

}

Start::~Start()
{
	SafeDelete(ship);
	SafeDelete(startearth);
}

void Start::Update()
{
	D3DXMATRIX V = values->MainCamera->View();
	D3DXMATRIX P = values->Projection;

	values->MainCamera->Update();

	startearth->Update(V, P);
	ship->Update(V, P);
}

void Start::Render()
{
	
	startearth->Render();
	ship->Render();

	//mGui::LabelText("time", "%f", time);
	//ImGui::LabelText("time%2", "%d", time );
}
