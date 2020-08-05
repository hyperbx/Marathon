// Marathon is licensed under the MIT License:
/* 
 * MIT License
 * 
 * Copyright (c) 2020 HyperPolygon64
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

using System.Xml.Linq;
using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;

namespace Marathon.Toolkit.OpenGL
{
    class Shaders
    {
        public static Dictionary<string, int> Program = new Dictionary<string, int>();

        /// <summary>
        /// Compiles a shader.
        /// </summary>
        /// <param name="resource">Shader code.</param>
        /// <param name="shaderType">Shader type.</param>
        public static int CompileShader(string resource, ShaderType shaderType)
        {
            int shaderID = GL.CreateShader(shaderType);

            GL.ShaderSource(shaderID, resource);
            GL.CompileShader(shaderID);

            return shaderID;
        }

        /// <summary>
        /// Compiles and loads all shaders.
        /// </summary>
        public static void Load()
        {
            XDocument xml = XDocument.Parse(Properties.Resources.Shaders);

            foreach (XElement shaderElem in xml.Root.Elements("Shader"))
            {
                string @name     = shaderElem.Value,
                       @vertex   = Properties.Resources.ResourceManager.GetString(shaderElem.Attribute("Vertex").Value),
                       @fragment = Properties.Resources.ResourceManager.GetString(shaderElem.Attribute("Fragment").Value);
                       
                int glProgram = GL.CreateProgram();

                int glVertexShader   = CompileShader(@vertex, ShaderType.VertexShader);
                int glFragmentShader = CompileShader(@fragment, ShaderType.FragmentShader);

                GL.AttachShader(glProgram, glVertexShader);
                GL.AttachShader(glProgram, glFragmentShader);

                GL.LinkProgram(glProgram);

                GL.DeleteShader(glVertexShader);
                GL.DeleteShader(glFragmentShader);

                Program.Add(@name, glProgram);
            }
        }
    }
}
