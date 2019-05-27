namespace OpenTKLearning
{
    class Program
    {
        #region Private Methods

        static void Main(string[] args)
        {
            using (var game = new Game(800, 600, "Game"))
            {
                game.Run(60.0);
            }
        }

        #endregion
    }
}