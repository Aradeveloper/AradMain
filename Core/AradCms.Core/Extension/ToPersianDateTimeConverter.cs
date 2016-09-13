using AradCms.Core.Utility;
using AutoMapper;
using System;

namespace AradCms.Core.Extentions
{
    public class ToPersianDateTimeConverter : ITypeConverter<DateTime, string>
    {
        private readonly bool _fullDateTime;

        public ToPersianDateTimeConverter(bool fullDateTime = true)
        {
            _fullDateTime = fullDateTime;
        }

        //public string Convert(ResolutionContext context)
        //{
        //    var dateTime = context.SourceValue;
        //    if (dateTime == null) return string.Empty;
        //    var persianDateTime = new PersianDateTime((DateTime)dateTime);

        //    return _fullDateTime
        //        ? string.Format("{0},{1}", persianDateTime.ToString("dddd d MMMM yyyy ساعت hh:mm:ss tt"),
        //            RemainingDateTime.Calculate((DateTime)dateTime))
        //        : string.Format("{0},{1}", persianDateTime.ToString(PersianDateTimeFormat.ShortDateShortTime),
        //            RemainingDateTime.Calculate((DateTime)dateTime));
        //}

        public string Convert(DateTime source, string destination, ResolutionContext context)
        {
            var dateTime = context.Mapper.Map(source, destination);
            if (dateTime == null) return string.Empty;
            var persianDateTime = new PersianDateTime(DateTime.Parse(dateTime));

            return _fullDateTime
                ? string.Format("{0},{1}", persianDateTime.ToString("dddd d MMMM yyyy ساعت hh:mm:ss tt"),
                    RemainingDateTime.Calculate(DateTime.Parse(dateTime)))
                : string.Format("{0},{1}", persianDateTime.ToString(PersianDateTimeFormat.ShortDateShortTime),
                    RemainingDateTime.Calculate(DateTime.Parse(dateTime)));
        }
    }
}