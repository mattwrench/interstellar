using System;

namespace Interstellar
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new Interstellar())
                game.Run();
        }
    }
}
