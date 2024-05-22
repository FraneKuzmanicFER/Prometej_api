using AutoMapper;
using Prometej_core.DataAccessLayer;
using Prometej_core.Models.efModels;
using Prometej_core.Models.Requests.Period;
using Prometej_core.Models.ViewModels;
using Prometej_core.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prometej_core.Services.Implementations
{
    public class PeriodService: IPeriodService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<PeriodContent> _periodRepository;
        public PeriodService(IMapper mapper, IRepository<PeriodContent> periodRepository)
        {
            _mapper = mapper;
            _periodRepository = periodRepository;
        }

        public PeriodContentViewModel GetPeriodContent(int id)
        {
            var periodEntity = _periodRepository.ReadAll().FirstOrDefault(p => p.PeriodId == id);
            if (periodEntity == null)
            {
                throw new Exception("Period not found");
            }

            PeriodContentViewModel periodViewModel = _mapper.Map<PeriodContentViewModel>(periodEntity);

            return periodViewModel;
        }

        public int UpdatePeriodContent(PeriodContentEditRequest period)
        {
            var periodEntity = _periodRepository.ReadAll().FirstOrDefault(p => p.Id == period.Id);
            if (periodEntity == null)
            {
                var periodCreationEntity = _mapper.Map<PeriodContent>(period);
                _periodRepository.Create(periodCreationEntity);
                _periodRepository.Save();

                return periodCreationEntity.Id;
            }

            periodEntity.Content = period.Content;

            _periodRepository.Update(periodEntity);
            _periodRepository.Save();

            return periodEntity.Id;
        }
    }
}
