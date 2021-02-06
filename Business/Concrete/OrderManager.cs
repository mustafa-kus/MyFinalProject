using System;
using System.Collections.Generic;
using System.Text;
using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class OrderManager : IOrderService
    {
        IOrderDal _orderDal;
        public OrderManager(IOrderDal orderDal)
        {
            _orderDal = orderDal;
        }
        public List<Order> GetAll()
        {
            return _orderDal.GetAll();
        }

        public Order GetById(int orderId)
        {
            return _orderDal.Get(p => p.OrderId == orderId);
        }
    }
}
