// NinjaMotion.cs is licensed under the MIT License:
/* 
 * MIT License
 * 
 * Copyright (c) 2021 Knuxfan24
 * Copyright (c) 2021 HyperBE32
 * Copyright (c) 2021 Radfordhound
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

using System.Collections.Generic;

namespace Marathon.IO.Formats.Meshes.SegaNN
{
    public class NinjaMotion
    {
        public uint Type { get; set; }

        public float StartFrame { get; set; }

        public float EndFrame { get; set; }

        public List<NinjaSubMotion> SubMotions = new List<NinjaSubMotion>();

        public float Framerate { get; set; }

        public uint Reserved0 { get; set; }

        public uint Reserved1 { get; set; }
    }

    public class NinjaMotionVectorHandle
    {
        public Vector2 In { get; set; }

        public Vector2 Out { get; set; }
    }

    public class NinjaMotionKey
    {
        public float Frame { get; set; }

        public object Value { get; set; }
    }

    public class NinjaMotionVectorHandleKey : NinjaMotionKey
    {
        public NinjaMotionVectorHandle Handle { get; set; }
    }

    public class NinjaMotionUVKey : NinjaMotionKey
    {
        public Vector2 Value { get; set; }
    }

    public class NinjaMotionVectorKey : NinjaMotionKey
    {
        public Vector3 Value { get; set; }
    }

    public class NinjaMotionQuaternionKey : NinjaMotionKey
    {
        public Quaternion Value { get; set; }
    }

    public class NinjaMotionInt32Key : NinjaMotionKey
    {
        public int Value { get; set; }
    }

    public class NinjaMotionHandleInt32Key : NinjaMotionVectorHandleKey
    {
        public int Value { get; set; }
    }

    public class NinjaMotionUInt32Key : NinjaMotionKey
    {
        public uint Value { get; set; }
    }

    public class NinjaMotionRotateInt32Key : NinjaMotionKey
    {
        public NinjaRotateInt32 Value { get; set; }
    }

    public class NinjaMotionInt16Key
    {
        public short Frame { get; set; }

        public short Value { get; set; }
    }

    public class NinjaMotionHandleInt16Key : NinjaMotionInt16Key
    {
        public NinjaMotionVectorHandle Handle { get; set; }
    }

    public class NinjaMotionRotateInt16Key : NinjaMotionKey
    {
        public short Frame { get; set; }

        public NinjaRotateInt16 Value { get; set; }
    }

    public class NinjaSubMotion
    {
        public uint SubMot_Type { get; set; }

        public uint SubMot_IPType { get; set; } // TODO: Find out what IP means in this context.

        public uint SubMot_ID { get; set; }

        public float SubMot_StartFrame { get; set; }

        public float SubMot_EndFrame { get; set; }

        public float SubMot_StartKey { get; set; }

        public float SubMot_EndKey { get; set; }

        public List<NinjaSubMotionKey> SubMot_KeyList = new List<NinjaSubMotionKey>();
    }

    public class NinjaSubMotionKey
    {
        public float Key_Translation_MFRM { get; set; }

        public float Key_Translation_MVAL { get; set; }

        public short Key_Rotation_MFRM { get; set; }

        public Half Key_Rotation_MVAL { get; set; }

        public float Key_Diffuse_MFRM { get; set; }

        public Vector3 Key_Diffuse_MVAL { get; set; }
    }
}
