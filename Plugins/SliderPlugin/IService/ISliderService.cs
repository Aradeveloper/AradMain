using SliderPlugin.Models;
using SliderPlugin.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SliderPlugin.IService
{
    public interface ISliderService
    {
        int Count { get; }

        void Add(AddOrUpdate model);

        void Edit(AddOrUpdate model);

        void Remove(int Id);

        SliderModel Find(int Id);

        IList<DataTableSlider> GetSliders();

        IList<AddOrUpdate> GetVisibleSlider();

        AddOrUpdate GetSlide(int Id);

        AddOrUpdate GetDetaile(int Id);
    }
}