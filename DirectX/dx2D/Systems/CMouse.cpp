#include "stdafx.h"
#include "CMouse.h"

CMouse::CMouse(HWND handle)
	: handle(handle), position(0,0)
	, wheelValue(0), wheelMoveValue(0), wheelPrevValue(0)
{
	ZeroMemory(buttonStatus, sizeof(BYTE) * 8);
	ZeroMemory(buttonPrevStatus, sizeof(BYTE) * 8);
	ZeroMemory(buttonMap, sizeof(Button) * 8);

	ZeroMemory(buttonCount, sizeof(int) * 8);

	doubleClickTime = GetDoubleClickTime();
	startDoubleClickTime[0] = GetTickCount();

	for (int i = 1; i < 8; i++)
		startDoubleClickTime[i] = startDoubleClickTime[0];
}

CMouse::~CMouse()
{
}

void CMouse::WndProc(UINT iMessage, WPARAM wParam, LPARAM lParam)
{
	if (iMessage == WM_MOUSEMOVE)
	{
		position.x = (float)LOWORD(lParam);
		position.y = (float)HIWORD(lParam);
	}

	if (iMessage == WM_MOUSEWHEEL)
	{
		long temp = HIWORD(wParam);

		wheelPrevValue = wheelValue;
		wheelValue += (temp);
	}
}

void CMouse::Update()
{
	memcpy(buttonPrevStatus, buttonStatus, sizeof(BYTE) * 8);

	ZeroMemory(buttonStatus, sizeof(BYTE) * 8);
	ZeroMemory(buttonMap, sizeof(Button) * 8);

	buttonStatus[0] = GetAsyncKeyState(VK_LBUTTON) & 0x8000 ? 1 : 0;
	buttonStatus[1] = GetAsyncKeyState(VK_RBUTTON) & 0x8000 ? 1 : 0;
	buttonStatus[2] = GetAsyncKeyState(VK_MBUTTON) & 0x8000 ? 1 : 0;

	for (UINT i = 0; i < 8; i++)
	{
		BYTE prevStatus = buttonPrevStatus[i];
		BYTE currentStatus = buttonStatus[i];

		if (prevStatus == 0 && currentStatus == 1)
			buttonMap[i] = Button::Down;
		else if (prevStatus == 1 && currentStatus == 0)
			buttonMap[i] = Button::Up;
		else if (prevStatus == 1 && currentStatus == 1)
			buttonMap[i] = Button::Press;
		else
			buttonMap[i] = Button::None;

	}

	//����Ŭ��
	UINT buttonStatus = GetTickCount();
	for (UINT i = 0; i < 8; i++)
	{
		if (buttonMap[i] == Button::Down) //Ư�� ��ư�� ���� ���� �ִٸ�
		{
			if (buttonCount[i] == 1) //1�� ���� ���
			{
				//�ι�° ���� Ÿ�� ������ - ù��° ���� Ÿ�� ������ > ����Ŭ�� ��� �ð�
				if (buttonStatus - startDoubleClickTime[i] >= doubleClickTime)
					buttonCount[i] = 0;
			}
				

			buttonCount[i]++; //Todo

							  //��ư�� �ѹ� ���� ���¸� ���� �ð��� �ٽ� ����
			if (buttonCount[i] == 1)
				startDoubleClickTime[i] = buttonStatus;
		}

		

		if (buttonMap[i] == Button::Up)
		{
			if (buttonCount[i] == 1)
			{
				//����Ŭ�� ����
				if (buttonStatus - startDoubleClickTime[i] >= doubleClickTime)
					buttonCount[i] = 0;
			}

			else if (buttonCount[i] == 2)
			{
				if (buttonStatus - startDoubleClickTime[i] <= doubleClickTime) //����Ŭ�� ����
					buttonMap[i] = Button::DoubleClick;

				buttonCount[i] = 0;
			}
		}
	}

	POINT point;
	GetCursorPos(&point); //���콺 ��ġ(OS����)
	ScreenToClient(handle, &point); //�츮 ȭ�鿡 �ݿ�

	wheelPrevValue = wheelValue;
	wheelMoveValue = wheelValue - wheelPrevValue;

}
