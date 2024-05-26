using RenderHeads.Media.AVProLiveCamera;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageCrop : MonoBehaviour
{
    public AVProLiveCamera _AVProLiveCamera;
    RawImage _CropImage;

    // Start is called before the first frame update
    void Start()
    {
        _CropImage = GetComponent<RawImage>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_AVProLiveCamera != null)
        {
            if(_CropImage != null)
            {
                //Texture2D _texture2D = new Texture2D(1920, 1080, TextureFormat.ARGB32, false);
                //_texture2D = (Texture2D)
                //Color[] color = _texture2D.GetPixels();
                //_texture2D.SetPixels(color);
                _CropImage.texture = _AVProLiveCamera.OutputTexture;
            }
        }
    }
}
