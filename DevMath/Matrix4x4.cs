using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevMath
{
    public class Matrix4x4
    {
        public float[][] m = new float[4][] { new float[4], new float[4], new float[4], new float[4] };

        public Matrix4x4()
        {

        }

        public static Matrix4x4 Identity
        {
            get
            {
                Matrix4x4 mat = new Matrix4x4();

                for (int i = 0; i < 4; i++)
                {
                    mat.m[i][i] = 1;
                }

                return mat;
            }
        }

        public float Determinant
        {
            get
            {
                float a = m[0][0], b = m[0][1], c = m[0][2], d = m[0][3];
                float e = m[1][0], f = m[1][1], g = m[1][2], h = m[1][3];
                float i = m[2][0], j = m[2][1], k = m[2][2], l = m[2][3];
                float mm = m[3][0], n = m[3][1], o = m[3][2], p = m[3][3];

                float ax = a * (f * k * p + g * l * n + h * j * o - h * k * n - g * j * p - f * l * o);
                float bx = e * (b * k * p + c * l * n + d * j * o - d * k * n - c * j * p - b * l * o);
                float cx = i * (b * g * p + c * h * n + d * f * o - d * g * n - c * f * p - b * h * o);
                float dx = mm * (b * g * l + c * h * j + d * f * k - d * g * j - c * f * l - b * h * k);

                return ax - bx + cx - dx;
            }
        }

        public Matrix4x4 Inverse
        {
            get
            {
                float a = m[0][0], b = m[0][1], c = m[0][2], d = m[0][3];
                float e = m[1][0], f = m[1][1], g = m[1][2], h = m[1][3];
                float i = m[2][0], j = m[2][1], k = m[2][2], l = m[2][3];
                float mm = m[3][0], n = m[3][1], o = m[3][2], p = m[3][3];

                if (Math.Abs(Determinant) < 0.0001)
                {
                    throw new Exception("Matrix has no inverse!");
                }

                float det = 1 / Determinant;

                Matrix4x4 mat = new Matrix4x4();

                //brainfuck is er niets bij
                mat.m[0][0] = det * (f * k * p + g * l * n + h * j * o - h * k * n - g * j * p - f * l * o);
                mat.m[0][1] = -det * (e * k * p + g * l * mm + h * i * o - h * k * mm - g * i * p - e * l * o);
                mat.m[0][2] = det * (e * j * p + f * l * mm + h * i * n - h * j * mm - f * i * p - e * l * n);
                mat.m[0][3] = -det * (e * j * o + f * k * mm + g * i * n - g * j * mm - f * i * o - e * k * n);

                mat.m[1][0] = det * (b * k * p + b * l * n + d * j * o - d * k * n - c * j * p - b * l * o);
                mat.m[1][1] = -det * (a * k * p + c * l * mm + d * i * o - d * k * mm - c * i * p - a * l * o);
                mat.m[1][2] = det * (a * j * p + b * l * mm + d * i * n - d * j * mm - b * i * p - a * l * n);
                mat.m[1][3] = -det * (a * j * o + b * k * mm + c * i * n - c * j * mm - b * i * o - a * k * n);

                mat.m[2][0] = det * (b * g * p + c * h * n + d * f * o - d * g * n - c * f * p - b * h * o);
                mat.m[2][1] = -det * (a * g * p + c * h * mm + d * e * o - d * g * mm - c * e * p - a * h * o);
                mat.m[2][2] = det * (a * f * p + b * h * mm + d * e * n - d * f * mm - b * e * p - a * h * n);
                mat.m[2][3] = -det * (a * f * o + b * g * mm + c * e * n - c * f * mm - b * e * o - a * g * n);

                mat.m[3][0] = det * (b * g * l + c * h * j + d * f * k - d * g * j - c * f * l - b * h * k);
                mat.m[3][1] = -det * (a * g * l + c * h * i + d * e * k - d * g * i - c * e * l - a * h * k);
                mat.m[3][2] = det * (a * f * l + b * h * i + d * e * j - d * f * i - b * e * l - a * h * j);
                mat.m[3][3] = -det * (a * f * k + b * g * i + c * e * j - c * f * i - b * e * k - a * g * j);

                return mat;
            }
        }

        public static Matrix4x4 Translate(Vector3 translation)
        {
            Matrix4x4 mat = Identity;

            mat.m[0][3] = translation.x;
            mat.m[1][3] = translation.y;
            mat.m[2][3] = translation.z;

            return mat;
        }

        public static Matrix4x4 Rotate(Vector3 rotation)
        {
            //klopt dit wel?
            Matrix4x4 mat = RotateX(rotation.x) * RotateY(rotation.y) * RotateZ(rotation.z);

            return mat;
        }

        public static Matrix4x4 RotateX(float rotation)
        {
            Matrix4x4 mat = new Matrix4x4();

            float cos = (float)Math.Cos(DevMath.DegToRad(rotation));
            float sin = (float)Math.Sin(DevMath.DegToRad(rotation));

            mat.m[0][0] = 1;
            mat.m[1][1] = cos;
            mat.m[1][2] = sin;
            mat.m[2][1] = -sin;
            mat.m[2][2] = cos;
            mat.m[3][3] = 1;

            return mat;
        }

        public static Matrix4x4 RotateY(float rotation)
        {
            Matrix4x4 mat = new Matrix4x4();

            float cos = (float)Math.Cos(DevMath.DegToRad(rotation));
            float sin = (float)Math.Sin(DevMath.DegToRad(rotation));

            mat.m[0][0] = cos;
            mat.m[0][2] = sin;
            mat.m[1][1] = 1;
            mat.m[2][0] = -sin;
            mat.m[2][2] = cos;
            mat.m[3][3] = 1;

            return mat;
        }

        public static Matrix4x4 RotateZ(float rotation)
        {
            Matrix4x4 mat = new Matrix4x4();

            float cos = (float)Math.Cos(DevMath.DegToRad(rotation));
            float sin = (float)Math.Sin(DevMath.DegToRad(rotation));

            mat.m[0][0] = cos;
            mat.m[0][1] = -sin;
            mat.m[1][0] = sin;
            mat.m[1][1] = cos;
            mat.m[2][2] = 1;
            mat.m[3][3] = 1;

            return mat;
        }

        public static Matrix4x4 Scale(Vector3 scale)
        {
            Matrix4x4 mat = new Matrix4x4();

            mat.m[0][0] = scale.x;
            mat.m[1][1] = scale.y;
            mat.m[2][2] = scale.z;
            mat.m[3][3] = 1;

            return mat;
        }

        public static Matrix4x4 operator *(Matrix4x4 lhs, Matrix4x4 rhs)
        {
            Matrix4x4 mat = new Matrix4x4();

            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    mat.m[y][x] = lhs.m[y][0] * rhs.m[0][x] + lhs.m[y][1] * rhs.m[1][x] + lhs.m[y][2] * rhs.m[2][x] + lhs.m[y][3] * rhs.m[3][x];
                }
            }

            return mat;
        }

        public static Vector4 operator *(Matrix4x4 lhs, Vector4 rhs)
        {
            Vector4 vec = new Vector4();

            vec.x = lhs.m[0][0] * rhs.x + lhs.m[0][1] * rhs.y + lhs.m[0][2] * rhs.z + lhs.m[0][3] * rhs.w;
            vec.y = lhs.m[1][0] * rhs.x + lhs.m[1][1] * rhs.y + lhs.m[1][2] * rhs.z + lhs.m[1][3] * rhs.w;
            vec.z = lhs.m[2][0] * rhs.x + lhs.m[2][1] * rhs.y + lhs.m[2][2] * rhs.z + lhs.m[2][3] * rhs.w;
            vec.w = lhs.m[3][0] * rhs.x + lhs.m[3][1] * rhs.y + lhs.m[3][2] * rhs.z + lhs.m[3][3] * rhs.w;


            return vec;
            /*
            for (int y = 0; y < 4; y++)
            {
                mat.m[y][0] *= rhs.x;
                mat.m[y][1] *= rhs.y;
                mat.m[y][2] *= rhs.z;
                mat.m[y][3] *= rhs.w;
            }

            for (int x = 0; x < 4; x++)
            {
                vec.x += mat.m[0][x];
                vec.y += mat.m[1][x];
                vec.z += mat.m[2][x];
                vec.w += mat.m[3][x];
            }*/
            
            return vec;
        }
    }
}
