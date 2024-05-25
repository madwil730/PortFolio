#pragma once

class Dome : public Renderer
{
public:
	Dome(Shader* shader, Vector3 position, Vector3 scale, UINT drawCount = 32);
	~Dome();

	void Update() override;
	void Render() override;

private:
	UINT drawCount;	
	UINT latitude;
	UINT longitude;

	Texture* starMap;
	ID3DX11EffectShaderResourceVariable* sStarMap;
};