using System;

namespace MauiApp1.Sevices;

public interface IAccountRepository
{
    Task<string> UpdateAccountStatus(string otNom, int status, string ot_uid);
    Task<int> GetAccountStatus(string userName);
}
