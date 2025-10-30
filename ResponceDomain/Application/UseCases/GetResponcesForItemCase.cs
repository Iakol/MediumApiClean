using Microsoft.AspNetCore.Http.HttpResults;
using ResponceDomain.Application.DTO;
using ResponceDomain.Application.Interfaces;
using ResponceDomain.Application.Services;
using ResponceDomain.Domain;
using ResponceDomain.Presentation.UseCases;
using System;
using System.Diagnostics;
using System.Linq;

namespace ResponceDomain.Application.UseCases
{
    public class GetResponcesForItemCase : IGetResponcesForItemCase
    {
        private readonly IResponceRepository _responceRepository;
        private readonly IClapsToResponceOfUsersIterfaces _ResponceClapsOfUsersIterfaces;
        private readonly CreateResponceTreeByResponceTreeBuilder _ResponceTreeBuilder;

        public GetResponcesForItemCase(IResponceRepository responceRepository, IClapsToResponceOfUsersIterfaces responceClapsOfUsersIterfaces, CreateResponceTreeByResponceTreeBuilder responceTreeBuilder)
        {
            _responceRepository = responceRepository;
            _ResponceClapsOfUsersIterfaces = responceClapsOfUsersIterfaces;
            _ResponceTreeBuilder = responceTreeBuilder;
        }

        public async Task<Result<List<ResponceDTO>>> Handle(string itemId)
        {
            if (string.IsNullOrWhiteSpace(itemId))
            {
                return Result<List<ResponceDTO>>.Failure("Item Id is Null");
            }

            try
            {
                //Stopwatch stopwatch = new Stopwatch();
                //stopwatch.Start();

                HashSet<Responce> responces = (await _responceRepository.GetAllResponcesByItem(itemId)).ToHashSet();

                Dictionary<int, int> AllResponceClapsBucket = await _ResponceClapsOfUsersIterfaces.getClapsCountToResponceOfUsersByRespocnceList(responces.Select(r => r.ResponceId).ToList());


                List<ResponceDTO> responceDTOs = _ResponceTreeBuilder.BuildTree(responces, AllResponceClapsBucket).ToList();
                //stopwatch.Stop();
                //TimeSpan ts = stopwatch.Elapsed;
                //Console.WriteLine(ts);
                return Result<List<ResponceDTO>>.Success(responceDTOs);
            }
            catch (Exception ex)
            {
                return Result<List<ResponceDTO>>.Failure(ex.Message);
            }
        }


    }
}
