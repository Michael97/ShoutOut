///\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\///
///                                                                                             ///
///     File name       :   GunVisualiser.cs                                                    ///
///                                                                                             ///
///     Author          :   Peter Phillips                                                      ///
///                                                                                             ///
///     Date created    :   14.05.2019                                                          ///
///                                                                                             ///
///     Last Modified   :   14.05.2019                                                          ///
///                                                                                             ///
///     Brief           :   Script to control the colour and size of the gun visualiser.        ///
///                                                                                             ///
///\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\///


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunVisualiser : MonoBehaviour
{    
    [SerializeField] public Transform[] _audioVisualiserBars;     /// The array for our audio visualiser bars.
    [SerializeField] private Transform[] _audioVisualiserShadow;  /// The array for our audio visualiser shadow.
    [SerializeField] private float _sizeMultiplier = 100000;      /// Determines the amount by which the audio visualiser bars expand based on input.
        // Samples = # of bands * 2^8.
    [SerializeField] private int _numberOfSamples = 4096;         /// Number of audio samples taken (determines frequency range for each frequency band).
    [SerializeField] private FFTWindow _fftWindow;                /// Different window options reduce signal leakage across frequency bands by different methods.
    [SerializeField] private float _stepSize = .2f;               /// The amount by which the lerp function will move what it's lerping (lower values give smoother/slower motion).
    [SerializeField] private GameObject _maxBarParticle;
    [SerializeField] private GameObject _player;

    private float[] _spectrumData;              /// An array for our audio spectrum data;
    private float _previousScale = 0f;          /// Used to compare current value to previous value;
    private float _ringDistance = 3.9f;         /// Distance between ring screens and map centre.
    private float _screenHeight = .12f;         /// Height of the ring screens.

    void Start()
    {
        // Set the initial colours of the bars.
        SetBarColours();

        // Set our spectrum data array to our sample size.
        _spectrumData = new float[_numberOfSamples];
    }

    private void SetBarColours()
    {
        // Loop through our array of bars and set them to different colours of the rainbow and position them in an arc around the map centre.
        for (int i = 0; i < _audioVisualiserBars.Length; i++)
        {
            // Set rainbow colours.
            Color _colour = Color.HSVToRGB(i * .875f / _audioVisualiserBars.Length, 1, 1);
            // Set alpha.
            _colour.a = 1f;
            // Set colour (if using standard type shaders);
            _audioVisualiserBars[i].GetComponent<Renderer>().material.color = _colour;
            // Set tint colour (if using FX type shaders);
            _audioVisualiserBars[i].GetComponent<Renderer>().material.SetColor("_TintColor", _colour);
            // Repeat for the shadows.
            _colour.a = .25f;
            _audioVisualiserShadow[i].GetComponent<Renderer>().material.color = _colour;
            _audioVisualiserShadow[i].GetComponent<Renderer>().material.SetColor("_TintColor", _colour);

            //// Move the bars to the edge of the ring.
            //_audioVisualiserBars[i].position += _audioVisualiserBars[i].up * (Vector3.Distance(_audioVisualiserBars[i].position, Vector3.zero) - _ringDistance);
            //// Make the bars face the centre.
            //_audioVisualiserBars[i].LookAt(Vector3.zero);
            //// Flip the bars back up because we actually need their 'up' vectors facing the centre to see them.
            //_audioVisualiserBars[i].Rotate(90f * Vector3.right);
            //// Reapeat for the shadows.
            //_audioVisualiserShadow[i].position += _audioVisualiserShadow[i].up * (Vector3.Distance(_audioVisualiserShadow[i].position, Vector3.zero) - _ringDistance);
            //_audioVisualiserShadow[i].LookAt(Vector3.zero);
            //_audioVisualiserShadow[i].Rotate(90f * Vector3.right);
        }
    }

    void Update()
    {
        // Set the size of the bars.
        SetBarSize();
    }

    private void SetBarSize()
    {
        // Put our spectrum data into our spectrum array accross all channels and using the window type we have selected in the inspector.
        _player.GetComponent<AudioSource>().GetSpectrumData(_spectrumData, 0, _fftWindow);

        // Loop through our audio visualiser bars and update their size.
        for (int i = 0; i < _audioVisualiserBars.Length; i++)
        {
            // Linearly interpolate from the current size to the detected input size by our step size.
            float lerpZ = Mathf.Lerp(_audioVisualiserBars[i].localScale.z, _spectrumData[i + 6] * _sizeMultiplier, _stepSize);  /// The first 6 frequency bands have been omitted as they are largely affected by background noise.
            float lerpZShadow = Mathf.Lerp(_audioVisualiserShadow[i].localScale.z, _spectrumData[i + 6] * _sizeMultiplier, _stepSize / 5);

            // Update the size of each bar (only the z-scale is changing).
            _audioVisualiserBars[i].localScale = new Vector3(_audioVisualiserBars[i].localScale.x, _audioVisualiserBars[i].localScale.y, Mathf.Clamp(lerpZ, 0, _screenHeight));
            _audioVisualiserShadow[i].localScale = new Vector3(_audioVisualiserShadow[i].localScale.x,
                                                                _audioVisualiserShadow[i].localScale.y,
                                                                _audioVisualiserBars[i].localScale.z > _previousScale && _audioVisualiserBars[i].localScale.z >= _audioVisualiserShadow[i].localScale.z ? _audioVisualiserBars[i].localScale.z : Mathf.Clamp(lerpZShadow, 0, _screenHeight));  /// Shadow quickly jumps up but slowly moves down.

            if (lerpZ >= _screenHeight)
            {
                GameObject _particle = Instantiate(_maxBarParticle, _audioVisualiserBars[i].position + Vector3.up * .25f, Quaternion.identity);
                _particle.GetComponent<Renderer>().material.SetColor("_TintColor", _audioVisualiserBars[i].GetComponent<Renderer>().material.GetColor("_TintColor"));
                Destroy(_particle, .2f);
            }

            // Update the scale tracker.
            _previousScale = _audioVisualiserBars[i].localScale.z;
        }
    }
}
