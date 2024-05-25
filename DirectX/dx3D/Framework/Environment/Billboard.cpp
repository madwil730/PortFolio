#include "Framework.h"
#include "Billboard.h"

Billboard::Billboard(Shader * shader)
	: Renderer(shader), v(NULL)
{
	Topology(D3D11_PRIMITIVE_TOPOLOGY_POINTLIST);

	sMaps = shader->AsSRV("Maps");
}

Billboard::~Billboard()
{
	SafeDelete(textures);
}

void Billboard::Add(Vector3 & position, Vector2 & scale, UINT mapIndex) // map index 왜 있음?
{
	VertexScale vertex;
	vertex.Position = position;
	vertex.Scale = scale;
	vertex.MapIndex = mapIndex;

	vertices.push_back(vertex);

	v = new VertexScale[vertices.size()];
}

void Billboard::AddTexture(wstring file)
{
	SafeDelete(textures);

	textureFiles.push_back(file);
}

void Billboard::CreateBuffer()
{
	if (textureFiles.size() > 0 && textures == NULL)
		textures = new TextureArray(textureFiles);

	if (vertices.size() != vertexCount)
	{
		vertexCount = vertices.size();

		SafeDelete(vertexBuffer);
		vertexBuffer = new VertexBuffer(&vertices[0], vertices.size(), sizeof(VertexScale), 0, true); 
	}
}

void Billboard::Update()
{
	Super::Update();
	
}

void Billboard::Render()
{
	Super::Render();

	sMaps->SetResource(textures->SRV());
	shader->Draw(0, Pass(), vertexCount);
}

void Billboard::Apply() // 이걸로 세이더로 보내는 버퍼 정보 수정, IA 수정
{

	D3D11_MAPPED_SUBRESOURCE subResource;

	D3D::GetDC()->Map(vertexBuffer->Buffer(), 0, D3D11_MAP_WRITE_DISCARD, 0, &subResource);
	{
		memcpy(subResource.pData, &vertices[0], sizeof(vertices[0]));
	}
	D3D::GetDC()->Unmap(vertexBuffer->Buffer(), 0);
}
