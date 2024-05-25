#include "stdafx.h"
#include "./Systems/Device.h"

#include "./Viewer/FreeCamera.h"

#include "./Scenes/Scene.h"
#include "./Scenes/Stage1.h"
#include "./Scenes/Stage2.h"
#include "./Scenes/Sonic/MarerTest.h"
#include "./Scenes/Sonic/MapEditor.h"
#include "./Scenes/Sonic/Game.h"
#include "./Scenes/Sonic/TitleScene.h"

#include "./Scenes/ShaderTest/Bloom.h"
#include "./Scenes/ShaderTest/SkyRectTest.h"
#include "./Scenes/ShaderTest/ColorOverlay.h"

#include "./Scenes/Astar/AstarTest.h"

#include "./Scenes/UnitTest/DashTest.h"
#include "./Scenes/UnitTest/ButtonDashTest.h"
#include "./Scenes/UnitTest/RapidDashTest.h"
#include "./Scenes/UnitTest/Move8WayTest.h"
#include "./Scenes/UnitTest/WarpTest.h"
#include "./Scenes//UnitTest/JumpStaticTest.h"
#include "./Scenes//UnitTest/JumpVariableTest.h"
#include "./Scenes/My/Space.h"
#include "./Object/My/Ship.h"
#include "./Scenes/My/Start.h"

SceneValues* values;
vector<Scene*> scenes;

bool aStart = false;
bool bStart = false;

void InitScene()
{
	values = new SceneValues();
	values->MainCamera = new FreeCamera();
	D3DXMatrixIdentity(&values->Projection);

	scenes.push_back(new Start(values));
	scenes.push_back(new Space(values));
	scenes.push_back(new Space(values));
	
}

void DestroyScene()
{
	for (Scene* scene : scenes)                                                                                                              
		SafeDelete(scene);
	
	SafeDelete(values);

}

void Update()
{	
	//View
	//values->MainCamera->Update();
	//Projection
	D3DXMatrixOrthoOffCenterLH
	(
		&values->Projection, 
		(float)Width * -0.5f,
		(float)Width * 0.5f,
		(float)Height * -0.5f,
		(float)Height * 0.5f,
		-1, 1
	);

	if (aStart == false) {
		scenes[0]->Update();

		if (Key->Down(VK_RETURN))
			aStart = true;
	}
	else if (aStart == true) {

		if (Ship::death) {

			if (Key->Down('R')) {
				bStart = !bStart;
				Space::score = 0;
				Space::shipcheck = 0;
				Ship::death = false;
			}
		}

		if (bStart == false)
			scenes[1]->Update();

		else if (bStart == true) {
			scenes[2]->Update();
		}
	}
		
}

void Render()
{
	//ImGui::LabelText("bstart", "%d", bStart);

	D3DXCOLOR bgColor = D3DXCOLOR(0xff555566); //D3DXCOLOR(R, G, B, 1)
	DeviceContext->ClearRenderTargetView(RTV, (float*)bgColor);
	{
		if (aStart == false) 
			scenes[0]->Render();

		else if (aStart == true) {

			if (bStart == false)
				scenes[1]->Render();

			else if (bStart == true) {
				//Space::score = 0;
				scenes[2]->Render();
			}
		}

	}
	ImGui::Render();


	

	/*DirectWrite::GetDC()->BeginDraw();
	{
		RECT rect = { 0, 0, 500, 200 };		

		rect.top += 0;
		rect.bottom += 0;
		wstring text = L"ImGui::FPS : " + to_wstring(ImGui::GetIO().Framerate);
		DirectWrite::RenderText(text, rect);

		rect.top += 20;
		rect.bottom += 20;
		text = L"Timer FPS : " + to_wstring(Time::Get()->FPS());
		DirectWrite::RenderText(text, rect);
	}
	DirectWrite::GetDC()->EndDraw();*/

	SwapChain->Present(0, 0);
	
}