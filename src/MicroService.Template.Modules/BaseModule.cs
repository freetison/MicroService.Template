namespace MicroService.Template.Modules
{
    using MediatR;

    public class BaseModule
    {
        /// <summary>
        /// Defines the _mediator.
        /// </summary>
        protected readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseFeature"/> class.
        /// </summary>
        /// <param name="mediator">The mediator<see cref="IMediator"/>.</param>
        protected BaseModule(IMediator mediator) => _mediator = mediator;

        /// <summary>
        /// The Handler.
        /// </summary>
        /// <typeparam name="T">.</typeparam>
        /// <param name="request">The request<see cref="IRequest"/>.</param>
        /// <returns>The <see cref="Task{IResult}"/>.</returns>
        public async Task<T> Handler<T>(IRequest<T> request)
        {
            var result = await _mediator.Send(request);

            return result;
        }
    }
}
