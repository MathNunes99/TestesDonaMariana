using System;
using System.Windows.Forms;
using TestesDonaMariana_WinApp.Arquivos;
using TestesDonaMariana_WinApp.Arquivos.Compartilhado;

namespace TestesDonaMariana_WinApp
{
    internal static class Program
    {
        static ISerializador serializador = new SerializadorDadosEmJsonEmDotNet();

        static DataContext contexto = new DataContext(serializador);

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new TelaMenu(contexto));

            contexto.GravarDados();
        }
    }
}
