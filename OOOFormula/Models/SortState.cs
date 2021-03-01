using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOOFormula.Models
{
    public enum SortState
    {
        //Критерии сортировки
        NameAsc,
        NameDesc,

        PriceAsc,
        PriceDesc,

        DescriptionAsc,
        DescriptionDesc,

        ImageAsc,
        ImageDesc,

        StatusAsc,
        StatusDesc,

        CategoryAsc,
        CategoryDesc,

        MaterialAsc,
        MaterialDesc,

        ManufacturerAsc,
        ManufacturerDesc,

        DateAsc,
        DateDesc,

        PhoneAsc,
        PhoneDesc,

        EmailAsc,
        EmailDesc,

        MessageAsc,
        MessageDesc
    }
}
