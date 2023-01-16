using ClinicService.Models;
using ClinicService.Services;
using ClinicService.Services.Impl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClinicService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetController : ControllerBase
    {
        private IPetRepository _petRepository;

        public PetController(IPetRepository petRepository)
        {
            _petRepository = petRepository;
        }

        [HttpPost("createPet")]
        public ActionResult<int> Create([FromBody] Models.Requests.CreatePetRequest createPetRequest)
        {
            int res = _petRepository.Create(new Pet
            {
                PetId = createPetRequest.PetId,
                ClientId = createPetRequest.ClientId,
                Name = createPetRequest.Name,
                Birthday = createPetRequest.Birthday,
            });
            return Ok(res);
        }

        [HttpPut("updatePet")]
        public ActionResult<int> Update([FromBody] Models.Requests.UpdatePetRequest petRequest)
        {
            int res = _petRepository.Update(new Pet
            {
                ClientId = petRequest.ClientId,
                PetId = petRequest.PetId,
                Name = petRequest.Name,
                Birthday = petRequest.Birthday,
            });
            return Ok(res);
        }


        [HttpDelete("deletePet")]
        public ActionResult<int> Delete(int PetId)
        {
            int res = _petRepository.Delete(PetId);
            return Ok(res);
        }

        [HttpGet("get-allPets")]
        public ActionResult<List<Pet>> GetAll()
        {
            return Ok(_petRepository.GetAll());
        }

        [HttpGet("get-by-idPet")]
        public ActionResult<Pet> GetById(int PetId)
        {
            return Ok(_petRepository.GetById(PetId));
        }
    }
}
