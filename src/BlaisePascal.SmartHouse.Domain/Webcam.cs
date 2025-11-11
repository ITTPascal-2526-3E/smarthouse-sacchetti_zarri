using OpenCvSharp;
using System;

public class Webcam
{
    private VideoCapture _capture;
    private Window _window;
    private Mat _frame;
    private bool _isRunning = false;
    public Guid cam_Id { get; set; } = Guid.NewGuid();

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

            // ESC = 27
            if (Cv2.WaitKey(1) == 27)
                Stop();
        }

        // Rilascia le risorse manualmente
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

