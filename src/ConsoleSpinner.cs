using System;
using System.Collections.Generic;
using System.Threading;


namespace pwned_reader
{
    /// <summary>
    /// A simple command line spinner
    /// </summary>
    class ConsoleSpinner : IDisposable
    {
        private readonly int DEFAULT_SPEED = 100;
        private readonly List<string> STATES =
            new List<string>() { "\\", "|", "/", "-" };

        private Thread SpinnerThread { get; set; }
        private SpinnerState State { get; set; }
        private int CurrentState { get; set; }
        private int TotalStates { get; set; }

        public ConsoleSpinner()
        {
            var threadStart = new ThreadStart(Spin);
            SpinnerThread = new Thread(threadStart);
            SpinnerThread.IsBackground = true;
            SpinnerThread.Start();

            Speed = DEFAULT_SPEED;
            TotalStates = STATES.Count;
            CurrentState = -1;
        }

        public ConsoleSpinner(int speed)
            : this()
        {
            Speed = speed;
        }

        /// <summary>
        /// Get or set the spining speed
        /// </summary>
        /// <value></value>
        public int Speed { get; set; }

        public void Stop()
        {
            State = SpinnerState.Stopped;
        }

        public void Start()
        {
            State = SpinnerState.Started;
        }

        public void Clear()
        {
            if (State == SpinnerState.Started)
            {
                State = SpinnerState.Stopped;
            }

            Console.Write("\b \b");
        }

        private string NextState()
        {
            CurrentState++;

            if (CurrentState >= TotalStates)
            {
                CurrentState = 0;
            }

            return STATES[CurrentState];
        }
        private void Spin()
        {
            while (true)
            {
                if (State == SpinnerState.Started)
                {
                    Console.SetCursorPosition(0, Console.CursorTop);
                    Console.Write(NextState());
                    Thread.Sleep(Speed);
                }
            }

        }

        public void Dispose()
        {
            SpinnerThread.Abort();
            State = SpinnerState.Stopped;
        }

        private enum SpinnerState
        {
            Started,
            Stopped
        }
    }
}