using Microsoft.AspNetCore.Mvc;
using StarboxLibraryNet8EF.Services;
using StarboxLibraryNet8EF.Dtos;

namespace StarboxNet8CoreApiEF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DrinksController : ControllerBase
    {
        private readonly IDrinkService _drinkService;

        public DrinksController(IDrinkService drinkService)
        {
            _drinkService = drinkService;
        }

        // GET: api/<DrinksController>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<DrinkDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<DrinkDto>>> GetAllDrinks()
        {           
            var drinks = await _drinkService.GetDrinksAsync();
            return Ok(drinks);
        }

        // GET api/<DrinksController>/5
        [HttpGet("{id}")]       
        [ProducesResponseType(typeof(DrinkDto), StatusCodes.Status200OK)]       
        public async Task<ActionResult<DrinkDto>> GetAvailableDrink(int id)
        {
            var drink = await _drinkService.GetAvailableDrinkAsync(id);
            return Ok(drink);
        }

        // POST api/<DrinksController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<DrinkDto>> CreateDrink([FromBody] DrinkDto drinkDto)
        {
            if (!drinkDto.Ingredients.Any())
            {
                return BadRequest("ingredients are missing");
            }

            DrinkDto createdDto = await _drinkService.AddDrinkAsync(drinkDto);
            return CreatedAtAction(nameof(GetAvailableDrink), new { id = createdDto.Id }, createdDto); 
        }

        // PUT api/<DrinksController>/5
        [HttpPut("{id}")]       
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateDrink(int id, [FromBody] DrinkDto drinkDto)
        {            
            var drink = await _drinkService.GetDrinkAsync(id); 

            if (drink == null || drink.Id == 0)
            {
                return NotFound();
            }

            if (id != drinkDto.Id)
            {
                return BadRequest("id does not match drinkDto.Id");
            }

            await _drinkService.UpdateDrinkAsync(drinkDto);
            return NoContent();
        }     

        // DELETE api/<DrinksController>/5
        [HttpDelete("{id}")]      
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteDrink(int id)
        {  
            await _drinkService.DeleteDrinkAsync(id);
            return NoContent();
        }
    }
}
