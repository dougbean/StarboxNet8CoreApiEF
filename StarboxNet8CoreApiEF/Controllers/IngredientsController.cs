using Microsoft.AspNetCore.Mvc;
using StarboxLibraryNet8EF.Services;
using StarboxLibraryNet8EF.Dtos;

namespace StarboxNet8CoreApiEF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientsController : ControllerBase
    {
        private readonly IIngredientService _ingredientService;

        public IngredientsController(IIngredientService ingredientService)
        {
            _ingredientService = ingredientService;
        }

        // GET: api/<IngredientsController>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<IngredientDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<IngredientDto>>> GetIngredients()
        {
            var ingredients = await _ingredientService.GetIngredientsAsync();
            return Ok(ingredients);
        }

        // GET api/<IngredientsController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(IngredientDto), StatusCodes.Status200OK)]      
        public async Task<ActionResult<IngredientDto>> GetIngredient(int id) 
        {
            var ingredient = await _ingredientService.GetIngredientAsync(id);

            if (ingredient == null || ingredient.Id == 0) 
            {
                return NotFound();
            }            

            return Ok(ingredient);
        }        

        // POST api/<IngredientsController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]        
        public async Task<ActionResult<IngredientDto>> Post(IngredientDto ingredientDto)
        {
            IngredientDto createdDto = await _ingredientService.AddIngredientAsync(ingredientDto);
            return CreatedAtAction(nameof(GetIngredient), new { id = createdDto.Id }, createdDto);
        }

        // PUT api/<IngredientsController>/5       
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateIngredient(int id, IngredientDto ingredientDto)
        {
            var ingredient = await _ingredientService.GetIngredientAsync(id);

            if (ingredient == null || ingredient.Id == 0)
            {
                return NotFound();
            }

            if (id != ingredientDto.Id)
            {
                return BadRequest();
            }

            await _ingredientService.UpdateIngredientAsync(ingredientDto);
            return NoContent();
        }       

        // DELETE api/<IngredientsController>/5
        [HttpDelete("{id}")]       
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(int id)
        {
            await _ingredientService.DeleteIngredientAsync(id);
            return NoContent();
        }
               
        [HttpPut("update-amounts")]       
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateIngredientsAmount(int amount) 
        {
            await _ingredientService.UpdateIngredientsAmountAsync(amount);
            return NoContent();
        }
    }
}
