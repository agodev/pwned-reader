using System;
using System.Threading;

public class ConsoleSpinner : IDisposable
{
    private Thread _spinnerThread;
    private SpinnerState _state;

    public ConsoleSpinner()
    {
        var threadStart = new ThreadStart(Spin);
        _spinnerThread = new Thread(threadStart);
        _spinnerThread.IsBackground = true;
        _spinnerThread.Start();
    }

    public void Stop()
    {
        _state = SpinnerState.Stopped;
    }

    public void Start()
    {
        _state = SpinnerState.Started;
    }

    public void Clear()
    {

    }

    private void Spin()
    {
        while (true)
        {
            if (_state == SpinnerState.Started)
            {
                Console.WriteLine("Spinning");
                Thread.Sleep(500);
            }
        }

    }

    public void Dispose()
    {
        _spinnerThread.Abort();
        _state = SpinnerState.Stopped;
    }

    private enum SpinnerState
    {
        Started,
        Stopped
    }
}