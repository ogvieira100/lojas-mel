using buildingBlocksCore.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace buildingBlocksCore.Data.ReadData
{
    public static class PagedDataMongoResponseExtension
    {

        public static async Task<PagedDataResponse<TModel>> PaginateAsync<TModel>(
                                            this IFindFluent<TModel, TModel> query,
                                            PagedDataRequest pagedDataRequest,
                                            Func<IFindFluent<TModel, TModel>> funcQueryOrder

    )
    where TModel : BaseMongo

        {

            var paged = new PagedDataResponse<TModel>();

            pagedDataRequest.Page = pagedDataRequest.Page < 0 ? 1 : pagedDataRequest.Page;

            paged.Page = pagedDataRequest.Page;
            paged.PageSize = pagedDataRequest.Limit;

            long totalItemsCountTask = 0;


            totalItemsCountTask = await query.CountDocumentsAsync();


            if (funcQueryOrder is not null)
            {
                query = funcQueryOrder?.Invoke();

            }
            var startRow = (pagedDataRequest.Page - 1) * pagedDataRequest.Limit;

            if (startRow > 0)
                query = query.Skip(startRow).Limit(paged.PageSize); ;



            paged.Items = await query
                       .ToListAsync();


            paged.TotalItens = totalItemsCountTask;
            paged.TotalPages = (int)Math.Ceiling(paged.TotalItens / (double)pagedDataRequest.Limit);

            return paged;


        }
        public static async Task<PagedDataResponse<TModel>> PaginateAsync<TModel>(
         this IMongoQueryable<TModel> query,
         PagedDataRequest pagedDataRequest,
         Func<IMongoQueryable<TModel>> funcQueryOrder

         )
         where TModel : BaseMongo

        {

            var paged = new PagedDataResponse<TModel>();

            pagedDataRequest.Page = pagedDataRequest.Page < 0 ? 1 : pagedDataRequest.Page;

            paged.Page = pagedDataRequest.Page;
            paged.PageSize = pagedDataRequest.Limit;

            var totalItemsCountTask = 0;


            totalItemsCountTask = await query.CountAsync();


            if (funcQueryOrder is not null)
            {
                query = funcQueryOrder?.Invoke();

            }
            var startRow = (pagedDataRequest.Page - 1) * pagedDataRequest.Limit;

            if (startRow > 0)
                query = query.Skip(startRow);



            paged.Items = await query
                           .Take(pagedDataRequest.Limit)
                           .ToListAsync();


            paged.TotalItens = totalItemsCountTask;
            paged.TotalPages = (int)Math.Ceiling(paged.TotalItens / (double)pagedDataRequest.Limit);

            return paged;


        }

    }
}
