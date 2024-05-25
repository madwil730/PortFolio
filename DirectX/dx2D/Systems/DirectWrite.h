#pragma once

//(1)DwriteFactory
//(2) Device
//(3) DeviceContext

class DirectWrite
{
private:
	DirectWrite();
	~DirectWrite();

private:
	static DirectWrite* instance;

private:
	ID2D1Factory1 * factory;
	static IDWriteFactory* writeFactory;

	ID2D1Device* device;
	static ID2D1DeviceContext* deviceContext;

	static ID2D1Bitmap1* bitmap; //백버퍼용
	static IDXGISurface* surface; //2D에서 RTV에 해당

	//TEXT
	static ID2D1SolidColorBrush* brush;
	static IDWriteTextFormat* format;

public:
	static void Create();
	static void Delete();

	static DirectWrite* Get();
	static ID2D1DeviceContext* GetDC() { return deviceContext; }

	static void CreateBackBuffer();
	static void DeleteBackBuffer();

	static void RenderText(wstring& text, RECT& rect);

};
