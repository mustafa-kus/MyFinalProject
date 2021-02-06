﻿using System;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            ProductTest();
            //CategoryTest();
            //OrderTest();


        }

        private static void OrderTest()
        {
            OrderManager orderManager = new OrderManager(new EfOrderDal());
            foreach (var order in orderManager.GetAll())
            {
                Console.WriteLine(order.OrderId);
            }
        }

        private static void CategoryTest()
        {
            CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());
            foreach (var category in categoryManager.GetAll())
            {
                Console.WriteLine(category.CategoryName);
            }
        }

        private static void ProductTest()
        {
            ProductManager productManager = new ProductManager(new EfProductDal());
            foreach (var product in productManager.GetProductDetails())
            {
                Console.WriteLine("{0} / {1}",product.ProductName, product.CategoryName);
            }
        }
    }
}
