using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;
using System.Management;
using System.Management.Instrumentation;
using SCCMInterop;
using Microsoft.ConfigurationManagement;
using Microsoft.ConfigurationManagement.ManagementProvider;
using Microsoft.ConfigurationManagement.ManagementProvider.WqlQueryEngine;

namespace SCCMExtension
{
    [Activity("Get SCCM Updates Assignment")]
    public class GetUpdatesAssignment : IActivity
    {
        private ConnectionCredentials settings;
        private String userName = String.Empty;
        private String password = String.Empty;
        private String SCCMServer = String.Empty;

        private int ObjCount = 0;

        [ActivityConfiguration]
        public ConnectionCredentials Settings
        {
            get { return settings; }
            set { settings = value; }
        }
        public void Design(IActivityDesigner designer)
        {
            designer.AddInput("Filter").WithDefaultValue("AssignmentID LIKE '%'");
            designer.AddCorellatedData(typeof(updatesAssignment));
            designer.AddOutput("Number of Updates Assignments");
        }
        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            SCCMServer = settings.SCCMSERVER;
            userName = settings.UserName;
            password = settings.Password;

            String filter = request.Inputs["Filter"].AsString();

            //Setup WQL Connection and WMI Management Scope
            WqlConnectionManager connection = CMInterop.connectSCCMServer(SCCMServer, userName, password);
            using(connection)
            {  
                IResultObject col = null;
                col = CMInterop.getSCCMUpdatesAssignment(connection, filter);

                if (col != null)
                {
                    response.WithFiltering().PublishRange(getObjects(col));
                }
                response.Publish("Number of Updates Assignments", ObjCount);
            }
        }
        private IEnumerable<updatesAssignment> getObjects(IResultObject objList)
        {
            foreach (IResultObject obj in objList)
            {
                ObjCount++;
                yield return new updatesAssignment(obj);
            }
        }
    }
}

