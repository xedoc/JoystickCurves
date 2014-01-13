using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JoystickCurves
{
    public class AxisFilterKalman
    {

        public double? PredictedState { get; private set; }
        public double? PredictedCovariance { get; private set; }

        public double? FactorRealToPrevious { get; private set; }
        public double? Noise { get; private set; }
        public double? FactorMeasuredToReal { get; private set; }
        public double? EnvironmentNoise { get; private set; }

        public double? State { get; private set; }
        public double? Covariance { get; private set; }

        public AxisFilterKalman(double q, double r, double f = 1, double h = 1)
        {
            Noise = q;
            EnvironmentNoise = r;
            FactorRealToPrevious = f;
            FactorMeasuredToReal = h;
        }

        public void SetState(double state, double covariance)
        {
            State = state;
            Covariance = covariance;
        }

        public double? Correct(double data)
        {
            if (State == null)
                SetState(data, 0.1f);

            PredictedState = FactorRealToPrevious * State;
            PredictedCovariance = FactorRealToPrevious * Covariance * FactorRealToPrevious + Noise;

            var K = FactorMeasuredToReal * PredictedCovariance / (FactorMeasuredToReal * PredictedCovariance * FactorMeasuredToReal + EnvironmentNoise);
            State = PredictedState + K * (data - FactorMeasuredToReal * PredictedState);
            Covariance = (1 - K * FactorMeasuredToReal) * PredictedCovariance;

            return State;
        }

    }
}
