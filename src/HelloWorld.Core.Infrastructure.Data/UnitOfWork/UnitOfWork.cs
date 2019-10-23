using HelloWorld.Core.Domain;
using HelloWorld.Core.Domain.Interfaces.UnitOfWork;
using HelloWorld.Core.Infrastructure.Data.Context;

namespace HelloWorld.Core.Infrastructure.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private DbCoreDataContext _dbCoreDataContext;

        public UnitOfWork(DbCoreDataContext skynetCoreDataContext)
        {
            _dbCoreDataContext = skynetCoreDataContext;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        public ActionResponse Commit()
        {
            var rowsAffected = _dbCoreDataContext.SaveChanges();
            return new ActionResponse(rowsAffected > 0);
        }

        private void Dispose(bool disposing)
        {
            if (!disposing)
                return;

            if (_dbCoreDataContext == null)
                return;

            _dbCoreDataContext.Dispose();
            _dbCoreDataContext = null;
        }
    }
}