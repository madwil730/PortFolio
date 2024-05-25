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
	INT64 ticksPerSecond;	/// <�ʴ� ƽī��Ʈ
	INT64 currentTime;		/// < ���� �ð�
	INT64 lastTime;			/// < ���� �ð�
	
	//FPS
	INT64 lastFPSUpdate;	/// < FPS ��ġ�� ���������� ���ŵ� �ð�
	INT64 fpsUpadateInterval;	///< FPS ������Ʈ ����
	UINT frameCount;		///������ ��
	float runningTime;		///����ð�
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