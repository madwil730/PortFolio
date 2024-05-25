#pragma once
#include <bitset>
/*
set() : bit�� 1(true)�� set
reset() : bit�� 0(false)�� ����
set(i, value) : i��° �ε����� ���� value�� ����
flip() : ���� (0->1, 1->0)
all() : ��� bit�� 1�϶� true�� ��ȯ
none() : ��� bit�� 0�϶� true�� ��ȯ
any() : 1���� 1�̸� true ��ȯ
count() : ��ü �����̳ʿ��� 1�� ������ ��ȯ

[] �迭 ���·� ���� ���� ex) bit[10] = false;
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