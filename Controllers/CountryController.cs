using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO.MemoryMappedFiles;
using World_Api.Data;
using World_Api.DTO.Country;
using World_Api.Models;

namespace World_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ApplicationDbContext _dbcontext;
        private readonly IMapper _mapper;

        public object[] Id { get; private set; }

        public CountryController(ApplicationDbContext dbContext,IMapper mapper)
        {
            _dbcontext = dbContext;
            _mapper=mapper;
        }


        [HttpGet]
        public ActionResult<IEnumerable<Country>> GetAll()
        {
            var countries= _dbcontext.countries.ToList();
            _dbcontext.SaveChanges();
            return Ok();
        }



        [HttpGet("{Id:int}")]
        public ActionResult<Country> Get(int Id)
        {
            return _dbcontext.countries.Find(Id);
        }



        [HttpPost]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        //[Route("countries")]

        public ActionResult<CreateCountryDTO> Create([FromBody] CreateCountryDTO countryDto)
        {
            var result1=_dbcontext.countries.AsQueryable().Where(c => c.Name.ToLower().Trim() == countryDto.Name.ToLower().Trim()).Any();
            if (result1)
            {
                return Conflict("Country Already Exsists in Our Data");
            }
            var result2= _dbcontext.countries.AsQueryable().Where(c => c.ShortName.ToLower().Trim()==countryDto.ShortName.ToLower().Trim()).Any();
            if(result2)
            {
                return Conflict("Country Short Name is Already exsists");
            }
            var result3=_dbcontext.countries.AsQueryable().Where(c=> c.MobileCode.ToLower().Trim().Trim()==countryDto.MobileCode.ToLower().ToLower().Trim()).Any();
            if (result3)
            {
                return Conflict("Country MobileCode is Already exsists");
            }
            //Country country = new Country();
            //country.Name = countryDto.Name;
            //country.ShortName = countryDto.ShortName;
            //country.MobileCode = countryDto.MobileCode;

            var country = _mapper.Map<Country>(countryDto);
            


            _dbcontext.countries.Add(country);
            _dbcontext.SaveChanges();
            return Ok();
        }



        [HttpPut("{Id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]



        public ActionResult<Country> Update(int Id,[FromBody] Country country)
        {
            if(country == null || Id != country.Id) 
            {
                return BadRequest();    
            }



            var countryFromDb = _dbcontext.countries.Find(Id);

            if (countryFromDb==null)
            {
                return NotFound();
            }
            countryFromDb.Name = country.Name;
            countryFromDb.ShortName = country.ShortName;
            countryFromDb.MobileCode=country.MobileCode;

            _dbcontext.countries.Update(countryFromDb);   
            _dbcontext.SaveChanges();
            return NoContent();
        }




        [HttpDelete("{Id:int}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult DeleteById(int Id) {

            if(Id == 0)
            {
                return BadRequest();
            }
            var country = _dbcontext.countries.Find(Id);
            if (country == null)
            {
                return NotFound();
            }
            _dbcontext.countries.Remove(country);
            _dbcontext.SaveChanges();
            return NoContent();
        }
    }  
}
