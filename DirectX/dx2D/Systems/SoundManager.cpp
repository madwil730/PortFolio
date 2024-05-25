#include "stdafx.h"
#include "SoundManager.h"

SoundManager::SoundManager()
	:system(NULL), channel(NULL), sound(NULL), buffers(15), volume(1.0f)
{
	//시스템 생성
	FMOD::System_Create(&system);

	//사운드 채널 생성
	system->init(buffers, FMOD_INIT_NORMAL, NULL);
	sound = new Sound*[buffers];
	channel = new Channel*[buffers];

	memset(sound, 0, sizeof(Sound*) * buffers);
	memset(channel, 0, sizeof(Channel*) * buffers);

}

SoundManager::~SoundManager()
{
	if (channel != NULL || sound != NULL)
	{
		for (UINT i = 0; i < buffers; i++)
		{
			if (channel != NULL)
			{
				if (channel[i])
					channel[i]->stop();
			}

			if (sound != NULL)
			{
				if (sound[i])
					sound[i]->release();
			}
		}
	}

	SafeDelete(channel);
	SafeDelete(sound);

	//시스템 닫기
	if (system != NULL)
	{
		system->release();
		system->close();
	}

	//맵 삭제
	sounds.clear();
}

void SoundManager::AddSound(string name, string soundFile, bool loop)
{
	if (loop == true)
	{
		system->createStream
		(
			soundFile.c_str(),
			FMOD_LOOP_NORMAL,
			NULL,
			&sound[sounds.size()]
		);
	}
	else
	{
		system->createStream
		(
			soundFile.c_str(),
			FMOD_DEFAULT,
			NULL,
			&sound[sounds.size()]
		);
	}

	sounds.insert(make_pair(name, &sound[sounds.size()]));
}

void SoundManager::Play(string name, float volume)
{
	int count = 0;
	iter = sounds.begin();
	for (iter; iter != sounds.end(); ++iter, count++)
	{
		if (name == iter->first)
		{
			system->playSound(FMOD_CHANNEL_FREE, *iter->second, false, &channel[count]);
			channel[count]->setVolume(volume);
		}
	}
}

void SoundManager::Stop(string name)
{
	int count = 0;
	iter = sounds.begin();
	for (iter; iter != sounds.end(); ++iter, count++)
	{
		if (name == iter->first)
		{
			channel[count]->stop();
			break;
		}
	}
}

void SoundManager::Pause(string name)
{
	int count = 0;
	iter = sounds.begin();
	for (iter; iter != sounds.end(); ++iter, count++)
	{
		if (name == iter->first)
		{
			channel[count]->setPaused(true);
			break;
		}
	}
}

void SoundManager::Resume(string name)
{
	int count = 0;
	iter = sounds.begin();
	for (iter; iter != sounds.end(); ++iter, count++)
	{
		if (name == iter->first)
		{
			channel[count]->setPaused(false);
			break;
		}
	}
}

void SoundManager::Volume(string name, float volume)
{
	int count = 0;
	iter = sounds.begin();
	for (iter; iter != sounds.end(); ++iter, count++)
	{
		if (name == iter->first)
		{
			channel[count]->setVolume(volume);
			break;
		}
	}
}

bool SoundManager::Playing(string name)
{
	bool bPlay = false;
	int count = 0;
	iter = sounds.begin();
	for (iter; iter != sounds.end(); ++iter, count++)
	{
		if (name == iter->first)
		{
			channel[count]->isPlaying(&bPlay);
			break;
		}
	}
	return bPlay;
}

bool SoundManager::Paused(string name)
{
	bool bPause = false;
	int count = 0;
	iter = sounds.begin();
	for (iter; iter != sounds.end(); ++iter, count++)
	{
		if (name == iter->first)
		{
			channel[count]->getPaused(&bPause);
			break;
		}
	}
	return bPause;
}

void SoundManager::Update()
{
	system->update();
}
