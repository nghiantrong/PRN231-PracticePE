using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
	public class UnitOfWork : IDisposable
	{
		private readonly WatercolorsPainting2024DBContext context;
		private GenericRepository<Style> styleRepo;
		private GenericRepository<WatercolorsPainting> waterColorsPaintingRepo;
		private GenericRepository<UserAccount> accountRepo;

		public UnitOfWork(WatercolorsPainting2024DBContext context)
		{
			this.context = context;
		}

		public GenericRepository<Style> StyleRepo
		{
			get
			{
				if (this.styleRepo == null)
				{
					this.styleRepo = new GenericRepository<Style>(context);
				}
				return styleRepo;
			}
		}
		public GenericRepository<WatercolorsPainting> WatercolorsPaintingRepo
		{
			get
			{
				if (this.waterColorsPaintingRepo == null)
				{
					this.waterColorsPaintingRepo = new GenericRepository<WatercolorsPainting>(context);
				}
				return waterColorsPaintingRepo;
			}
		}
		public GenericRepository<UserAccount> AccountRepo
		{
			get
			{
				if (this.accountRepo == null)
				{
					this.accountRepo = new GenericRepository<UserAccount>(context);
				}
				return accountRepo;
			}
		}

		public void Save()
		{
			context.SaveChanges();
		}

		private bool disposed = false;

		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					context.Dispose();
				}
			}
			this.disposed = true;
		}
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
	}
}
