using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Common;
using DAL.Entity;
using DAL.Interfaces;
using DAL.Repositories;


namespace BLL
{
  public class UserService : IUserService
  {
    private readonly IRepository<User> _repository;
    private readonly IRepositoryUser _repositoryUser;

    public UserService(IRepository<User> repository, IRepositoryUser repositoryUser)
    {
      _repository = repository;
      _repositoryUser = repositoryUser;
    }

    public Task<object> Authenticate(string username, string password)
    {
      object mapper = _repositoryUser.Authenticate(username, password);
      return Task.FromResult(mapper);
    }

    public async Task<bool> Create(UserModel user)
    {
      var mapper = Mapper.Map<User>(user);
      return Mapper.Map<bool>(await _repository.Create(mapper, user.Password));
    }

    public async Task<IEnumerable<UserModel>> GetAllUsers()
    {
      IEnumerable<User> authors = await _repository.GetAll();
      return Mapper.Map<IEnumerable<UserModel>>(authors);
    }

    public async Task<UserModel> GetUserById(int id)
    {
      return Mapper.Map<UserModel>( await _repository.Get(id.ToString()));
    }
  }
}
