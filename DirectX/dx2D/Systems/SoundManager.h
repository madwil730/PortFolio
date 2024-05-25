#pragma once

using namespace FMOD;

class SoundManager
{
private:
	System * system; //Soud Device
	Sound** sound; //Sound Resource(음원)
	Channel** channel; //재생 채널

	UINT buffers; //음원 출력 개수

	map<string, Sound**> sounds;
	map<string, Sound**>::iterator iter;

	float volume;

public:
	SoundManager();
	~SoundManager();

	void AddSound(string name, string soundFile, bool loop = false);
	void Play(string name, float volume = 1.0f);
	void Stop(string name);
	void Pause(string name);
	void Resume(string name);

	void Volume(string name, float volume);
	float Volume() { return volume; }

	bool Playing(string name);
	bool Paused(string name);

	void Update();


};