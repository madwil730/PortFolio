#include "stdafx.h"
#include "Time.h"

Time* Time::instance = NULL;

bool Time::isTimerStopped = true;
float Time::timeElapsed = 0.0f;

Time::Time()
	: ticksPerSecond(0), currentTime(0), lastTime(0)
	, lastFPSUpdate(0), fpsUpadateInterval(0), frameCount(0)
	, runningTime(0), framePerSecond(0)
{
	QueryPerformanceFrequency((LARGE_INTEGER*)&ticksPerSecond);
	fpsUpadateInterval = ticksPerSecond >> 1;

}

Time::~Time()
{
}

Time * Time::Get()
{
	assert(instance != NULL);

	return instance;
}

void Time::Create()
{
	assert(instance == NULL);

	instance = new Time();
}

void Time::Delete()
{
	SafeDelete(instance);
}

void Time::Update()
{
	if (isTimerStopped) return;

	//1. 현재 시간을 가져와서 시간 간격 및 진행 시간을 계산
	QueryPerformanceCounter((LARGE_INTEGER*)&currentTime);
	timeElapsed = (float)(currentTime - lastTime) / (float)ticksPerSecond;
	runningTime += timeElapsed;

	//2. FPS Update
	frameCount++;
	if (currentTime - lastFPSUpdate >= fpsUpadateInterval)
	{
		float tempCurrentTime = (float)currentTime / (float)ticksPerSecond;
		float tempLastTime = (float)lastFPSUpdate / (float)ticksPerSecond;
		framePerSecond = (float)frameCount / (tempCurrentTime - tempLastTime);

		lastFPSUpdate = (INT64)currentTime;
		frameCount = 0;
	}


	lastTime = currentTime;

}

void Time::Start()
{
	if (!isTimerStopped)
		assert(false);

	QueryPerformanceCounter((LARGE_INTEGER*)&lastTime);
	isTimerStopped = false;
}

void Time::Stop()
{
	if (isTimerStopped)
		assert(false);

	INT64 stopTime = 0;
	QueryPerformanceCounter((LARGE_INTEGER*)&stopTime);

	runningTime += (float)(stopTime - lastTime) / (float)ticksPerSecond;
	isTimerStopped = true;

}
