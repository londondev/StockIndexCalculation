// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StructuremapMvc.cs" company="Web Advanced">
// Copyright 2012 Web Advanced (www.webadvanced.com)
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0

// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using StructureMap;
using System;
using System.Web.Http;
using System.Web.Mvc;
using StockPrice.DependencyResolution;
using StockPriceService;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(StockPrice.App_Start.StructureMapConfig), "Start")]

namespace StockPrice.App_Start
{
    public static class StructureMapConfig
    {
        public static void Start()
        {
            IContainer container = IoC.Initialize();

            ObjectFactory.Initialize(initialise =>
            {   
                // TODO: Structure map should normally scan all assemblies and matches interfaces and classes
                // by convention. It is not working for some reason now. I am registering manually for until fix it.
                initialise.For<IStockDataManager>().Use<StockDataManager>();
                // TODO: As we have more than one FileDataParser(CSV and EXCEL), this should be set parametically.
                // Since exceldata parser class is not implemented yet, it will stay as this until excel class is implemented.
                initialise.For<IFileDataParser>().Use<CsvDataParser>();
                initialise.SetAllProperties(action => action.NameMatches(name => name.EndsWith("Manager", StringComparison.OrdinalIgnoreCase)));
            });

            container = ObjectFactory.Container;

            DependencyResolver.SetResolver(new StructureMapDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new StructureMapDependencyResolver(container);
        }
    }

}