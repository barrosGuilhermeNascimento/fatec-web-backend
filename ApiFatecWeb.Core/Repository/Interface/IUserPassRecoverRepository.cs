namespace ApiFatecWeb.Core.Repository.Interface;

public interface IUserPassRecoverRepository
{
    Task SaveRecover(int idUser, int number);
    Task<int> CodeExists(int idUser);
}