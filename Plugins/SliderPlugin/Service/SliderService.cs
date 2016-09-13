using AradCms.Core.Context;
using SliderPlugin.IService;
using SliderPlugin.Models;
using SliderPlugin.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SliderPlugin.Service
{
    public class SliderService : ISliderService
    {
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<SliderModel> _slider;

        public SliderService(IUnitOfWork unitOfWork)
        {
            _uow = unitOfWork;
            _slider = _uow.Set<SliderModel>();
        }

        public int Count
        {
            get
            {
                return _slider.Count();
            }
        }

        public void Add(AddOrUpdate model)
        {
            SliderModel slidemodel = new SliderModel { Body = model.Body, ImagePath = model.ImagePath, Published = model.Published, TitleOne = model.TitleOne, TitleThree = model.TitleThree, TitleTwo = model.TitleTwo };
            _slider.Add(slidemodel);
        }

        public void Edit(AddOrUpdate model)
        {
            var EditModel = _slider.Find(model.Id);
            EditModel.Body = model.Body;
            EditModel.ImagePath = model.ImagePath;
            EditModel.Published = model.Published;
            EditModel.TitleOne = model.TitleOne;
            EditModel.TitleThree = model.TitleThree;
            EditModel.TitleTwo = model.TitleTwo;
        }

        public IList<DataTableSlider> GetSliders()
        {
            if (Count > 0)
            {
                var model = (from a in _slider
                             select new DataTableSlider
                             {
                                 Id = a.Id,
                                 ImagePath = a.ImagePath,
                                 Published = a.Published,
                                 TitleOne = a.TitleOne
                             }
                           ).ToList();
                return model;
            }
            else
            {
                return null;
            }
        }

        public IList<AddOrUpdate> GetVisibleSlider()
        {
            if (Count > 0)
            {
                var model = (from a in _slider
                             select new AddOrUpdate
                             {
                                 Id = a.Id,
                                 ImagePath = a.ImagePath,
                                 Published = a.Published,
                                 TitleOne = a.TitleOne,
                                 TitleThree = a.TitleThree,
                                 TitleTwo = a.TitleTwo
                             }
                           ).Where(x => x.Published == true).ToList();
                return model;
            }
            else
            {
                return null;
            }
        }

        public void Remove(int Id)
        {
            _slider.Remove(_slider.Find(Id));
        }

        public AddOrUpdate GetSlide(int Id)
        {
            if (Count > 0)
            {
                var model = (from a in _slider
                             select new AddOrUpdate
                             {
                                 Id = a.Id,
                                 ImagePath = a.ImagePath,
                                 Published = a.Published,
                                 TitleOne = a.TitleOne,
                                 TitleThree = a.TitleThree,
                                 TitleTwo = a.TitleTwo
                             }
                           ).Where(x => x.Id == Id).FirstOrDefault();
                return model;
            }
            else
            {
                return null;
            }
        }

        public AddOrUpdate GetDetaile(int Id)
        {
            if (Count > 0)
            {
                var model = (from a in _slider
                             select new AddOrUpdate
                             {
                                 Id = a.Id,
                                 ImagePath = a.ImagePath,
                                 Published = a.Published,
                                 TitleOne = a.TitleOne,
                                 TitleThree = a.TitleThree,
                                 TitleTwo = a.TitleTwo
                             }
                           ).Where(x => x.Published == true && x.Id == Id).FirstOrDefault();
                return model;
            }
            else
            {
                return null;
            }
        }

        public SliderModel Find(int Id)
        {
            return _slider.Find(Id);
        }
    }
}