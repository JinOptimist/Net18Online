using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Everything.Data.Repositories;

namespace WebPortalEverthing.Services.LoadTesting
{
    public class LoadVolumeService
    {
        private readonly ILoadVolumeTestingRepositoryReal _loadVolumeTestingRepositoryReal;

        public LoadVolumeService(ILoadVolumeTestingRepositoryReal loadVolumeTestingRepositoryReal)
        {
            _loadVolumeTestingRepositoryReal = loadVolumeTestingRepositoryReal;
        }

        /// <summary>
        /// Получает список LoadVolumes в виде SelectListItem.
        /// </summary>
        /// <returns>Список SelectListItem для выпадающего списка.</returns>
        public List<SelectListItem> GetLoadVolumes()
        {
            return _loadVolumeTestingRepositoryReal.GetAll()
                .Select(loadVolume => new SelectListItem(loadVolume.Title, loadVolume.Id.ToString()))
                .ToList();
        }
    }
}
