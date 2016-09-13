using AradCms.Core.Context;
using FormPlugin.IService;
using FormPlugin.Models;
using FormPlugin.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace FormPlugin.Service
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<OrderForm> _order;
        private TarckCode tcode = new TarckCode();

        public OrderService(IUnitOfWork unitOfWork)
        {
            _uow = unitOfWork;
            _order = _uow.Set<OrderForm>();
        }

        public int Count

        {
            get
            {
                return _order.Count();
            }
        }

        public string Add(AddOrUpdateOrderForm model)
        {
            string trackcode = tcode.GenerateId();
            var addmodel = new OrderForm { Address = model.Address, Company = model.Company, Description = model.Description, Email = model.Email, Name = model.Name, Phone = model.Phone, SendTime = DateTime.Now, Status = model.Status, Subject = model.Subject, Website = model.Website, TrackingCode = trackcode };
            _order.Add(addmodel);
            return trackcode;
        }

        public OrderForm Find(int Id)
        {
            return _order.Find(Id);
        }

        public OrderFormDetail GetOrderDetail(int Id)
        {
            var model = _order.Find(Id);
            OrderFormDetail detail = new OrderFormDetail
            {
                Id = model.Id,
                Address = model.Address,
                Company = model.Company,
                Description = model.Description,
                Email = model.Email,
                Name = model.Name,
                Phone = model.Phone,
                Status = model.Status,
                Subject = model.Subject,
                TrackingCode = model.TrackingCode,
                Website = model.Website
            };
            return detail;
        }

        public IList<DataTableOrderForm> GetOrders()
        {
            if (Count > 0)
            {
                var model = (from a in _order
                             select new DataTableOrderForm
                             {
                                 Id = a.Id,

                                 Company = a.Company,

                                 Email = a.Email,
                                 Name = a.Name,
                                 Phone = a.Phone,
                                 Status = a.Status,
                                 Subject = a.Subject,
                                 TrackingCode = a.TrackingCode
                             }).ToList();
                return model;
            }
            else
            {
                return null;
            }
        }

        public IList<DataTableOrderForm> GetOrders(FormStatus Status)
        {
            if (Count > 0)
            {
                var model = (from a in _order
                             select new DataTableOrderForm
                             {
                                 Id = a.Id,

                                 Company = a.Company,

                                 Email = a.Email,
                                 Name = a.Name,
                                 Phone = a.Phone,
                                 Status = a.Status,
                                 Subject = a.Subject,
                                 TrackingCode = a.TrackingCode
                             }).Where(x => x.Status == Status).ToList();
                return model;
            }
            else
            {
                return null;
            }
        }

        public AddOrUpdateOrderForm GetUpdateData(int Id)
        {
            var model = _order.Find(Id);
            AddOrUpdateOrderForm detail = new AddOrUpdateOrderForm
            {
                Id = model.Id,
                Address = model.Address,
                Company = model.Company,
                Description = model.Description,
                Email = model.Email,
                Name = model.Name,
                Phone = model.Phone,
                Status = model.Status,
                Subject = model.Subject,
                TrackingCode = model.TrackingCode,
                Website = model.Website
            };
            return detail;
        }

        public void Remove(int Id)
        {
            _order.Remove(_order.Find(Id));
        }

        public void Update(AddOrUpdateOrderForm model)
        {
            var updatemodel = _order.Find(model.Id);
            updatemodel.SendTime = DateTime.Now;
            updatemodel.Status = model.Status;
        }
    }
}