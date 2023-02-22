using JWTAuthAPI.Application.Common.Exceptions;
using JWTAuthAPI.Application.Helpers;
using JWTAuthAPI.Core.Entities;
using JWTAuthAPI.Core.Entities.Identity;
using JWTAuthAPI.Core.Interfaces;
using MediatR;

namespace JWTAuthAPI.Application.CommandQuery.Products.Commands
{
    public record DeleteProductCommand(string Id) : IRequest;

    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
    {
        private readonly IRepositoryActivator _repositoryActivator;
        public DeleteProductCommandHandler(IRepositoryActivator repositoryActivator)
        {
            _repositoryActivator = repositoryActivator;
        }

        public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            Guid id = GuidParser.Parse(request.Id);

            var entity = await _repositoryActivator.Repository<Product>().GetByIdAsync(id);

            if (entity == null)
                throw new NotFoundException(nameof(ApplicationUser), request.Id);

            await _repositoryActivator.Repository<Product>().DeleteAsync(entity);
        }
    }
}
