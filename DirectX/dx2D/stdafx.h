#pragma once

//Window
#include <Windows.h>
#include <assert.h>

//#ifdef _DEBUG
//#pragma comment(linker, "/entry:WinMainCRTStartup /subsystem:console")
//#endif

//STL
#include <vector>
#include <string>
#include <map>
#include <functional>
#include <iostream>
using namespace std;

//DX
#include <d3d11.h>
#include <D3DX11.h>
#include <D3DX10.h>
#include <D3DX10math.h>
#include <d3dx11effect.h>
#include <d3dcompiler.h>

#pragma comment(lib, "d3d11.lib")
#pragma comment(lib, "d3dx11.lib")
#pragma comment(lib, "d3dx10.lib")

#pragma comment(lib, "Effects11d.lib")
#pragma comment(lib, "d3dcompiler.lib")

//ImGui
#include <imgui.h>
#include <imguiDx11.h>
#pragma comment(lib, "imgui.lib")

//Direct Wirte
#include "d2d1_2.h"
#include <dwrite.h>
#pragma comment(lib, "d2d1.lib")
#pragma comment(lib, "dwrite.lib")

#include "./FMOD/fmod/fmod.hpp"
#pragma comment(lib, "./FMOD/fmod/fmodex_vc.lib")



//Framework


#include "./Systems/Keyboard.h"
#include "./Systems/CMouse.h"
#include "./Systems/Time.h"
#include "./Systems/DirectWrite.h"
#include "./Systems/SoundManager.h"
#include "./Scenes/Scene.h"

#include "./Utilities/String.h"
#include "./Utilities/Math.h"
#include "./Utilities/BinaryFile.h"
#include "./Utilities/Path.h"
#include "./Utilities/Xml.h"

#include "./Renders/Shader.h"
#include "./Renders/Rect.h"
#include "./Renders/Sprite.h"
#include "./Renders/Clip.h"
#include "./Renders/Animation.h"


//Macro
#define SafeRelease(p) { if(p) {(p)->Release(); (p) = NULL; } }
#define SafeDelete(p) { if(p) {delete (p); (p) = NULL; } }
#define SafeDeleteArray(p)	{ if(p) {delete[] (p); (p) = NULL; } }

//Global
extern UINT Width;
extern UINT Height;

const wstring Textures = L"./_Textures/";
const wstring Shaders = L"./_Shader2D/";

extern HWND Hwnd;
extern wstring Title;

//DX Interface
extern IDXGISwapChain* SwapChain;
extern ID3D11Device* Device;
extern ID3D11DeviceContext* DeviceContext;
extern ID3D11RenderTargetView* RTV;

extern Keyboard* Key;
extern CMouse* Mouse;

