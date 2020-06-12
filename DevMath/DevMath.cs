using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevMath
{
    public class DevMath
    {
        public static float Lerp(float a, float b, float t)
        {
            return a + (b - a) * Clamp(t, 0, 1);
        }

        public static float DistanceTraveled(float startVelocity, float acceleration, float time)
        {
            return startVelocity * time + .5f * acceleration * (float)Math.Pow(time, 2);
        }

        public static float Clamp(float value, float min, float max)
        {
            return value < min ? min : value > max ? max : value;
        }

        public static float RadToDeg(float angle)
        {
            return angle * 180 / (float)Math.PI;
        }

        public static float DegToRad(float angle)
        {
            return angle * (float)Math.PI / 180;
        }
    }
}
