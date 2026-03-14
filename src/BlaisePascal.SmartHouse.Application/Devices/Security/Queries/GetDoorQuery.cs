using BlaisePascal.SmartHouse.Domain.Devices.Security;
using BlaisePascal.SmartHouse.Domain.Devices.Security.SecurityAbstraction.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Application.Devices.Security.Queries
{
    public class GetSecureDoorsQuery
    {
        private readonly ISecurityRepository _securityRepository;

        public GetSecureDoorsQuery(ISecurityRepository securityRepository)
        {
            _securityRepository = securityRepository;
        }

        public SecureDoor Execute(Guid id)
        {
            return _securityRepository.GetById(id);
        }


    }
}
