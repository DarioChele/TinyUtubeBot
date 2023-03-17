using Logger;
using Microsoft.Extensions.DependencyInjection;

namespace TinyUtubeBot;
public class Program{
    static void Main(string[] args){
    int vistas = 0; int vueltaActual = 0; int numVueltas = 1;

        Console.WriteLine("Ingrese la Url: ");
        string url = Console.ReadLine().Trim();

        Console.WriteLine("Ingrese total de vistas: ");
        int VistasPedidas = int.Parse(Console.ReadLine().Trim());

        Console.WriteLine("Ingrese tiempo de reproduccion en minutos: ");
        //int minutes = int.Parse(Console.ReadLine().Trim() );        
        int minutes =  int.Parse(Console.ReadLine().Trim() );
        var serviceProvider = new ServiceCollection()
            .AddTransient<ILogger>(provider => new FileLogger())
            .BuildServiceProvider();
        
        var logger = serviceProvider.GetService<ILogger>();
        
        try{
            if (VistasPedidas > 5){
                numVueltas = VistasPedidas / 5;
            }
            while (vistas < VistasPedidas){
                UrlOpener opener = new UrlOpener();
                int vistasPend = VistasPedidas - (vueltaActual * 5);
                if (vistasPend < 5){
                    opener.OpenUrl(url, vistasPend, minutes);
                    vistas = vistas + vistasPend;
                }else{
                    opener.OpenUrl(url, 5, minutes);
                    vistas = vistas + 5;
                }
                opener.CloseUrls();
                vueltaActual++;
            }
        }catch (Exception ex){
            Console.WriteLine($"Ha ocurrido un error: {ex.Message}");
            logger.Log(ex.Message);
        }


    }
}