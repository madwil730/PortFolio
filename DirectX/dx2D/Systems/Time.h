#pragma once

class Time
{
private:
	Time();
	~Time();

	static Time* instance;

	static bool isTimerStopped;
	static float timeElapsed;

	//Delta Time
	INT64 ticksPerSecond;	/// <초당 틱카운트
	INT64 currentTime;		/// < 현재 시간
	INT64 lastTime;			/// < 이전 시간
	
	//FPS
	INT64 lastFPSUpdate;	/// < FPS 수치가 마지막으로 갱신된 시간
	INT64 fpsUpadateInterval;	///< FPS 업데이트 간격
	UINT frameCount;		///프레임 수
	float runningTime;		///진행시간
	float framePerSecond;	/// < FPS


public:
	static Time* Get();

	static void Create();
	static void Delete();

	static bool Stopped() { return isTimerStopped; }
	static float Delta() { return isTimerStopped ? 0.0f : timeElapsed; }

	void Update();

	void Start();
	void Stop();

	float FPS() { return framePerSecond; }
	float Running() { return runningTime; }

};