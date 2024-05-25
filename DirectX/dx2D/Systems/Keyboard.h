#pragma once
#include <bitset>
/*
set() : bit를 1(true)로 set
reset() : bit를 0(false)로 설정
set(i, value) : i번째 인덱스의 값을 value로 설정
flip() : 반전 (0->1, 1->0)
all() : 모든 bit가 1일때 true를 반환
none() : 모든 bit가 0일때 true를 반환
any() : 1개라도 1이면 true 반환
count() : 전체 컨테이너에서 1의 개수를 반환

[] 배열 형태로 접근 가능 ex) bit[10] = false;
*/

#define KEYMAX 256

class Keyboard
{
private:
	bitset<KEYMAX> up; //-> bool up[256]
	bitset<KEYMAX> down;

public:
	Keyboard();
	~Keyboard();

	bool Down(int key);
	bool Up(int key);
	bool Press(int key);
	bool Toggle(int key);
};