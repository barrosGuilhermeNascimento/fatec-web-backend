using ApiFatecWeb.Code.Helpers;
using ApiFatecWeb.Core.Entity;
using ApiFatecWeb.Core.Model;
using ApiFatecWeb.Core.Model.Enum;
using ApiFatecWeb.Core.Repository;
using ApiFatecWeb.Core.Repository.Interface;
using ApiFatecWeb.Core.Security;
using ApiFatecWeb.Core.Service.Interface;
using AutoMapper;

namespace ApiFatecWeb.Core.Service
{
    public class UserService : IUserService
    {
        private readonly ILogHandler _logHandler;
        private readonly IMapper _mapper;

        private readonly IUserRepository _userRepository;
        private readonly IUserPassRecoverRepository _passRepository;

        private readonly IEmailService _emailService;
        public UserService(
            ILogHandler logHandler, IMapper mapper,
            IUserRepository userRepository, IUserPassRecoverRepository passRepository,
            IEmailService emailService)
        {
            _logHandler = logHandler;
            _mapper = mapper;

            _userRepository = userRepository;
            _passRepository = passRepository;

            _emailService = emailService;
        }

        public async Task<UserModel> GetOneByEmailAsync(string email)
        {
            var entity = await _userRepository.GetOneByEmailAsync(email); ;
            var model = _mapper.Map<UserModel>(entity);
            model.Role = Enum.GetName(typeof(RoleEnum), model.IdRole);
            return model;
        }
        public async Task<List<UserModel>> ListAsync(int idRole = 0)
        {
            var entity = (await _userRepository.ListAsync()).ToList();
            var modelList = new List<UserModel>();

            foreach (var item in entity)
            {
                if (idRole != 0 && item?.IdRole != idRole)
                {
                    continue;
                }
                var model = _mapper.Map<UserModel>(item);
                model.Password = "";
                model.Role = Enum.GetName(typeof(RoleEnum), model.IdRole);
                modelList.Add(model);
            }

            return modelList;
        }


        public async Task<UserModel?> Login(UserLoginModel login)
        {
            var user = await _userRepository.GetOneByEmailAsync(login.Email);

            if (user == null)
            {
                return null;
            }
            var model = user.Password == BasicSecurity.HashPassword(login.Password) ? _mapper.Map<UserModel>(user) : null;
            if (model == null)
                return null;
            model.Role = Enum.GetName(typeof(RoleEnum), model.IdRole);

            return model;
        }

        public async Task<UserModel> Register(UserRegisterModel register)
        {
            try
            {
                if (register.Password.Length < 6)
                {
                    throw new Exception("Senha deve conter no mínimo 6 digitos");
                }

                var entity = _mapper.Map<UserEntity>(register);
                entity.Identifier = Guid.NewGuid();
                entity.Password = BasicSecurity.HashPassword(entity.Password);
                entity.DtUpdate = DateTime.Now;
                await _userRepository.Save(entity);
                var user = await _userRepository.GetOneByEmailAsync(entity.Email);
                return _mapper.Map<UserModel>(user);
            }
            catch (Exception ex)
            {
                _logHandler.SaveLog("UserService/Register", ex.Message, 0);
                throw ex;
            }

        }

        public async Task<bool> RecoverPassword(UserModel user)
        {
            try
            {
                var rnd = new Random();
                var recoverNumber = rnd.Next(10000, 99999);

                await _passRepository.SaveRecover(user.IdUser, recoverNumber);

                var email = new EmailModel()
                {
                    Title = "Recuperação de senha",
                    Body = $"Utilize o código {recoverNumber} Para recuperar sua senha"
                };
                return await _emailService.SendEmail(user.Email, email);
            }
            catch (Exception ex)
            {
                _logHandler.SaveLog("UserService/RecoverPassword", ex.Message, user.IdUser);
                return false;
            }
        }

        public async Task ChangePassword(UserChangePasswordModel model)
        {

            var user = await _userRepository.GetOneByEmailAsync(model.Email);
            if (user == null)
            {
                throw new Exception("Usuário não encontrado");
            }

            if (model.Password != model.ConfirmPassword)
            {
                throw new Exception("Senha e confirmação se diferem");
            }

            var recoverCode = await _passRepository.CodeExists(user.IdUser);
            if (recoverCode != model.RecoverToken)
            {
                throw new Exception("Código Invalido ou expirado");
            }

            user.Password = BasicSecurity.HashPassword(model.Password);

            if (!await _userRepository.Update(user))
            {
                throw new Exception("Erro ao atualizar usuário, tente novamente mais tarde");
            }
        }
    }
}
