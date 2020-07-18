// Vector.cs is licensed under the MIT License:
/* 
 * MIT License
 * 
 * Copyright (c) 2018 Radfordhound
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

using System;

namespace Marathon.IO
{
    [Serializable]
    public struct Vector2
    {
        // Variables/Constants
        public float X, Y;

        // Constructors
        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }

        // Methods
        public override string ToString()
        {
            return $"({X}, {Y})";
        }

        public static Vector2 operator +(Vector2 v1, Vector2 v2) =>
            new Vector2(v1.X + v2.X, v1.Y + v2.Y);

        public static Vector2 operator -(Vector2 v1, Vector2 v2) =>
            new Vector2(v1.X - v2.X, v1.Y - v2.Y);

        public static Vector2 operator *(Vector2 v1, Vector2 v2) =>
            new Vector2(v1.X * v2.X, v1.Y * v2.Y);

        public static Vector2 operator *(Vector2 v1, float m) =>
            new Vector2(v1.X * m, v1.Y * m);

        public static Vector2 operator /(Vector2 v1, Vector2 v2) =>
            new Vector2(v1.X / v2.X, v1.Y / v2.Y);

        public static Vector2 operator /(Vector2 v1, float m) =>
            new Vector2(v1.X / m, v1.Y / m);
    }

    // All Vector3s in HedgeLib should use these axes:
    // X-left, Y-up, Z-forward
    // This follows the axes used in Sonic Generations.
    [Serializable]
    public struct Vector3
    {
        // Variables/Constants
        public float X, Y, Z;

        // Constructors
        public Vector3(float x, float y)
        {
            X = x;
            Y = y;
            Z = 0;
        }

        public Vector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        // Methods
        public override string ToString()
        {
            return $"({X}, {Y}, {Z})";
        }

        public static Vector3 operator +(Vector3 v1, Vector3 v2) =>
            new Vector3(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);

        public static Vector3 operator -(Vector3 v1, Vector3 v2) =>
            new Vector3(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);

        public static Vector3 operator *(Vector3 v1, Vector3 v2) =>
            new Vector3(v1.X * v2.X, v1.Y * v2.Y, v1.Z * v2.Z);

        public static Vector3 operator *(Vector3 v1, float m) =>
            new Vector3(v1.X * m, v1.Y * m, v1.Z * m);

        public static Vector3 operator /(Vector3 v1, Vector3 v2) =>
            new Vector3(v1.X / v2.X, v1.Y / v2.Y, v1.Z / v2.Z);

        public static Vector3 operator /(Vector3 v1, float m) =>
            new Vector3(v1.X / m, v1.Y / m, v1.Z / m);
    }

    [Serializable]
    public class Vector4
    {
        // Variables/Constants
        public float X = 0, Y = 0, Z = 0, W = 0;

        // Constructors
        public Vector4() { }
        public Vector4(float x, float y)
        {
            X = x;
            Y = y;
            Z = 0;
            W = 0;
        }

        public Vector4(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
            W = 0;
        }

        public Vector4(float x, float y, float z, float w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        // Methods
        public override string ToString()
        {
            return $"({X}, {Y}, {Z}, {W})";
        }
    }
}