using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_MVVM_Crud.Model
{
    public class Funcionario
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string SobreNome { get; set; }

        public DateTime DataNascimento { get; set; }

        public Sexo Sexo { get; set; }

        public EstadoCivil EstadoCivil { get; set; }

        public DateTime DataAdmissao { get; set; }

        internal Funcionario Clone()
        {
            throw new NotImplementedException();
        }
    }
}
