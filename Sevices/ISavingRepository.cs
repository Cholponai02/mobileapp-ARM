using MauiApp1.Models;

namespace MauiApp1.Sevices
{
     public interface ISavingRepository
    {
        Task<string> SaveAbsFile(string clientInn, string ZvPozn, string longitude, string latitude, byte[] photoData, string type, string IdZalog, int OtNom, string token);
        Task<string> SaveNewZalog(int vidZal, string pozn, string otNom);
    }
}
