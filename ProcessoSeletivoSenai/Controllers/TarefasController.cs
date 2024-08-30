using Microsoft.AspNetCore.Mvc;

namespace ProcessoSeletivoSenai.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefasController : ControllerBase
    {

        private readonly ILogger<TarefasController> _logger;

        public TarefasController(ILogger<TarefasController> logger)
        {
            _logger = logger;
        }


        /// <summary>
        /// Endpoint de criacao de tarefas.
        /// </summary>
        /// <param name="titulo">String</param>
        /// <param name="descricao">String</param>
        /// <returns>
        /// 400:Validaçao inválida.
        /// 200:Id da tarefa inserida.
        /// </returns>
        [HttpPost]
        public IActionResult Post(string titulo, string? descricao,DateTime? conclusao,Status status = Status.Pendente)
        {
            if (String.IsNullOrEmpty(titulo))
                return BadRequest("O título deve ser preenchido.");


            Guid id = Guid.NewGuid();

            while (Tarefas.tarefas.Exists(t => t.Id.Equals(id)))
                id = Guid.NewGuid();


            Tarefas.tarefas.Add(new Tarefa
            {
                Id = id,
                Titulo = titulo,
                Descricao = descricao,
                DataCriacao = DateTime.Now.Date,
                DataConclusao = conclusao,
                Status = status
            });

            return Ok(id.ToString());
        }

        [HttpGet]
        public IEnumerable<Tarefa> Get()
        {
            return Tarefas.tarefas.ToArray();
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Put(Guid id,string? titulo, string? descricao, DateTime? conclusao, Status? status)
        {
            Tarefa tarefa = Tarefas.tarefas.FirstOrDefault(t => t.Id.Equals(id));

            if (tarefa is null)
                return NotFound("Tarefa não encontrada.");

            if (!String.IsNullOrEmpty(titulo))
                tarefa.Titulo = titulo;

            if (!String.IsNullOrEmpty(descricao))
                tarefa.Descricao = descricao;

            if (conclusao is not null)
                tarefa.DataConclusao = conclusao;

            if (status is not null)
                tarefa.Status = status??0;

            return Ok("Tarefa atualizada.");
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(Guid id)
        {
            var index = Tarefas.tarefas.FindIndex(t => t.Id.Equals(id));

            if (index < 0)
                return NotFound("Tarefa não encontrada.");

            Tarefas.tarefas.RemoveAt(index);

            return Ok("Tarefa removida.");
        }
    }
}
