using System.Windows.Forms;

static class Program
{
    static void Main(string[] args)
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new BazuziTetris.Form1());
    }
}
