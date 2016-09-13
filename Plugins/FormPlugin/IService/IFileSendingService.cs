using FormPlugin.Models;
using FormPlugin.ViewModel;
using System;
using System.Collections.Generic;

namespace FormPlugin.IService
{
    public interface IFileSendingService

    {
        int Count { get; }

        void Add(AddOrUpdateReceipt model);

        void Update(AddOrUpdateReceipt model);

        void Remove(int Id);

        Receipt Find(int Id);

        IList<DataTableReceipt> GetReciepts(ReciptType Status);

        IList<DataTableReceipt> GetReciepts();

        AddOrUpdateReceipt GetUpdateData(int Id);

        ReceiptDetail GetRecieptDetail(int Id);
    }
}