#pragma once

class  ParticleEditor
{
public:
	 ParticleEditor();

	 void UpdateParticleList();
	 void UpdateTextureList();

	 void SetSkill(Vector3 transform);
	 void Update();
	 void Render();
	 void OnGUI();
	 void OnGUI_List();
	 void OnGUI_Settings();
	 void OnGUI_Write();
	 void WriteFile(wstring file);

	 Shader * shader;
	 ParticleSystem* particleSystem = NULL;

	 float windowWidth = 500;
	 bool bLoop = false;
	 UINT maxParticle = 0;

	 vector<wstring> particleList;
	 vector<wstring> textureList;
	 wstring file = L"";

};

