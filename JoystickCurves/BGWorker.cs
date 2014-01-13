using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading;

namespace JoystickCurves
{
    public class BGWorker
    {
        private BackgroundWorker bw;
        public delegate void StartFunc();
        public delegate void CompleteFunc();

        private StartFunc _startFunc;
        private CompleteFunc _completeFunc;

        public BGWorker(StartFunc startFunc, CompleteFunc completeFunc)
        {

            _startFunc = startFunc;
            _completeFunc = completeFunc;
            bw = new BackgroundWorker();
            bw.WorkerSupportsCancellation = true;
            bw.DoWork += DoWork;
            bw.RunWorkerCompleted += Complete;
            bw.RunWorkerAsync();
        }
        public bool IsComplete
        {
            get;
            set;
        }
        public void Stop()
        {
            //bw.DoWork -= DoWork;
            //bw.RunWorkerCompleted -= Complete;
            //if (bw.IsBusy)
            bw.CancelAsync();
            bw.Dispose();
        }
        public bool CancelationPending
        {
            get { return bw.CancellationPending; }
        }
        private void DoWork(object sender, DoWorkEventArgs e)
        {
            IsComplete = false;
            if (_startFunc != null)
                _startFunc();
        }
        private void Complete(object sender, RunWorkerCompletedEventArgs e)
        {
            if (_completeFunc != null)
                _completeFunc();

            IsComplete = true;
        }
    }
}
