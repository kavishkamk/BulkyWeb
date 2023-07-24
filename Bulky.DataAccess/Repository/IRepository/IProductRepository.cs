﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bulky.Models;

namespace Bulky.DataAccess.Repository.IRepository
{
	public interface IProductRepository : IRepository<Product>
	{

		public void save();
		public void update(Product product);

	}
}
