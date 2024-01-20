using buildingBlocksCore.Data.PersistData.Context;
using buildingBlocksCore.Data.ReadData.Context;
using buildingBlocksCore.Data.ReadData.Interfaces.Query;
using buildingBlocksCore.Data.ReadData.Interfaces.Repository;
using buildingBlocksCore.Data.ReadData.MongoManage.Interfaces;
using buildingBlocksCore.Data.ReadData.MongoManage;
using buildingBlocksCore.Data.ReadData.Query;
using buildingBlocksCore.Data.ReadData.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using buildingBlocksCore.Data.PersistData.Interfaces;
using buildingBlocksCore.Data.PersistData.Uow;
using buildingBlocksCore.Data.PersistData;

namespace buildingBlocksCore.IoC
{
    public static class DataBootstrap
    {
        public static IServiceCollection ContextBootstrap(this IServiceCollection collection)

        {
            collection.AddScoped<ApplicationContext>();
            collection.AddScoped<MongoContext>();
            collection.AddScoped<IUnitOfWork, UnitOfWork>();

            collection.AddScoped(typeof(IBaseRepositoryMongo<>), typeof(BaseRepositoryMongo<>));
            collection.AddScoped(typeof(IRepositoryConsultMongo<>), typeof(RepositoryConsultMongo<>));

            collection.AddScoped(typeof(IRepositoryConsult<>), typeof(RepositoryConsult<>));
            collection.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

            return collection;
        }

        public static IServiceCollection NotaBootstrapReady(this IServiceCollection collection)

        {
            collection.AddScoped<INotaQuery, NotaQuery>();
            collection.AddScoped<INotaMongoManage, NotaMongoManage>();

            return collection;

        }
            public static IServiceCollection NotaBootstrapWrite(this IServiceCollection collection)

        {
            collection.AddScoped<INotaMongoRepository, NotaMongoRepository>();
            
            return collection;
        }

        public static IServiceCollection PedidoItensBootstrapReady(this IServiceCollection collection)

        {
            collection.AddScoped<IPedidoItensMongoManage, PedidoItensMongoManage>();
            collection.AddScoped<IPedidoItensMongoRepository, PedidoItensMongoRepository>();
            return collection;
        }
        public static IServiceCollection PedidoBootstrapReady(this IServiceCollection collection)

        {
            collection.AddScoped<IPedidoMongoRepository, PedidoMongoRepository>();
            collection.AddScoped<IPedidoQuery, PedidoQuery>();
            collection.AddScoped<IPedidoMongoManage, PedidoMongoManage>();
            return collection;
        }

        public static IServiceCollection PedidoItensBootstrapWrite(this IServiceCollection collection)
        {



            return collection;
        }
        public static IServiceCollection PedidoBootstrapWrite(this IServiceCollection collection)
        {
          
           

            return collection;
        }


        public static IServiceCollection EnderecoBootstrapReady(this IServiceCollection collection)

        {
            collection.AddScoped<IEnderecoMongoMange, EnderecoMongoMange>();
            return collection;
        }

        public static IServiceCollection ProdutoBootstrapWrite(this IServiceCollection collection)

        {
            return collection;
        }

        public static IServiceCollection ProdutoBootstrapReady(this IServiceCollection collection)

        {
            collection.AddScoped<IProdutoQuery, ProdutoQuery>();
            collection.AddScoped<IProdutoMongoManage, ProdutoMongoManage>();
            collection.AddScoped<IProdutoMongoRepository, ProdutoMongoRepository>();
            return collection;
        }

        public static IServiceCollection FornecedorBootstrapReady(this IServiceCollection collection)

        {
            collection.AddScoped<IFornecedorMongoManage, FornecedorMongoManage>();
            collection.AddScoped<IFornecedorQuery, FornecedorQuery>();
            collection.AddScoped<IFornecedorMongoRepository, FornecedorMongoRepository>();
            return collection;
        }
        public static IServiceCollection FornecedorBootstrapWrite(this IServiceCollection collection)

        {
           
            return collection;
        }

        public static IServiceCollection CustomerBootstrapWrite(this IServiceCollection collection)

        {
          
            return collection;
        }
        public static IServiceCollection CustomerBootstrapReady(this IServiceCollection collection)

        {
            collection.AddScoped<IClienteQuery, ClienteQuery>();
            collection.AddScoped<IClienteMongoRepository, ClienteMongoRepository>();
            collection.AddScoped<IClientesMongoManage, ClientesMongoManage>();

            return collection;
        }
    }
}
