#include "stdafx.h"
#include "Line.h"

Line::Line(D3DXVECTOR2 position, D3DXVECTOR2 position2)
	: position(0,0), scale(1,1), left(position), right(position2)
	, bDrawCollision(false), slope(0)
{
	wstring shaderFile = Shaders + L"Line.fx";

	CreateBuffer(shaderFile, position, position2);

	if (position.x > position2.x)
	{
		left = position2;
		right = position;
	}
	else
	{
		left = position;
		right = position2;
	}

	slope = fabsf((position2.y - position.y) / (position2.x - position.x));
}

Line::~Line()
{
	SafeDelete(shader);
	SafeRelease(vertexBuffer);
}

void Line::Update(D3DXMATRIX& V, D3DXMATRIX& P)
{
	shader->AsMatrix("View")->SetMatrix(V);
	shader->AsMatrix("Projection")->SetMatrix(P);

	D3DXMATRIX W, S, T;
	D3DXMatrixScaling(&S, scale.x, scale.y, 1.0f);
	D3DXMatrixTranslation(&T, position.x, position.y, 0.0f);

	W = S * T;
	shader->AsMatrix("World")->SetMatrix(W);

	vertices[0].Position = D3DXVECTOR3(left.x, left.y, 0);
	vertices[1].Position = D3DXVECTOR3(right.x, right.y, 0);

	DeviceContext->UpdateSubresource
	(
		vertexBuffer, 0, NULL, vertices, sizeof(Vertex) * 2, 0
	);
}

void Line::Render()
{
	UINT stride = sizeof(Vertex);
	UINT offset = 0;

	DeviceContext->IASetVertexBuffers(0, 1, &vertexBuffer, &stride, &offset);
	DeviceContext->IASetPrimitiveTopology(D3D11_PRIMITIVE_TOPOLOGY_LINELIST);

	shader->Draw(0, bDrawCollision ? 1 : 0, 2);
}

void Line::CreateBuffer(wstring shaderFile, D3DXVECTOR2 position, D3DXVECTOR2 position2)
{
	shader = new Shader(shaderFile);

	vertices[0].Position = D3DXVECTOR3(position.x, position.y, 0);
	vertices[1].Position = D3DXVECTOR3(position2.x, position2.y, 0);

	//Create Vertex Buffer
	{
		D3D11_BUFFER_DESC desc = { 0 };
		desc.Usage = D3D11_USAGE_DEFAULT;
		desc.ByteWidth = sizeof(Vertex) * 2;
		desc.BindFlags = D3D11_BIND_VERTEX_BUFFER;

		D3D11_SUBRESOURCE_DATA data = { 0 };
		//Todo : ZeroMemory
		data.pSysMem = vertices;

		HRESULT hr =  Device->CreateBuffer(&desc, &data, &vertexBuffer);
		assert(SUCCEEDED(hr));
	}
}

