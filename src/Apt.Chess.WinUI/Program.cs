using Apt.Chess.Core.Services;
using Apt.Chess.Core.Services.Standard;

namespace Apt.Chess.WinUI;

internal static class Program
{
   /// <summary>
   ///  The main entry point for the application.
   /// </summary>
   [STAThread]
   static void Main()
   {
      // To customize application configuration such as set high DPI settings or default font,
      // see https://aka.ms/applicationconfiguration.
      //ApplicationConfiguration.Initialize();
      //Application.Run(new MainForm());

      var host = Host.CreateDefaultBuilder()
         .ConfigureServices((context, services) =>
         {
            ConfigureServices(context.Configuration, services);
         })
         .Build();

      var services = host.Services;
      var mainForm = services.GetRequiredService<MainForm>();

      Application.Run(mainForm);
   }

   private static void ConfigureServices(IConfiguration configuration, IServiceCollection services)
   {
      services
         .AddSingleton<MainForm>()
         .AddSingleton<IBoardModelFactory, StandardBoardModelFactory>()
         ;
   }
}
