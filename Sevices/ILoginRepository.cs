using MauiApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Sevices
{
    public interface ILoginRepository
    {
        Task<User> Login(string username, string password);
        Task<string> LoginLog(string login, string fio, string otdel, string uniq, string result);
    }
}
