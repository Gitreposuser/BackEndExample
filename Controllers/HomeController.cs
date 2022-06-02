using Data.Pagination;
using Domain.Contracts.Filters;
using Domain.Contracts.Filters.Models;
using Domain.Entities;
using Domain.Repositories;
using Host.Models;
using Microsoft.AspNetCore.Mvc;

namespace Host.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMangaRepository _repository;
        private readonly IEntityFilter<Manga, IMangaFilterParameters> _filterBuilder;

        public HomeController(
            ILogger<HomeController> logger,
            IMangaRepository repository,
            IEntityFilter<Manga, IMangaFilterParameters> filterBuilder)
        {
            _logger = logger;
            _repository = repository;
            _filterBuilder = filterBuilder;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Manga>>> GetAll([FromQuery]MangaFilterModel filterParameters, 
            [FromQuery] PaginationModel pagination)
        {
            _logger.LogInformation("Get all manga");
            var filterExpression = _filterBuilder.BuildExpression(filterParameters);
            var itemsCount = await _repository.CountAsync(filterExpression);
            if (itemsCount == 0)
            { 
                return NoContent();
            }

            var items = (await _repository.FindAsync(filterExpression, pagination)).ToList();
            var pagedList = new PagedList<Manga>(items, itemsCount, pagination.PageNumber, pagination.PageSize);
            Response?.Headers.Add("X-Pagination", pagedList.GenerateMetadata());
            Response?.Headers.Add("Access-Control-Expose-Headers", "Content-Encoding, X-Pagination");
            return Ok(pagedList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Manga>>> Get(string id)
        {
            _logger.LogInformation($"Get manga with id '{id}'");
            try
            {
                return Ok(await _repository.GetAsync(id));
            }
            catch
            {
                return BadRequest("Something went wrong");
            }
        }
    }
}