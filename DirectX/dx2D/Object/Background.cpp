#include "stdafx.h"
#include "Background.h"

Background::Background(wstring shaderFile)
	: position(0, 0), scale(1, 1)
{
	Initialize();
}

Background::Background(wstring shaderFile, D3DXVECTOR2 position, D3DXVECTOR2 scale)
	: position(position), scale(scale)
{
	Initialize();
}

void Background::Initialize()
{
	background = new Sprite(Textures + L"./my/Back_SpaceBlue.png", Shaders + L"Effect.fx");
	float width = background->TextureSize().x * scale.x;
	float height = background->TextureSize().y * scale.y;
	textureSize = D3DXVECTOR2(width, height);
}


Background::~Background()
{
	SafeDelete(background);	
}

void Background::Update(D3DXMATRIX & V, D3DXMATRIX & P)
{
	position.x -= 100 * Time::Delta();

	background->Scale(scale);
	background->Position(position);
	background->Update(V, P);
}

void Background::Render()
{
	background->Render();
}

// set
void Background::Position(float x, float y)
{
	Position(D3DXVECTOR2(x, y));
}

// set
void Background::Position(D3DXVECTOR2 & vec)
{
	position = vec;
}

// get
D3DXVECTOR2 Background::Position()
{
	return position;
}

void Background::Scale(float x, float y)
{
	Scale(D3DXVECTOR2(x, y));
}

void Background::Scale(D3DXVECTOR2 & vec)
{
	scale = vec;
}

D3DXVECTOR2 Background::Scale()
{
	return scale;
}

Sprite * Background::GetSprite()
{
	return background;
}

