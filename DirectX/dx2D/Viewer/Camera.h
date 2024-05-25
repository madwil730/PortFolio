#pragma once

class Camera
{
private:
	D3DXMATRIX view;

protected :
	D3DXVECTOR2 position;

public:
	Camera();
	virtual ~Camera();

	D3DXMATRIX View() { return view; }
	D3DXVECTOR2 Position() { return position; }
	void Position(float x, float y);
	void Position(D3DXVECTOR2& vec);

	virtual void Update();

};
