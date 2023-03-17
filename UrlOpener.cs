using System.Diagnostics;
using System.IO;
using System.Net;
using System.Web;

namespace TinyUtubeBot;
public class UrlOpener{
    public List<int> pIds = new List<int>();

    public void OpenUrl(string url, int vistasPend, int minutes){
        for (int i = 0; i < vistasPend; i++){
            try{
                Process edgeProcess = new Process();
                ProcessStartInfo psi = new ProcessStartInfo();
                ///Esta version si funciona, hay q hacer un par de ajustes pero si funciona, algunas ventanas no reproducen automaticamente.
                psi.FileName = "chrome.exe";
                //psi.Arguments = $"{url}?autoplay=1 --new-window";
                psi.Arguments = $"{url} --new-window --autoplay-policy=no-user-gesture-required"; // Añadido el argumento para la reproducción automática

                psi.UseShellExecute = true;
                edgeProcess.StartInfo = psi;
                edgeProcess.Start();
                pIds.Add(edgeProcess.Id);
              
                TimeSpan timeout = TimeSpan.FromSeconds(5);
                Thread.Sleep(timeout);

            }catch (System.Exception){
                throw;
            }
        }
        TimeSpan twait = TimeSpan.FromMinutes(minutes);
        Thread.Sleep(twait);
    }
    public void CloseUrls(){
        foreach (var Id in pIds){
            try{
                Process p = Process.GetProcessById(Id);                
                if (!p.HasExited){
                    p.Kill();
                }
                Process[] ProcesosAbiertos = Process.GetProcessesByName ("chrome");
            try{
                foreach (Process ProcesoActual in ProcesosAbiertos){
                    ProcesoActual.Kill();
                }
            }
            catch (Exception){
                throw;
            }
            }catch (ArgumentException){
                // Process with this Id not found
            }
        }
    }
}