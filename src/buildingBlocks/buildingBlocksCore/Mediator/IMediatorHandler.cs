using buildingBlocksCore.Mediator.Messages;
using buildingBlocksCore.Utils;

namespace buildingBlocksCore.Mediator
{
    public interface IMediatorHandler
    {
        Task PublishEvent<T>(T evento) where T : Event;
        Task<ResponseCommad<R>> SendCommand<T,R>(T comando) where T : Command<R> where R: class;
    }
}
