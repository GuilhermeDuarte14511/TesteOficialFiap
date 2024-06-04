using Swashbuckle.AspNetCore.Filters;
using Entities;

namespace TesteTecnicoFIAP.Web
{
    public class AlunoTurmaExample : IExamplesProvider<AlunoTurmaDto>
    {
        public AlunoTurmaDto GetExamples()
        {
            return new AlunoTurmaDto
            {
                AlunoId = 1,
                TurmaId = 1
            };
        }
    }
}
