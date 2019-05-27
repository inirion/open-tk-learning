using System;
using System.Collections.Generic;
using System.Linq;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;

namespace OpenTKLearning
{
    public class Game : GameWindow
    {
        #region Fields

        private readonly IEnumerable<float> _verticles = new[]
        {
            -0.5f, -0.5f, 0.0f,
            0.5f, -0, 5f, 0.0f,
            0.0f, 0.5f, 0.0f
        };

        private readonly Shader _shader = new Shader();

        #endregion

        #region Properties

        private int VertexBufferObject { get; set; }

        #endregion

        #region Constructors

        public Game(int width, int height, string title) : base(width, height, GraphicsMode.Default, title)
        {
        }

        #endregion

        #region Protected Methods

        protected override void OnLoad(EventArgs e)
        {
            GL.ClearColor(0.2f, 0.2f, 0.5f, 1.0f);
            VertexBufferObject = GL.GenBuffer();

            GL.BufferData(BufferTarget.ArrayBuffer, _verticles.Count() * sizeof(float), _verticles.ToArray(), BufferUsageHint.StaticDraw);

            _shader.Generate("shader.vert", "shader.frag");

            base.OnLoad(e);
        }

        protected override void OnUnload(EventArgs e)
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.DeleteBuffer(VertexBufferObject);

            _shader.Dispose();

            base.OnUnload(e);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            var input = Keyboard.GetState();

            if (input.IsKeyDown(Key.Escape))
            {
                Exit();
            }

            base.OnUpdateFrame(e);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);

            Context.SwapBuffers();

            base.OnRenderFrame(e);
        }

        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);

            base.OnResize(e);
        }

        #endregion
    }
}