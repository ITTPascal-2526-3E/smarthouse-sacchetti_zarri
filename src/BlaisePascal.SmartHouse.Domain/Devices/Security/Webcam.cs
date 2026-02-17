using OpenCvSharp;
using System;

public sealed class Webcam
{
    private VideoCapture _capture;
    private Window _window;
    private Mat _frame;
    private bool _isRunning = false;
    public Guid cam_Id { get; protected set; } = Guid.NewGuid();

    public Webcam(int cameraIndex = 0)
    {
        _capture = new VideoCapture(cameraIndex);
        _window = new Window("Webcam");
        _frame = new Mat();

        if (!_capture.IsOpened())
            throw new Exception("Impossibile aprire la webcam.");
    }

    public void Start()
    {
        _isRunning = true;
        
        while (_isRunning)
        {
            _capture.Read(_frame);
            if (_frame.Empty()) break;

            _window.ShowImage(_frame);

            if (Cv2.WaitKey(1) == 27)
                Stop();
        }

        Close();
    }

    public void Stop()
    {
        _isRunning = false;
    }

    public void Close()
    {
        _capture?.Release();
        _frame?.Dispose();
        _window?.Dispose();
    }
}

