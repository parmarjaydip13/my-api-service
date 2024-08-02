using Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Products.Query.GetProductById;

public sealed record GetProductByIdQuery(Guid productId) : IQuery<ProductResponse>
{
}


