using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.ServiceModel;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Core;
using System.Configuration;

namespace Fintrak.Shared.Common.Data
{
    public static class DataConnector 
    {
        public static string LoginName
        {
            get
            {
                //get login user
                string _LoginName = "Auto";
                OperationContext context = OperationContext.Current;
                if (context != null)
                {
                    try
                    {
                        _LoginName = OperationContext.Current.IncomingMessageHeaders.GetHeader<string>("String", "System");

                        if (!string.IsNullOrEmpty(_LoginName))
                        {
                            if (_LoginName.IndexOf(@"\") > -1)
                                _LoginName = "Auto";
                        }
                        else
                            _LoginName = "Auto";
                       
                    }
                    catch
                    {
                        //throw new Exception("Unable to load user's meta-data.");
                    }
                }
              

                return _LoginName;
            }
        }

        public static string CompanyCode
        {
            get
            {
                //get login user
                string _CompanyCode = "";
                OperationContext context = OperationContext.Current;
                if (context != null)
                {
                    try
                    {
                        _CompanyCode = OperationContext.Current.IncomingMessageHeaders.GetHeader<string>("Company", "System");

                        if (string.IsNullOrEmpty(_CompanyCode))
                        {
                            _CompanyCode = "";
                        }

                    }
                    catch
                    {
                        //throw new Exception("Unable to load user's meta-data.");
                    }
                }


                return _CompanyCode;
            }
        }

       
    }
}
