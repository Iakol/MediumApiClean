using ResponceDomain.Application.DTO;
using ResponceDomain.Application.Interfaces;
using ResponceDomain.Application.UnitsOfWorks;
using ResponceDomain.Domain;
using ResponceDomain.Infrastructure.DataBase.DBContex;
using ResponceDomain.Presentation.Comand;
using ResponceDomain.Presentation.UseCases;

namespace ResponceDomain.Application.UseCases
{
    public class AddClapsToResponceCase : IAddClapsToResponceCase
    {
        private readonly IAddClapsToResponceUnit _addClapsToResponceUnit;
        private readonly IUpdateClapsToResponceUnit _updateClapsToResponceUnit;
        private readonly IClapsToResponceOfUsersIterfaces _clapsToResponceOfUsersIterfaces;
        private readonly IResponceRepository _responceRepository;

        public AddClapsToResponceCase(IAddClapsToResponceUnit addClapsToResponceUnit,
            IUpdateClapsToResponceUnit updateClapsToResponceUnit,
            IClapsToResponceOfUsersIterfaces clapsToResponceOfUsersIterfaces,
            IResponceRepository responceRepository)
        {
            _addClapsToResponceUnit = addClapsToResponceUnit;
            _updateClapsToResponceUnit = updateClapsToResponceUnit;
            _clapsToResponceOfUsersIterfaces = clapsToResponceOfUsersIterfaces;
            _responceRepository = responceRepository;
        }

        public async Task<Result<int>> Handle(AddClapsToResponceCommandData addClapsData, string userId)
        {
            Responce responce = await _responceRepository.GetResponceById(addClapsData.responceId);
            if (responce.UserId.Equals(userId)) 
            {
                return Result<int>.Failure("It`s users responce");
            }

            try
            {
                ClapsToResponceOfUsers usersClaps = await _clapsToResponceOfUsersIterfaces.getClapsToResponceOfUsers(addClapsData.responceId, userId);

                if (usersClaps == null)
                {
                    usersClaps = new ClapsToResponceOfUsers(addClapsData.responceId, userId, addClapsData.countOfClaps);
                    await _addClapsToResponceUnit.AddClaps(usersClaps);
                    return Result<int>.Success(addClapsData.countOfClaps);
                }
                else
                {
                    usersClaps.ClapsCount += addClapsData.countOfClaps;
                    await _updateClapsToResponceUnit.UpdateClaps(usersClaps);
                    return Result<int>.Success(usersClaps.ClapsCount);
                }

            }
            catch (Exception ex) 
            {
                return Result<int>.Failure(ex.Message);
            }
        }
    }
}
