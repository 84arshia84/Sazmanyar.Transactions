using ApplicationService.DtoModels.SettingDtos;
using AppCore.Entities.SettingEntities.Activitycenters;
using AppCore.UnitOfWork;
using ApplicationService.Mapper.SettingMapper;
using ApplicationService.ServicesContract.Setting;
using InfraStructure.Repository.SettingRepository;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace ApplicationService.Services.SettingServices
{
    public class ActivitycenterService : IActivitycenterService
    {
        private IUnitOfWork _unitOfWork;
        private IConfiguration _configuration;
        public ActivitycenterService(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }
        /// <summary>
        /// اضافه کردن به دیتابیس
        /// </summary>
        /// <param name="activitycenter"></param>
        /// <returns></returns>
        public async Task<Tuple<string, bool>> Add(ActivitycenterDto activitycenter)
        {
            try
            {
                activitycenter.key = Guid.NewGuid();
                var model = ActivityCenterAutoMapperProfile.DtoToEntity(activitycenter);
                return await _unitOfWork.ActivitycenterRepository.Add(model);
            }
            catch (Exception ex)
            {
                return Tuple.Create("در اتصال به دیتابیس با مشکل مواجه شدیم.", false);
            }
        }
        /// <summary>
        /// گرفتن تمام مرکز فعالیت ها
        /// </summary>
        /// <returns></returns>
        public async Task<List<ActivitycenterDto>> GetAll()
        {
            try
            {
                string connectionstring = _configuration["ConnectionStrings:PwaDbConnection"];
                string Query = @"select LT_UID,LT_NAME
                    into #LookUpTable from pjpub.MSP_CUSTOM_FIELDS f
                    left join pjpub.MSP_LOOKUP_TABLES t on t.LT_UID=f.MD_LOOKUP_TABLE_UID
                    where LT_UID is not null and MD_PROP_UID_SECONDARY is not null
                    ------------------------------------------------------------------
                    select lv.LT_STRUCT_UID as ID,lv.LT_VALUE_TEXT  as Title from #LookUpTable l
                    left join pjpub.MSP_LOOKUP_TABLE_VALUES lv on lv.LT_UID=l.LT_UID
                    where LT_NAME='TaskCod'
                    ------------------------------------------------------------------
                    drop table #LookUpTable";
                var models = await _unitOfWork.ActivitycenterRepository.GetAll(Query,connectionstring);
                return ActivityCenterAutoMapperProfile.EntitiesToDtos(models);
            }
            catch (Exception ex)
            {

                return new List<ActivitycenterDto>();
            }

        }
        /// <summary>
        /// بروزرسانی مرکز فعالیت
        /// </summary>
        /// <param name="activitycenter"></param>
        /// <returns></returns>
        public async Task<Tuple<string, bool>> Update(ActivitycenterDto activitycenter)
        {
            try
            {
                var model = ActivityCenterAutoMapperProfile.DtoToEntity(activitycenter);
                return await _unitOfWork.ActivitycenterRepository.Update(model);
            }
            catch (Exception ex)
            {
                return Tuple.Create("در اتصال به دیتابیس با مشکل مواجه شدیم.", false);
            }
        }
        /// <summary>
        /// حذف مرکز فعالیت
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Tuple<string, bool>> Delete(Guid id)
        {
            try
            {
                return await _unitOfWork.ActivitycenterRepository.Delete(id);
            }
            catch (Exception ex)
            {
                return Tuple.Create("در اتصال به دیتابیس با مشکل مواجه شدیم.", false);
            }
        }
    }
}
