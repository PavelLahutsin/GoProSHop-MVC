﻿using GoProShop.BLL.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoProShop.BLL.Services.Interfaces
{
    public interface IProductSubGroupService
    {
        IEnumerable<ProductSubGroupDTO> GetProductSubGroups();
    }
}