using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace buildingBlocksCore.Models.Request
{

    public class ProductUpdateRequest()
    {
        public Guid Id { get; set; }
    }

    public class ProductRegisterRequest()
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
    }
    public class ProdutoPagedRequest : PagedDataRequest
    {
    }
}
