using HelloWorld.Core.Domain;
using HelloWorld.Core.Domain.Interfaces.UnitOfWork;
using HelloWorld.Core.Infrastructure.Data.Context;

namespace HelloWorld.Core.Infrastructure.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private SerkoCoreDataContext _skynetCoreDataContext;

        public UnitOfWork(SerkoCoreDataContext skynetCoreDataContext)
        {
            _skynetCoreDataContext = skynetCoreDataContext;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        public ActionResponse Commit()
        {
            var rowsAffected = _skynetCoreDataContext.SaveChanges();
            return new ActionResponse(rowsAffected > 0);
        }

        private void Dispose(bool disposing)
        {
            if (!disposing)
                return;

            if (_skynetCoreDataContext == null)
                return;

            _skynetCoreDataContext.Dispose();
            _skynetCoreDataContext = null;
        }
    }
}