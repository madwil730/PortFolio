#include "stdafx.h"
#include "SkyRect.h"

SkyRect::SkyRect(wstring shaderFile)
	:position(0, 0), scale(1, 1)
{
	CreateBuffer(shaderFile);
}

SkyRect::~SkyRect()
{
	SafeDelete(shader);
	SafeRelease(vertexBuffer);
}

void SkyRect::Udpate(D3DXMATRIX& V, D3DXMATRIX &P)
{
	D3DXMATRIX W, S, T;

	D3DXMatrixScaling(&S, scale.x, scale.y, 1.0f);
	D3DXMatrixTranslation(&T, position.x, position.y, 0.0f);
	W = S * T;

	shader->AsMatrix("World")->SetMatrix(W);

	shader->AsMatrix("View")->SetMatrix(V);
	shader->AsMatrix("Projection")->SetMatrix(P);

	shader->Variable("Sky")->SetRawValue(&sky, 0, sizeof(LinearColorVertex));
}

void SkyRect::Render()
{
	ImGui::ColorEdit3("Center", (float*)&sky.Center);
	ImGui::ColorEdit3("Apex", (float*)&sky.Apex);
	ImGui::SliderFloat("Height", (float*)&sky.Height, -10, 10);

	UINT stride = sizeof(Vertex);
	UINT offset = 0;

	DeviceContext->IASetVertexBuffers(0, 1, &vertexBuffer, &stride, &offset);
	DeviceContext->IASetPrimitiveTopology(D3D11_PRIMITIVE_TOPOLOGY_TRIANGLELIST);
	shader->Draw(0, 0, 6);
}

void SkyRect::Position(float x, float y)
{
	Position(D3DXVECTOR2(x, y));
}

void SkyRect::Position(D3DXVECTOR2 & vec)
{
	position = vec;
}

void SkyRect::Scale(float x, float y)
{
	Scale(D3DXVECTOR2(x, y));
}

void SkyRect::Scale(D3DXVECTOR2 & vec)
{
	scale = vec;
}

void SkyRect::CreateBuffer(wstring shaderFile)
{
	shader = new Shader(shaderFile);

	Vertex vertices[6];

	vertices[0].Position = D3DXVECTOR3(-0.5f, -0.5f, 0.0f);
	vertices[1].Position = D3DXVECTOR3(-0.5f, +0.5f, 0.0f);
	vertices[2].Position = D3DXVECTOR3(+0.5f, -0.5f, 0.0f);
	vertices[3].Position = D3DXVECTOR3(+0.5f, -0.5f, 0.0f);
	vertices[4].Position = D3DXVECTOR3(-0.5f, +0.5f, 0.0f);
	vertices[5].Position = D3DXVECTOR3(+0.5f, +0.5f, 0.0f);

	//Create VertexBuffer
	{
		D3D11_BUFFER_DESC desc;
		ZeroMemory(&desc, sizeof(D3D11_BUFFER_DESC));
		desc.Usage = D3D11_USAGE_DEFAULT;
		desc.ByteWidth = sizeof(Vertex) * 6;
		desc.BindFlags = D3D11_BIND_VERTEX_BUFFER;

		D3D11_SUBRESOURCE_DATA data;
		ZeroMemory(&data, sizeof(D3D11_SUBRESOURCE_DATA));
		data.pSysMem = vertices;

		HRESULT hr = Device->CreateBuffer(&desc, &data, &vertexBuffer);
		assert(SUCCEEDED(hr));
	}
}