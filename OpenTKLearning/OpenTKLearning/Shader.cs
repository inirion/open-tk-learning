using System;
using System.IO;
using System.Text;
using OpenTK.Graphics.OpenGL4;

namespace OpenTKLearning
{
    public class Shader : IDisposable
    {
        #region Fields

        private bool _disposedValue;

        #endregion

        #region Properties

        public int Handle { get; private set; }

        #endregion

        #region Public Methods

        public void Generate(string vertexPath, string fragmentPath)
        {
            var vertexShader = GL.CreateShader(ShaderType.VertexShader);
            var fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            string vertexShaderSource;
            string fragmentShaderSource;

            using (var reader = new StreamReader(vertexPath, Encoding.UTF8))
                vertexShaderSource = reader.ReadToEnd();

            using (var reader = new StreamReader(fragmentPath, Encoding.UTF8))
                fragmentShaderSource = reader.ReadToEnd();

            GL.ShaderSource(vertexShader, vertexShaderSource);
            GL.ShaderSource(fragmentShader, fragmentShaderSource);

            Handle = GL.CreateProgram();

            GL.AttachShader(Handle, vertexShader);
            GL.AttachShader(Handle, fragmentShader);
            GL.LinkProgram(Handle);
            GL.DetachShader(Handle, vertexShader);
            GL.DetachShader(Handle, fragmentShader);
            GL.DeleteShader(fragmentShader);
            GL.DeleteShader(vertexShader);
        }

        public void Use()
        {
            GL.UseProgram(Handle);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        #region Protected Methods

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                GL.DeleteProgram(Handle);

                _disposedValue = true;
            }
        }

        #endregion

        ~Shader()
        {
            GL.DeleteProgram(Handle);
        }
    }
}