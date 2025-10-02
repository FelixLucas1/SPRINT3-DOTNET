using Microsoft.AspNetCore.Mvc;
using Sprint03.DTO;

namespace Sprint03.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidosController : ControllerBase
    {
        private readonly Sprint03.Service.PedidoService _service;
        public PedidosController(Sprint03.Service.PedidoService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber = 1, int pageSize = 10)
        {
            // Validar parâmetros
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1 || pageSize > 100) pageSize = 10; // Limite máximo

            var (pedidos, totalCount, totalPages) = await _service.GetPaginatedAsync(pageNumber, pageSize);

            // Criar links HATEOAS
            var links = new List<object>
    {
        new { rel = "self", href = Url.Action(nameof(GetAll), new { pageNumber, pageSize }), method = "GET" }
    };

            if (pageNumber > 1)
                links.Add(new { rel = "prev", href = Url.Action(nameof(GetAll), new { pageNumber = pageNumber - 1, pageSize }), method = "GET" });

            if (pageNumber < totalPages)
                links.Add(new { rel = "next", href = Url.Action(nameof(GetAll), new { pageNumber = pageNumber + 1, pageSize }), method = "GET" });

            links.Add(new { rel = "first", href = Url.Action(nameof(GetAll), new { pageNumber = 1, pageSize }), method = "GET" });
            links.Add(new { rel = "last", href = Url.Action(nameof(GetAll), new { pageNumber = totalPages, pageSize }), method = "GET" });

            return Ok(new
            {
                Data = pedidos,
                Pagination = new
                {
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalPages = totalPages,
                    TotalCount = totalCount,
                    HasPrevious = pageNumber > 1,
                    HasNext = pageNumber < totalPages
                },
                Links = links
            });
        }

        [HttpGet("{id:int}", Name = "GetPedido")]
        public async Task<IActionResult> Get(int id)
        {
            var p = await _service.GetByIdAsync(id);
            if (p == null) return NotFound();
            return Ok(p);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PedidoDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var (Success, Error, Pedido) = await _service.CreateAsync(dto);
            if (!Success) return BadRequest(new { error = Error });

            return CreatedAtRoute("GetPedido", new { id = Pedido!.Id }, Pedido);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _service.DeleteAsync(id);
            return ok ? NoContent() : NotFound();
        }
    }
}
