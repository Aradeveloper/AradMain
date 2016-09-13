using AradCms.Core.Context;
using AradCms.Core.IService;
using FormPlugin.IService;
using FormPlugin.Models;
using FormPlugin.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FormPlugin.Service
{
    public class RecieptService : IReceiptService
    {
        private readonly IUnitOfWork _uow;
        private readonly IFileService _fileservice;

        private readonly IDbSet<Receipt> _reciept;

        public RecieptService(IUnitOfWork unitOfWork, IFileService fileservice)
        {
            _uow = unitOfWork;
            _fileservice = fileservice;
            _reciept = _uow.Set<Receipt>();
        }

        public int Count
        {
            get
            {
                return _reciept.Count();
            }
        }

        public void Add(AddOrUpdateReceipt model)
        {
            var addmodel = new Receipt { BankName = model.BankName, Name = model.Name, ReciptCode = model.ReciptCode, ReciptionType = model.ReciptionType, ReciptImage = model.ReciptImage, ReciptTime = model.ReciptTime, TrackingCode = model.TrackingCode };
            _reciept.Add(addmodel);
        }

        public Receipt Find(int Id)
        {
            return _reciept.Find(Id);
        }

        public ReceiptDetail GetRecieptDetail(int Id)
        {
            var model = _reciept.Find(Id);
            var detail = new ReceiptDetail
            {
                Id = model.Id,
                BankName = model.BankName,
                Name = model.Name,
                ReciptCode = model.ReciptCode,
                ReciptImage = model.ReciptImage,
                ReciptionType = model.ReciptionType,
                ReciptTime = model.ReciptTime,
                TrackingCode = model.TrackingCode
            };
            return detail;
        }

        public IList<DataTableReceipt> GetReciepts()
        {
            if (Count > 0)
            {
                var model = (from a in _reciept
                             select new DataTableReceipt
                             {
                                 ReciptTime = a.ReciptTime,
                                 BankName = a.BankName,
                                 Id = a.Id,
                                 Name = a.Name,
                                 ReciptCode = a.ReciptCode,
                                 ReciptionType = a.ReciptionType
                             }
                           ).ToList();
                return model;
            }
            else
            {
                return null;
            }
        }

        public IList<DataTableReceipt> GetReciepts(ReciptType Status)
        {
            if (Count > 0)
            {
                var model = (from a in _reciept
                             select new DataTableReceipt
                             {
                                 ReciptTime = a.ReciptTime,
                                 BankName = a.BankName,
                                 Id = a.Id,
                                 Name = a.Name,
                                 ReciptCode = a.ReciptCode,
                                 ReciptionType = a.ReciptionType
                             }
                           ).Where(x => x.ReciptionType == Status).ToList();
                return model;
            }
            else
            {
                return null;
            }
        }

        public AddOrUpdateReceipt GetUpdateData(int Id)
        {
            var model = _reciept.Find(Id);
            var detail = new AddOrUpdateReceipt
            {
                Id = model.Id,
                BankName = model.BankName,
                Name = model.Name,
                ReciptCode = model.ReciptCode,
                ReciptImage = model.ReciptImage,
                ReciptionType = model.ReciptionType,
                ReciptTime = model.ReciptTime,
                TrackingCode = model.TrackingCode
            };
            return detail;
        }

        public void Remove(int Id)
        {
            _reciept.Remove(_reciept.Find(Id));
        }

        public void Update(AddOrUpdateReceipt model)
        {
            Receipt remodel = _reciept.Find(model.Id);
        }
    }
}