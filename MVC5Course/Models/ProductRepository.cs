using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;

namespace MVC5Course.Models
{   
	public  class ProductRepository : EFRepository<Product>, IProductRepository
	{
        public override IQueryable<Product> All()
        {
            return base.All().Where(p => !p.isDelete);
        }

        public IQueryable<Product> All(bool showAll)
        {
            if(showAll)
            {
                return base.All();
            }
            else
            {
                return this.All();
            }
        }


        public Product Get單筆資料ByProductId(int id)
        {
            return (this.All().FirstOrDefault(p => p.ProductId == id));
        }

        public IQueryable<Product> GetProduct列表頁資料(bool Active, bool showAll=false)
        {
            var data = this.All()
                .Where(p => p.Active.HasValue && p.Active.Value == Active)
                .OrderByDescending(p => p.ProductId).Take(10);

            return data;

        }

        public void Add一筆資料(Product product)
        {
            this.Add(product);
            this.UnitOfWork.Commit();         
        }

        public void Update(Product product)
        {
            this.UnitOfWork.Context.Entry(product).State = EntityState.Modified;
        }
	}

	public  interface IProductRepository : IRepository<Product>
	{

	}
}