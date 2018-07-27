using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf_MVVM_Crud.Model;

namespace Wpf_MVVM_Crud.ViewModel
{
    public class FuncionariosViewModel : BaseNotifyPropertyChanged
    {
        public ObservableCollection<Funcionario> Funcionarios { get; set; }

        private Funcionario _funcionarioSelecionado;
        public Funcionario FuncionarioSelecionado
        {
            get { return _funcionarioSelecionado; }
            set
            {
                SetField(ref _funcionarioSelecionado, value);
                Deletar.RaiseCanExecuteChanged();
                Editar.RaiseCanExecuteChanged();
            }
        }

        public FuncionariosViewModel()
        {
            Funcionarios = new ObservableCollection<Funcionario>();
            Funcionarios.Add(new Funcionario()
            {
                Id = 1,
                Nome = "André",
                SobreNome = "Lima",
                DataNascimento = new DateTime(1984, 12, 31),
                Sexo = Sexo.Masculino,
                EstadoCivil = EstadoCivil.Casado,
                DataAdmissao = new DateTime(2010, 1, 1)
            });

            FuncionarioSelecionado = Funcionarios.FirstOrDefault();
        }

        #region Commands
        #region Deletar
        //public DeletarCommand Deletar { get; private set; } = new DeletarCommand();
        public DeletarCommand Deletar { get; private set; }

        public class DeletarCommand : BaseCommand 
        {
            public override bool CanExecute(object parameter) 
            {
                var viewModel = parameter as FuncionariosViewModel;
                return viewModel != null && viewModel.FuncionarioSelecionado != null;
            }

            public override void Execute(object parameter)
            {
                var viewModel = (FuncionariosViewModel)parameter;
                viewModel.Funcionarios.Remove(viewModel.FuncionarioSelecionado);
                viewModel.FuncionarioSelecionado = viewModel.Funcionarios.FirstOrDefault();
            }
        }
        #endregion

        #region Novo
        //public NovoCommand Novo { get; private set; } = new NovoCommand();
        public NovoCommand Novo { get; private set; }
        public class NovoCommand : BaseCommand
        {
            public override bool CanExecute(object parameter)
            {
                return parameter is FuncionariosViewModel;
            }

            public override void Execute(object parameter)
            {
                var viewModel = (FuncionariosViewModel)parameter;
                var funcionario = new Funcionario();
                var maxId = 0;
                if(viewModel.Funcionarios.Any())
                {
                    maxId = viewModel.Funcionarios.Max(f => f.Id);
                }
                funcionario.Id = maxId + 1;

                var fw = new FuncionarioWindow();
                fw.DataContext = funcionario;
                fw.ShowDialog();

                if(fw.DialogResult.HasValue && fw.DialogResult.Value)
                {
                    viewModel.Funcionarios.Add(funcionario);
                    viewModel.FuncionarioSelecionado = funcionario;
                }
            }
        }
        #endregion

        #region Editar
        //public EditarCommand Editar { get; private set; } = new EditarCommand();
        public EditarCommand Editar { get; private set; }
        public class EditarCommand : BaseCommand
        {
            public override bool CanExecute(object parameter)
            {
                var viewModel = parameter as FuncionariosViewModel;
                return viewModel != null && viewModel.FuncionarioSelecionado != null;
            }

            public override void Execute(object parameter)
            {
                var viewModel = (FuncionariosViewModel)parameter;
                var cloneFuncionario = (Funcionario)viewModel.FuncionarioSelecionado.Clone();
                var fw = new FuncionarioWindow();
                fw.DataContext = cloneFuncionario;
                fw.ShowDialog();

                if(fw.DialogResult.HasValue && fw.DialogResult.Value)
                {
                    viewModel.FuncionarioSelecionado.Nome = cloneFuncionario.Nome;
                    viewModel.FuncionarioSelecionado.SobreNome = cloneFuncionario.SobreNome;
                    viewModel.FuncionarioSelecionado.DataNascimento = cloneFuncionario.DataNascimento;
                    viewModel.FuncionarioSelecionado.Sexo = cloneFuncionario.Sexo;
                    viewModel.FuncionarioSelecionado.EstadoCivil = cloneFuncionario.EstadoCivil;
                    viewModel.FuncionarioSelecionado.DataAdmissao = cloneFuncionario.DataAdmissao;
                }
            }
        }
        #endregion

        #endregion
    }
}