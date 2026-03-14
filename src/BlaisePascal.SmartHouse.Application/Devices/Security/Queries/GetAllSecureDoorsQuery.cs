using BlaisePascal.SmartHouse.Domain.Devices.Security;
using BlaisePascal.SmartHouse.Domain.Devices.Security.SecurityAbstraction.Repositories;
using System;
using System.Collections.Generic;

namespace BlaisePascal.SmartHouse.Application.Devices.Security.Queries
{
    public class GetAllSecureDoorsQuery
    {
        private readonly ISecurityRepository _securityRepository;

        public GetAllSecureDoorsQuery(ISecurityRepository securityRepository)
        {
            _securityRepository = securityRepository;
        }

        public List<SecureDoor> Execute()
        {
            return _securityRepository.GetAllDoors();
        }


    }

}