﻿// Author: Dominic Beger (Trade/ProgTrade) 2016

using System;
using System.Linq;
using System.Reflection;
using nUpdate.UpdateInstaller.Client.GuiInterface;

namespace nUpdate.UpdateInstaller.Core
{
    internal class ServiceProviderHelper
    {
        public static IServiceProvider CreateServiceProvider(Assembly assembly)
        {
            if (assembly == null)
                throw new ArgumentNullException(nameof(assembly));

            var attribute =
                assembly.GetCustomAttributes(typeof (ServiceProviderAttribute), false)
                    .Cast<ServiceProviderAttribute>()
                    .SingleOrDefault();

            if (attribute == null)
                return null;

            return (IServiceProvider) Activator.CreateInstance(attribute.ServiceType);
        }
    }
}