using JWTAuthAPI.Application.Common.Exceptions;
using JWTAuthAPI.Core.Entities;
using JWTAuthAPI.Application.Helpers;
using JWTAuthAPI.Core.Interfaces;
using MediatR;

namespace JWTAuthAPI.Application.CommandQuery.Products.Commands
{
    public record UpdateProductCommand : IRequest
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public string? Description { get; set; }
    }

    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
    {
        private readonly IRepositoryActivator _repositoryActivator;

        public UpdateProductCommandHandler(IRepositoryActivator repositoryActivator)
        {
            _repositoryActivator = repositoryActivator;
        }

        public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            Guid id = GuidParser.Parse(request.Id);

            var entity = await _repositoryActivator.Repository<Product>().GetByIdAsync(id);

            if(entity == null) 
                throw new NotFoundException(nameof(entity), request.Id);

            await _repositoryActivator.Repository<Product>().UpdateAsync(entity);
        }

    }
}
