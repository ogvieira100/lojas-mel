using buildingBlocksCore.Data.PersistData.Mapping;
using buildingBlocksCore.Data.ReadData.MongoManage.Interfaces;
using buildingBlocksCore.Models;
using buildingBlocksCore.Utils;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace buildingBlocksCore.Data.PersistData.Context
{
    public class ApplicationContext : DbContext
    {

        readonly IClientesMongoManage _clientesMongoManage;
        readonly IEnderecoMongoMange _enderecoMongoMange;
        readonly IProdutoMongoManage _produtoMongoManage;
        readonly IFornecedorMongoManage _fornecedorMongoManage;
        readonly IPedidoMongoManage _pedidoMongoManage;
        readonly INotaMongoManage _notaMongoManage;
        readonly IPedidoItensMongoManage _pedidoItensMongoManage;
        readonly IMediator _mediator;
        public ApplicationContext(DbContextOptions<ApplicationContext> options,
            IClientesMongoManage clientesMongoManage,
            IProdutoMongoManage produtoMongoManage,
            IFornecedorMongoManage fornecedorMongoManage,
            IPedidoMongoManage pedidoMongoManage,
            INotaMongoManage notaMongoManage,
            IMediator mediator,
            IPedidoItensMongoManage pedidoItensMongoManage,
            IEnderecoMongoMange enderecoMongoMange)
         : base(options)
        {
            _produtoMongoManage = produtoMongoManage;
            _clientesMongoManage = clientesMongoManage;
            _enderecoMongoMange = enderecoMongoMange;
            _fornecedorMongoManage = fornecedorMongoManage;
            _pedidoMongoManage = pedidoMongoManage;
            _notaMongoManage = notaMongoManage;
            _mediator = mediator;
            _pedidoItensMongoManage = pedidoItensMongoManage;
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entrys = GetEntrys();
            var ret = await base.SaveChangesAsync(cancellationToken);
            return ret;
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            var entrys = GetEntrys();
            var ret = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
            await SaveChangesMongoAsync(ret, entrys);
            return ret;

        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        List<Tuple<Microsoft.EntityFrameworkCore.EntityState, Type, object>> GetEntrys()
        {
            List<Tuple<Microsoft.EntityFrameworkCore.EntityState, Type, object>> entrys = new List<Tuple<Microsoft.EntityFrameworkCore.EntityState, Type, object>>();
            foreach (var entry in ChangeTracker.Entries())
            {
                var baseEntry = entry.Entity;
                entrys.Add(new Tuple<Microsoft.EntityFrameworkCore.EntityState, Type, object>(entry.State,
                                                                baseEntry.GetType(),
                                                                baseEntry));
            }
            return entrys;
        }

        private async Task SaveChangesMongoAsync(int ret, List<Tuple<Microsoft.EntityFrameworkCore.EntityState, Type, object>> entrys)
        {

            if (ret > 0)
            {
                #region " Pedidos "

                var tuplePedidos = entrys.Where(x => x.Item2 == typeof(Pedido))
                              .Select(x => new Tuple<Microsoft.EntityFrameworkCore.EntityState, Pedido>(x.Item1, (x.Item3 as Pedido)))
                              .ToList();

                await _pedidoMongoManage.ExecManager(tuplePedidos);

                #endregion

                #region " Notas "

                var tupleNotas = entrys.Where(x => x.Item2 == typeof(Nota))
                           .Select(x => new Tuple<Microsoft.EntityFrameworkCore.EntityState, Nota>(x.Item1, (x.Item3 as Nota)))
                           .ToList();

                await _notaMongoManage.ExecManager(tupleNotas);

                #endregion

                #region " Pedido Itens "

                var tuplePedidosItens = entrys.Where(x => x.Item2 == typeof(PedidoItens))
                           .Select(x => new Tuple<Microsoft.EntityFrameworkCore.EntityState, PedidoItens>(x.Item1, (x.Item3 as PedidoItens)))
                           .ToList();

                await _pedidoItensMongoManage.ExecManager(tuplePedidosItens);

                #endregion

                #region " Produtos "

                var tupleProdutos = entrys.Where(x => x.Item2 == typeof(Produto))
                .Select(x => new Tuple<Microsoft.EntityFrameworkCore.EntityState, Produto>(x.Item1, (x.Item3 as Produto)))
                               .ToList();

                await _produtoMongoManage.ExecManager(tupleProdutos);


                #endregion

                #region " Clientes "
                var tupleClientes = entrys.Where(x => x.Item2 == typeof(Cliente))
                .Select(x => new Tuple<Microsoft.EntityFrameworkCore.EntityState, Cliente>(x.Item1, (x.Item3 as Cliente)))
                               .ToList();

                await _clientesMongoManage.ExecManager(tupleClientes);

                #endregion

                #region " Enderecos "
                var tupleEnderecos = entrys.Where(x => x.Item2 == typeof(Endereco))
                                     .Select(x => new Tuple<Microsoft.EntityFrameworkCore.EntityState, Endereco>(x.Item1, (x.Item3 as Endereco)))
                                     .ToList();

                await _enderecoMongoMange.ExecManager(tupleEnderecos);
                #endregion

                #region " Fornecedor "

                var tupleFornecedores = entrys.Where(x => x.Item2 == typeof(Fornecedor))
                             .Select(x => new Tuple<Microsoft.EntityFrameworkCore.EntityState, Fornecedor>(x.Item1, (x.Item3 as Fornecedor)))
                             .ToList();

                await _fornecedorMongoManage.ExecManager(tupleFornecedores);
                #endregion

                await _mediator.PublicarEventos(this);
            }
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EnderecoMapping());
            modelBuilder.ApplyConfiguration(new ClientesMapping());
            modelBuilder.ApplyConfiguration(new FornecedorMapping());
            modelBuilder.ApplyConfiguration(new NotaMapping());
            modelBuilder.ApplyConfiguration(new NotaItensMapping());
            modelBuilder.ApplyConfiguration(new PedidosItensMapping());
            modelBuilder.ApplyConfiguration(new PedidosMapping());
            modelBuilder.ApplyConfiguration(new ProdutosMapping());
        }


    }
}
