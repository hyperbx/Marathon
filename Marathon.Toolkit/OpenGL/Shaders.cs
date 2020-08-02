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
