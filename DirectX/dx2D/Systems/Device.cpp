#include "stdafx.h"
#include "Device.h"

int APIENTRY WinMain(HINSTANCE hInstance, HINSTANCE prevInstance, LPSTR lpCmdLine, int nCmdShow)
{
	InitWindow(hInstance, nCmdShow);
	InitDirect3D(hInstance);

	Running();
	Destroy();

	return 0;
}

UINT Width = 800;
UINT Height = 600;

HWND Hwnd = NULL;
wstring Title = L"D2D Game";

IDXGISwapChain* SwapChain = NULL; //백버퍼를 통합 관리하는 자원
ID3D11Device* Device = NULL; //자원 생성
ID3D11DeviceContext* DeviceContext = NULL; //GPU -> Render
ID3D11RenderTargetView* RTV = NULL; //Render 대상

Keyboard* Key = NULL;
CMouse* Mouse = NULL;

void InitWindow(HINSTANCE hInstance, int nCmdShow)
{
	//RegisterWindow
	{
		WNDCLASSEX wc;
		wc.cbSize = sizeof(WNDCLASSEX);
		wc.style = CS_HREDRAW | CS_VREDRAW;
		wc.lpfnWndProc = WndProc;
		wc.cbClsExtra = NULL;
		wc.cbWndExtra = NULL;
		wc.hInstance = hInstance;
		wc.hIcon = LoadIcon(NULL, IDI_APPLICATION);
		wc.hCursor = LoadCursor(NULL, IDC_ARROW);
		wc.hbrBackground = (HBRUSH)(WHITE_BRUSH);
		wc.lpszMenuName = NULL;
		wc.lpszClassName = Title.c_str();
		wc.hIconSm = LoadIcon(NULL, IDI_APPLICATION);

		WORD check = RegisterClassEx(&wc);
		assert(check != NULL);
	}

	//Create Handle
	{
		Hwnd = CreateWindowEx
		(
			NULL,
			Title.c_str(),
			Title.c_str(),
			WS_OVERLAPPEDWINDOW,
			CW_USEDEFAULT,
			CW_USEDEFAULT,
			Width,
			Height,
			NULL,
			NULL,
			hInstance,
			NULL
		);
		
		assert(Hwnd != NULL);
	}

	RECT rect = { 0, 0, (LONG)Width, (LONG)Height };
	UINT centerX = (GetSystemMetrics(SM_CXSCREEN) - (UINT)Width) / 2;
	UINT centerY = (GetSystemMetrics(SM_CYSCREEN) - (UINT)Height) / 2;
	AdjustWindowRect(&rect, WS_OVERLAPPEDWINDOW, FALSE);
	MoveWindow
	(
		Hwnd,
		centerX, centerY,
		rect.right - rect.left, rect.bottom - rect.top,
		TRUE
	);


	ShowWindow(Hwnd, nCmdShow);
	UpdateWindow(Hwnd);
		

}

//IA(위치, 형태, 모양) -> VS(공간변환) -> RS(3D->2D, 픽셀폐기) -> PS(색상변환) -> OM(화면 출력)
void InitDirect3D(HINSTANCE hInstance)
{
	//SwapChain -> Buffer
	DXGI_MODE_DESC bufferDesc;
	ZeroMemory(&bufferDesc, sizeof(DXGI_MODE_DESC));
	bufferDesc.Width = Width;
	bufferDesc.Height = Height;
	bufferDesc.RefreshRate.Numerator = 60;
	bufferDesc.RefreshRate.Denominator = 1;
	bufferDesc.Format = DXGI_FORMAT_R8G8B8A8_UNORM;
	bufferDesc.ScanlineOrdering = DXGI_MODE_SCANLINE_ORDER_UNSPECIFIED;
	bufferDesc.Scaling = DXGI_MODE_SCALING_UNSPECIFIED;

	//SwapChain -> Sampling
	DXGI_SWAP_CHAIN_DESC swapDesc;
	ZeroMemory(&swapDesc, sizeof(DXGI_SWAP_CHAIN_DESC));
	swapDesc.BufferDesc = bufferDesc;
	swapDesc.BufferUsage = DXGI_USAGE_RENDER_TARGET_OUTPUT;
	swapDesc.BufferCount = 1;
	swapDesc.SampleDesc.Count = 1;
	swapDesc.SampleDesc.Quality = 0;
	swapDesc.OutputWindow = Hwnd;
	swapDesc.Windowed = TRUE;
	swapDesc.SwapEffect = DXGI_SWAP_EFFECT_DISCARD;

	//Featured Level
	vector<D3D_FEATURE_LEVEL> featured_level =
	{
		D3D_FEATURE_LEVEL_11_1,
		D3D_FEATURE_LEVEL_11_0,
		D3D_FEATURE_LEVEL_10_1,
		D3D_FEATURE_LEVEL_10_0,
		D3D_FEATURE_LEVEL_9_3,
		D3D_FEATURE_LEVEL_9_2,
		D3D_FEATURE_LEVEL_9_1,
	};

#ifdef _DEBUG
	UINT creationFlags = D3D11_CREATE_DEVICE_BGRA_SUPPORT;
#endif


	HRESULT hr =  D3D11CreateDeviceAndSwapChain
	(
		NULL,
		D3D_DRIVER_TYPE_HARDWARE,
		NULL,
		creationFlags,
		featured_level.data(),
		featured_level.size(),
		D3D11_SDK_VERSION,
		&swapDesc,
		&SwapChain,
		&Device,
		NULL,
		&DeviceContext
	);

	assert(SUCCEEDED(hr));

	
	CreateBackBuffer();

}

void Destroy()
{	
	DeleteBackBuffer();

	SafeRelease(DeviceContext);
	SafeRelease(SwapChain);
	SafeRelease(Device);
}

void CreateBackBuffer()
{
	//BackBuffer
	ID3D11Texture2D* BackBuffer;
	HRESULT hr = SwapChain->GetBuffer(0, __uuidof(ID3D11Texture2D), (void**)&BackBuffer);
	assert(SUCCEEDED(hr));

	//Create RTV
	hr = Device->CreateRenderTargetView(BackBuffer, NULL, &RTV);
	assert(SUCCEEDED(hr));
	BackBuffer->Release();

	//OM
	DeviceContext->OMSetRenderTargets(1, &RTV, NULL);

	//Create ViewPort
	{
		D3D11_VIEWPORT viewPort;
		ZeroMemory(&viewPort, sizeof(D3D11_VIEWPORT));
		viewPort.TopLeftX = 0;
		viewPort.TopLeftY = 0;
		viewPort.Width = (FLOAT)Width;
		viewPort.Height = (FLOAT)Height;

		DeviceContext->RSSetViewports(1, &viewPort);
	}
}

void DeleteBackBuffer()
{
	SafeRelease(RTV);
}

WPARAM Running()
{
	MSG msg;
	ZeroMemory(&msg, sizeof(MSG));

	ImGui::Create(Hwnd, Device, DeviceContext);
	ImGui::StyleColorsDark();

	DirectWrite::Create();

	Time::Create();
	Time::Get()->Start();

	Key = new Keyboard();
	Mouse = new CMouse(Hwnd);

	InitScene();
	//////////////////////////////////////////////////////
	while (true)
	{
		if (PeekMessage(&msg, NULL, 0, 0, PM_REMOVE))
		{
			if (msg.message == WM_QUIT) break;
			TranslateMessage(&msg);
			DispatchMessage(&msg);
		}
		else
		{
			Time::Get()->Update();
			Mouse->Update();
			Update();
			ImGui::Update();
			Render();
		}
	}
	
	//////////////////////////////////////////////////////

	SafeDelete(Mouse);
	SafeDelete(Key);

	Time::Delete();
	ImGui::Delete();
	DirectWrite::Delete();

	DestroyScene();

	return msg.wParam;

}

LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	if (ImGui::WndProc((UINT*)hwnd, msg, wParam, lParam))
		return true;

	if (Mouse != NULL)
		Mouse->WndProc(msg, wParam, lParam);

	switch (msg)
	{
	case WM_SIZE:
	{
		if (Device != NULL)
		{
			ImGui::Invalidate();

			Width = LOWORD(lParam);
			Height = HIWORD(lParam);

			DeleteBackBuffer();
			DirectWrite::DeleteBackBuffer();

			HRESULT hr = SwapChain->ResizeBuffers(0, Width, Height, DXGI_FORMAT_UNKNOWN, 0);
			assert(SUCCEEDED(hr));

			DirectWrite::CreateBackBuffer();
			CreateBackBuffer();

			ImGui::Validate();
		}
	}
	break;
	case WM_DESTROY: PostQuitMessage(0); return 0;
	}

	return DefWindowProc(hwnd, msg, wParam, lParam);
}
