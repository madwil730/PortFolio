#pragma once

class Math
{
public:
	static const float PI;

	static float ToRadian(float degree);
	static float ToDegree(float radian);

	static int Random(int r1, int r2);
	static float Random(float r1, float r2);

	static float Cross(D3DXVECTOR2 vec1, D3DXVECTOR2 vec2);
	static float Ccw(D3DXVECTOR2 vec1, D3DXVECTOR2 vec2);
	static float Ccw(D3DXVECTOR2 vec1, D3DXVECTOR2 vec2, D3DXVECTOR2 vec3);

	static bool SementIntersect(D3DXVECTOR2 a, D3DXVECTOR2 b, D3DXVECTOR2 c, D3DXVECTOR2 d);
};