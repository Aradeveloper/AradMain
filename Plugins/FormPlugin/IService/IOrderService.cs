using FormPlugin.Models;
using FormPlugin.ViewModel;
using System.Collections.Generic;

namespace FormPlugin.IService
{
    public interface IOrderService
    {
        int Count { get; }

        string Add(AddOrUpdateOrderForm model);

        void Update(AddOrUpdateOrderForm model);

        void Remove(int Id);

        OrderForm Find(int Id);

        IList<DataTableOrderForm> GetOrders(FormStatus Status);

        IList<DataTableOrderForm> GetOrders();

        AddOrUpdateOrderForm GetUpdateData(int Id);

        OrderFormDetail GetOrderDetail(int Id);
    }
}