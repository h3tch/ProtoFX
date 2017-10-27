using System.Collections.Generic;

namespace protofx
{
    class Double
    {
        public double value;
        private List<Double> Targets;
        public UpdateEvent OnUpdate;
        public delegate void UpdateEvent(double value);

        public void Connect(Double other)
        {
            if (Targets == null)
                Targets = new List<Double>();
            Targets.Add(other);
        }

        public void Update()
        {
            if (Targets == null)
                return;

            for (int i = 0; i < Targets.Count; i++)
            {
                var target = Targets[i];

                target.value = value;
                if (OnUpdate != null)
                    OnUpdate(value);

                target.Update();
            }
        }

        public static double operator +(Double a, double b)
        {
            return a.value + b;
        }

        public static double operator -(Double a, double b)
        {
            return a.value - b;
        }

        public static double operator *(Double a, double b)
        {
            return a.value * b;
        }

        public static double operator /(Double a, double b)
        {
            return a.value / b;
        }

        public static double operator %(Double a, double b)
        {
            return a.value %= b;
        }

        public static implicit operator double(Double d)
        {
            return d.value;
        }

        public static implicit operator float(Double d)
        {
            return (float)d.value;
        }
    }
}
