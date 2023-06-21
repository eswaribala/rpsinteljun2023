using CQRSAPI.Contexts;
using CQRSAPI.Models;

namespace CQRSAPI.Repositories
{
    public class CartSqliteRepository
    {
        private readonly CQRSContext _CQRSContext;

        public CartSqliteRepository(CQRSContext cqrsContext)
        {
            _CQRSContext = cqrsContext; 
        }
        public Cart Create(Cart Cart)
        {
            Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<Cart> entry =
                _CQRSContext.Carts.Add(Cart);
            _CQRSContext.SaveChanges();
            return entry.Entity;
        }
        public void Update(Cart Cart)
        {
            _CQRSContext.Carts.Update(Cart);
            _CQRSContext.SaveChanges();
        }
        public void Remove(long id)
        {
            _CQRSContext.Carts.Remove(GetById(id));
            _CQRSContext.SaveChanges();
        }
        public IQueryable<Cart> GetAll()
        {
            return _CQRSContext.Carts;
        }
        public Cart GetById(long id)
        {
            return _CQRSContext.Carts.Find(id);
        }

    }
}
